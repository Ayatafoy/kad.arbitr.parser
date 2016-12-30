using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Kad.Arbitr.ru.Models;
using xNet;

namespace Kad.Arbitr.ru
{
    class ContentResultRequestPost : ContentResultRequest
    {
        private ResponseModel _responseModel;
        public ContentResultRequestPost(ResponseModel responseModel, string url, List<string> proxies) : base(url, proxies)
        {
            _responseModel = responseModel;
        }

        public override string Request(HttpRequest request, string url)
        {
            //Thread.Sleep(100);
            string json = new JavaScriptSerializer().Serialize(_responseModel);
           // request.Post(url, json, "application/json").None();
            return request.Post(url, json, "application/json").ToString();
        }
    }
}
