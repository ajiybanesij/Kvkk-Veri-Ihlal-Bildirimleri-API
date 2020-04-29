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
            _client.Encoding = System.Text.Encoding.UTF8;
            _sumList = new List<Summary>();
        }
        private bool String_ControlContent(string content)
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

        private int PageCount()
        {
            _url = new Uri(_adress.Summary);
            string htmlContent = _client.DownloadString(_url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            List<HtmlNode> nodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'page-link')]").ToList();
            return nodes.Count - 2;
        }
        private Result TitleParser(HtmlNode node)
        {
            Result resultObj = new Result();
            if (node != null)
            {
                var data_title = node.InnerHtml;

                if (String_ControlContent(data_title))
                {
                    resultObj.IsSuccess = true;
                    resultObj.Detail = resultObj.Success;
                    data_title = data_title.Replace(" - ", " – ");
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

        private Result ObjParser(HtmlNode node)
        {
            Result resultObj = new Result();
            if (node != null)
            {
                var data_obj = node.InnerHtml;

                if (String_ControlContent(data_obj))
                {
                    resultObj.IsSuccess = true;
                    resultObj.Detail = resultObj.Success;
                    resultObj.Content = data_obj;
                    return resultObj;
                }
                else
                {
                    resultObj.IsSuccess = false;
                    resultObj.Detail = "Object is Empty";
                    resultObj.Content = null;
                    return resultObj;
                }

            }
            else
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = "Object Node is Empty";
                resultObj.Content = null;
                return resultObj;
            }
        }

        private Result UrlParser(HtmlNode node)
        {
            Result resultObj = new Result();
            if (node != null)
            {
                var data_url = node.GetAttributeValue("href", string.Empty);

                if (String_ControlContent(data_url))
                {
                    resultObj.IsSuccess = true;
                    resultObj.Detail = resultObj.Success;
                    resultObj.Content = "https://www.kvkk.gov.tr" + data_url;
                    return resultObj;
                }
                else
                {
                    resultObj.IsSuccess = false;
                    resultObj.Detail = "Url is Empty";
                    resultObj.Content = null;
                    return resultObj;
                }

            }
            else
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = "Url Node is Empty";
                resultObj.Content = null;
                return resultObj;
            }
        }

        private Result ImageParser(HtmlNode node)
        {
            Result resultObj = new Result();
            if (node != null)
            {
                var data_image_url = node.GetAttributeValue("src", string.Empty);

                if (String_ControlContent(data_image_url))
                {
                    resultObj.IsSuccess = true;
                    resultObj.Detail = resultObj.Success;
                    resultObj.Content = "https://www.kvkk.gov.tr" + data_image_url;
                    return resultObj;
                }
                else
                {
                    resultObj.IsSuccess = false;
                    resultObj.Detail = "Url is Empty";
                    resultObj.Content = null;
                    return resultObj;
                }

            }
            else
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = "Url Node is Empty";
                resultObj.Content = null;
                return resultObj;
            }
        }

        private void OneSummary(HtmlDocument document)
        {
            Summary sum = new Summary();

            HtmlNode title_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/h3");
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

            HtmlNode date_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/p[1]");
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


            HtmlNode url_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/div/a");
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

            HtmlNode image_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[1]/img");
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
            List<HtmlNode> nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'col-lg-4 col-md-6 col-sm-12 pb-3')]").ToList();

            if (nodes != null || nodes.Count != 0)
            {
                for (int i = 2; i <= nodes.Count + 1; i++)
                {
                    Summary sum = new Summary();

                    HtmlNode row = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[" + i + "]/div/div[2]/h4/a");

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


                    HtmlNode date_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[" + i + "]/div/div[2]/p/span/a");
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


                    HtmlNode url_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[" + i + "]/div/div[2]/h4/a");
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

                    HtmlNode image_node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[1]/div[" + i + "]/div/div[1]/a/img");
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
