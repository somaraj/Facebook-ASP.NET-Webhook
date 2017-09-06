using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using FacebookLeadAdsWebhooks.Model;

namespace FacebookLeadAdsWebhooks.Controller
{
    public class WebhooksController : ApiController
    {
        #region Get Request
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(HttpContext.Current.Request.QueryString["hub.challenge"])
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }
        #endregion Get Request

        #region Post Request

        [HttpPost]
        public void Post([FromBody] JsonData data)
        {
            try
            {
                var entry = data.Entry.FirstOrDefault();
                //Get change
                var change = entry?.Changes.FirstOrDefault();
                if (change == null) return;

                //Get lead Id
                var leadId = change.Value.LeadGenId;

                //Lead Id is used for further processing
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error >> {ex.Message} >> StackTrace {ex.StackTrace}");
            }
        }

        #endregion Post Request
    }
}
