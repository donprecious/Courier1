using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using RestSharp;

namespace Courier.Services
{
    public class SmsService
    {
        /// <summary>
        /// format
        ///  https://www.bulksmsnigeria.com/api/v1/sms/create
        /// ?api_token=XLPfEAt3aIakYtne03V8wvVQrXLRQevP4gOBJ2qEY8DxBajMA6AV6IpEqY55&
        /// from=BulkSMS.ng
        /// &to=2348051170615&
        /// body=Welcome&dnd=2
        /// api from bulk sms nigeria
        /// https://www.bulksmsnigeria.com/app/api-settings
        /// </summary>
        ///  ///  https://www.bulksmsnigeria.com/api/v1/sms/create?api_token=XLPfEAt3aIakYtne03V8wvVQrXLRQevP4gOBJ2qEY8DxBajMA6AV6IpEqY55&from=BulkSMS.ng&to=2348051170615&body=Welcome&dnd=1
        private string ApiKey = "XLPfEAt3aIakYtne03V8wvVQrXLRQevP4gOBJ2qEY8DxBajMA6AV6IpEqY55";

        RestClient client = new RestClient("https://www.bulksmsnigeria.com/api/v1/sms/");
        public bool SendMessage(List<string> phones,string from , string message , int dnd = 1)
        {
            var sendTo = "";
            foreach (var i in phones)
            {
                sendTo += i + ",";
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = new RestRequest("create");
            request.AddParameter("api_token", ApiKey);
            request.AddParameter("from", from);
            request.AddParameter("to", sendTo);
            request.AddParameter("body", message);
            request.AddParameter("dnd", dnd);
            var response = client.Get(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}