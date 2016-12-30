using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kad.Arbitr.ru.Models;
using System.Web.Script.Serialization;
using System.Net;
using xNet;

namespace Kad.Arbitr.ru
{
    abstract class ContentResultRequest
    {
        
        private string _url;
        private List<string> _proxies;
        private int _goodProxyIndex = 0;

        public ContentResultRequest(string url, List<string> proxies) {
            _url = url;
            _proxies = proxies;
        }

        public string GetProxiedContent()
        {
            var numProxies = _proxies.Count;
            for (int i = 0; i < numProxies; i++)
            {
                try
                {
                    return GetContent(_proxies[_goodProxyIndex]);
                }
                catch (WebException)
                {
                    _goodProxyIndex = (_goodProxyIndex + 1) % numProxies;
                }
            }
            throw new NoGoodProxyException("Proxyes is not valid!");
        }

        private string GetContent(string proxy)
        {
            var proxyClient = HttpProxyClient.Parse(proxy);
            var request = new HttpRequest();
            request.Proxy = proxyClient;
            return Request(request, _url);
        }

        public abstract string Request(HttpRequest request, string url);
    }
}
