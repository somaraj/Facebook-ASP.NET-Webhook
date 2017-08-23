using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using FacebookLeadAdsWebhooks.Model;

namespace FacebookLeadAdsWebhooks.Controller
{
    public class WebhooksController : ApiController
    {
        #region Get Request
        [HttpGet]
        public string Get()
        {
            var response = HttpContext.Current.Request.QueryString["hub.challenge"];
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
