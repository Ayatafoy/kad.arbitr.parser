using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kad.Arbitr.ru.Models;
using xNet;

namespace Kad.Arbitr.ru
{
    class ContentResultRequestGet : ContentResultRequest
    {
        public ContentResultRequestGet(string url, List<string> proxies) : base(url, proxies)
        {
        }

        public override string Request(HttpRequest request, string url)
        {
            Thread.Sleep(100);
            request.Get(url).None();
            return request.Get(url).ToString();
        }
    }
}
