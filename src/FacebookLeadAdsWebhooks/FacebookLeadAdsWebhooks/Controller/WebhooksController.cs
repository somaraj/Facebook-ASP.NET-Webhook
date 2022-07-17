using FacebookLeadAdsWebhooks.Helpers;
using FacebookLeadAdsWebhooks.Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FacebookLeadAdsWebhooks.Controller
{
    public class WebhooksController : ApiController
    {
        #region Fields

        private const string _verificationToken = "abc";

        #endregion

        #region Get Request

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var mode = HttpContext.Current.Request.QueryString["hub.mode"].ToString();
                var challenge = HttpContext.Current.Request.QueryString["hub.challenge"].ToString();
                var verifyToken = HttpContext.Current.Request.QueryString["hub.verify_token"].ToString();

                MongoDBManager.Insert("Verification", $"Mode->{mode}, Challenge->{challenge}, Verification Token->{verifyToken}");

                if (verifyToken == _verificationToken)
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(challenge) };
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Invalid verification token") };
            }
            catch (Exception ex)
            {
                MongoDBManager.Insert($"Error", $"Get->{ex.Message}, StackTrace->{ex.StackTrace}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message) };
            }
        }

        #endregion Get Request

        #region Post Request

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] JsonDataModel data)
        {
            MongoDBManager.Insert("LeadGen", $"Data -> {JsonConvert.SerializeObject(data)}");
            try
            {
                var entry = data.Entry.FirstOrDefault();
                var change = entry?.Changes.FirstOrDefault();
                if (change == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);

                //Generate user access token here https://developers.facebook.com/tools/accesstoken/
                const string token = "<Your Token>";

                var leadUrl = $"https://graph.facebook.com/v14.0/{change.Value.LeadGenId}?access_token={token}";
                var formUrl = $"https://graph.facebook.com/v14.0/{change.Value.FormId}?access_token={token}";

                MongoDBManager.Insert("LeadGen", $"LeadGenId -> {change.Value.LeadGenId}, FormId -> {change.Value.FormId}");

                if (!string.IsNullOrEmpty(token))
                {
                    using (var httpClientLead = new HttpClient())
                    {
                        var response = await httpClientLead.GetStringAsync(formUrl);
                        if (!string.IsNullOrEmpty(response))
                        {
                            var jsonObjLead = JsonConvert.DeserializeObject<LeadFormData>(response);
                            //jsonObjLead.Name contains the lead ad name

                            //If response is valid get the field data
                            using (var httpClientFields = new HttpClient())
                            {
                                var responseFields = await httpClientFields.GetStringAsync(leadUrl);
                                if (!string.IsNullOrEmpty(responseFields))
                                {
                                    var jsonObjFields = JsonConvert.DeserializeObject<LeadData>(responseFields);
                                    //jsonObjFields.FieldData contains the field value
                                }
                            }
                        }
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                MongoDBManager.Insert($"Error", $"Post->{ex.Message}, StackTrace->{ex.StackTrace}");
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        #endregion Post Request
    }
}
