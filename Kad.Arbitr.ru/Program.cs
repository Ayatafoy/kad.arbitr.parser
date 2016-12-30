using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xNet;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using Kad.Arbitr.ru.Models;
using System.Text.RegularExpressions;

namespace Kad.Arbitr.ru
{
    class Program
    {
        private static List<string> _proxies;
        

        public Program(string pathToProxy)
        {
            _proxies = File.ReadLines("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/proxy.txt").ToList();
        }
        static void Main(string[] args)
        {
            var dl = new Program("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/proxy.txt");
            try
            {
                GetResult();

            }
            catch (HttpException ex)
            {
                Console.WriteLine("Произошла ошибка при работе с HTTP-сервером: {0}", ex.Message);

                switch (ex.Status)
                {
                    case HttpExceptionStatus.Other:
                        Console.WriteLine("Неизвестная ошибка");
                        break;

                    case HttpExceptionStatus.ProtocolError:
                        Console.WriteLine("Код состояния: {0}", (int)ex.HttpStatusCode);
                        break;

                    case HttpExceptionStatus.ConnectFailure:
                        Console.WriteLine("Не удалось соединиться с HTTP-сервером.");
                        GetResult();
                        break;

                    case HttpExceptionStatus.SendFailure:
                        Console.WriteLine("Не удалось отправить запрос HTTP-серверу.");
                        break;

                    case HttpExceptionStatus.ReceiveFailure:
                        Console.WriteLine("Не удалось загрузить ответ от HTTP-сервера.");
                        break;
                }
                Console.Read();
            }
            Console.WriteLine("Ok");
            Console.Read();
        }

        private static void GetResult()
        {
            for (int page = 1; page < 3; page++)
            {
                //ResponseModelLawsuits model = new ResponseModelLawsuits()
                //{
                //    Page = page,
                //    Count = 25,
                //    Courts = new List<string>(),
                //    DateFrom = null,
                //    DateTo = null,
                //    Sides = new List<string>(),
                //    Judges = new List<string>(),
                //    CaseNumbers = new List<string>(),
                //    WithVKSInstances = false
                //};
                //ContentResultRequest contentResultRequest = new ContentResultRequestPost(model, "http://kad.arbitr.ru/Kad/SearchInstances", _proxies);
                //List<string> hrefs = getHrefs(contentResultRequest.GetProxiedContent());
                //File.AppendAllLines("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/response.txt", hrefs.ToArray());
                //foreach (string href in hrefs)
                //{
                //    contentResultRequest = new ContentResultRequestGet(href.Replace("Card", "PrintCard"), _proxies);
                //    string result = contentResultRequest.GetProxiedContent();
                //    getAdditionInfo(href, result);
                //    File.WriteAllText("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/result.txt", result);
                    
                //}



                ResponseModelSolution model = new ResponseModelSolution()
                {
                    Cases = new List<string>(),
                    Count = 25,           
                    DateFrom = new DateTime(2000, 01, 01, 00, 00, 00),
                    DateTo = new DateTime(2030, 01, 01, 23, 59, 59),
                    GroupByCase = false,
                    Judges = new List<string>(),
                    Page = page,
                    Sides = new List<string>(),
                    Text = "взыскать"
                };
                ContentResultRequest contentResultRequest = new ContentResultRequestPost(model, "http://ras.arbitr.ru/Ras/Search", _proxies);
                List<string> hrefs = getHrefs(contentResultRequest.GetProxiedContent());
                File.AppendAllLines("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/response.txt", hrefs.ToArray());
                foreach (string href in hrefs)
                {
                    Console.WriteLine(href);
                    //contentResultRequest = new ContentResultRequestGet(href.Replace("Card", "PrintCard"), _proxies);
                    //string result = contentResultRequest.GetProxiedContent();
                    //getAdditionInfo(href, result);
                    //File.WriteAllText("C:/Users/aromanov/Documents/Visual Studio 2015/Projects/Kad.Arbitr.ru/result.txt", result);

                }
            }
        }

        private static List<string> getHrefs(string html) {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);

            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }

        private static void getAdditionInfo(string href, string html)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);
            string cardNumber = htmlSnippet.DocumentNode.SelectNodes("//body//h1").First().InnerText;
            string cardCategory = htmlSnippet.GetElementbyId("case-category").InnerText;
            string plaintiffName = htmlSnippet.DocumentNode.SelectNodes("//span[@class='js-rollover b-newRollover']")[0].InnerText;
            string plaintiffAdress = htmlSnippet.DocumentNode.SelectNodes("//span[@class='js-rolloverHtml']")[0].InnerText;
            string defendantName = htmlSnippet.DocumentNode.SelectNodes("//span[@class='js-rollover b-newRollover']")[1].InnerText;
            string defendantAdress = htmlSnippet.DocumentNode.SelectNodes("//span[@class='js-rolloverHtml']")[1].InnerText;
            string trialName = htmlSnippet.DocumentNode.SelectNodes("//span[@class='instantion-name']")[0].InnerText;
            string lawsuitDate = htmlSnippet.DocumentNode.SelectNodes("//p[@class='case-date']")[0].InnerText;
            string lawsuitType = htmlSnippet.DocumentNode.SelectNodes("//h2[@class='b-case-result']")[1].InnerText;
            string amountOfTheClaim = htmlSnippet.DocumentNode.SelectNodes("//span[@class='additional-info']")[0].InnerText;
            amountOfTheClaim = amountOfTheClaim.Substring(amountOfTheClaim.LastIndexOf('й') + 1, amountOfTheClaim.Length - amountOfTheClaim.LastIndexOf('й') - 1);            
            int[] matches = Regex.Matches(amountOfTheClaim, "\\d+")
                .Cast<Match>()
                .Select(x => int.Parse(x.Value))
                .ToArray();           
            if (matches.Length != 0)
                Console.WriteLine(href + "   " + amountOfTheClaim);
        }

    }
}
