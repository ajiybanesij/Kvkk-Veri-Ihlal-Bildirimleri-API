using HtmlAgilityPack;
using ParserAPI.Helper;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParserAPI.Controller
{
    public class MainController
    {

        private Adresses _adress;
        private Uri _url;
        private WebClient _client;
        private List<Summary> _sumList;
        public MainController()
        {
            _adress = new Adresses();
            _client = new WebClient();
            _sumList = new List<Summary>();
        }
        public bool String_ControlContent(string content)
        {

            if (content != null || content.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Result TitleParser(HtmlNode node)
        {
            Result resultObj = new Result();
            if (node != null)
            {
                var data_title = node.InnerHtml;

                if (String_ControlContent(data_title))
                {
                    resultObj.IsSuccess = true;
                    resultObj.Detail = resultObj.Success;
                    resultObj.Content = data_title.Split(" – ")[1];
                    return resultObj;
                }
                else
                {
                    resultObj.IsSuccess = false;
                    resultObj.Detail = "Title is Empty";
                    resultObj.Content = null;
                    return resultObj;
                }

            }
            else
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = "Title Node is Empty";
                resultObj.Content = null;
                return resultObj;
            }
        }



        public void ParseSummary()
        {
            _url = new Uri(_adress.Summary);
            _client.Encoding = System.Text.Encoding.UTF8;

            string htmlContent = _client.DownloadString(_url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            List<HtmlNode> nodes = document.DocumentNode.SelectNodes("/html/body/div[3]/div/div[1]/div[1]").ToList();

            if (nodes != null || nodes.Count != 0)
            {
                foreach (var item in nodes)
                {
                    Summary sum = new Summary();
                    HtmlNode row = item.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/h3");

                    var title_result = TitleParser(row);
                    if (title_result.IsSuccess)
                    {
                        sum.title = title_result.Content.ToString();
                        Console.WriteLine(sum.title);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                //Siteyi alamadı.
                //URL Hatası olabilir.
                //Sunucu hatası olabilir.
                Console.WriteLine("ERROR URL");
            }




            var a = 0;
        }
    }
}
