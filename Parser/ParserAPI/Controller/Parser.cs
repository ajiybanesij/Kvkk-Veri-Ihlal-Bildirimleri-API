using HtmlAgilityPack;
using Parser.Helper;
using ParserAPI.Helper;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ParserAPI.Controller
{
    public class Parser : Parsers
    {

        private Adresses _adress;
        private Uri _url;
        private WebClient _client;
        private List<Summary> _sumList;
        private xPaths _xpath;
        public Parser()
        {
            _adress = new Adresses();
            _client = new WebClient();
            _client.Encoding = Encoding.UTF8;
            _sumList = new List<Summary>();
            _xpath = new xPaths();
        }


        private int PageCount()
        {
            _url = new Uri(_adress.Summary);
            string htmlContent = _client.DownloadString(_url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            List<HtmlNode> nodes = document.DocumentNode.SelectNodes(_xpath.count).ToList();
            return nodes.Count - 2;
        }


        private void OneSummary(HtmlDocument document)
        {
            Summary sum = new Summary();

            HtmlNode title_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_title);
            var title_result = TitleParser(title_node);
            if (title_result.IsSuccess)
            {
                sum.title = title_result.Content.ToString();
            }
            else
            {
                //Title Gelmedi. Hata Var..
                Console.WriteLine(title_result.Detail);
            }

            HtmlNode date_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_date);
            var date_result = ObjParser(date_node);
            if (date_result.IsSuccess)
            {
                sum.date = date_result.Content.ToString();

            }
            else
            {
                //Date Gelmedi. Hata Var..
                Console.WriteLine(date_result.Detail);
            }


            HtmlNode url_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_url);
            var url_result = UrlParser(url_node);
            if (url_result.IsSuccess)
            {
                sum.url = url_result.Content.ToString();

            }
            else
            {
                //Url Gelmedi. Hata Var..
                Console.WriteLine(url_result.Detail);
            }

            HtmlNode image_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_img);
            var image_result = ImageParser(image_node);
            if (image_result.IsSuccess)
            {
                sum.image = image_result.Content.ToString();

            }
            else
            {
                //Image Gelmedi. Hata Var..
                Console.WriteLine(image_result.Detail);
            }

            _sumList.Add(sum);


        }

        private void ParseSummary(HtmlDocument document)
        {
            List<HtmlNode> nodes = document.DocumentNode.SelectNodes(_xpath.items_list).ToList();

            if (nodes != null || nodes.Count != 0)
            {
                for (int i = 2; i <= nodes.Count + 1; i++)
                {
                    Summary sum = new Summary();


                    string item_title = String.Format(_xpath.items_title, i);

                    HtmlNode row = document.DocumentNode.SelectSingleNode(item_title);


                    var title_result = TitleParser(row);
                    if (title_result.IsSuccess)
                    {
                        sum.title = title_result.Content.ToString();
                    }
                    else
                    {
                        //Title Gelmedi. Hata Var..
                        Console.WriteLine(title_result.Detail);
                    }

                    string items_date = String.Format(_xpath.items_date, i);
                    HtmlNode date_node = document.DocumentNode.SelectSingleNode(items_date);
                    var date_result = ObjParser(date_node);
                    if (date_result.IsSuccess)
                    {
                        sum.date = date_result.Content.ToString();

                    }
                    else
                    {
                        //Date Gelmedi. Hata Var..
                        Console.WriteLine(date_result.Detail);
                    }

                    string items_url = String.Format(_xpath.items_url, i);
                    HtmlNode url_node = document.DocumentNode.SelectSingleNode(items_url);
                    var url_result = UrlParser(url_node);
                    if (url_result.IsSuccess)
                    {
                        sum.url = url_result.Content.ToString();

                    }
                    else
                    {
                        //Url Gelmedi. Hata Var..
                        Console.WriteLine(url_result.Detail);
                    }

                    string items_image = String.Format(_xpath.items_image, i);
                    HtmlNode image_node = document.DocumentNode.SelectSingleNode(items_image);
                    var image_result = ImageParser(image_node);
                    if (image_result.IsSuccess)
                    {
                        sum.image = image_result.Content.ToString();

                    }
                    else
                    {
                        //Image Gelmedi. Hata Var..
                        Console.WriteLine(image_result.Detail);
                    }

                    _sumList.Add(sum);

                }
            }
            else
            {
                //Siteyi alamadı.
                //URL Hatası olabilir.
                //Sunucu hatası olabilir.
                Console.WriteLine("ERROR URL");
            }
        }

        public List<Summary> ParseData()
        {
            int page_count = PageCount();

            for (int i = 1; i <= PageCount(); i++)
            {
                _url = new Uri(_adress.Summary + i);
                string htmlContent = _client.DownloadString(_url);
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(htmlContent);
                if (i == 1)
                {
                    OneSummary(document);
                }
                else
                {
                    ParseSummary(document);
                }
            }
            return _sumList;

        }
    }
}
