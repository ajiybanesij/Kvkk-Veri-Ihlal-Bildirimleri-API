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
        private List<string> _errorList;
        public Parser()
        {
            _adress = new Adresses();
            _client = new WebClient();
            _client.Encoding = Encoding.UTF8;
            _sumList = new List<Summary>();
            _xpath = new xPaths();
            _errorList = new List<string>();
        }


        private int PageCount()
        {
            _url = new Uri(_adress.Summary);
            string htmlContent = _client.DownloadString(_url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            List<HtmlNode> nodes = document.DocumentNode.SelectNodes(_xpath.count).ToList();
            if (nodes.Count == 0 || nodes == null)
            {
                return -1;
            }
            else
            {
                return nodes.Count - 2;
            }

        }


        private void OneSummary(HtmlDocument document)
        {
            Summary sum = new Summary();
            HtmlNode title_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_title);
            var title_result = TitleParser(title_node);
            if (!string.IsNullOrEmpty(title_result))
            {
                sum.title = title_result;
            }
            else
            {
                //Title Gelmedi. Hata Var...
                _errorList.Add("Title Error");
                Console.WriteLine(title_result);
            }

            HtmlNode date_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_date);
            var date_result = ObjParser(date_node);
            if (!string.IsNullOrEmpty(date_result))
            {
                sum.date = date_result;

            }
            else
            {
                //Date Gelmedi. Hata Var...
                _errorList.Add("Date Error");
                Console.WriteLine(date_result);
            }


            HtmlNode url_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_url);
            var url_result = UrlParser(url_node);
            if (!string.IsNullOrEmpty(url_result))
            {
                sum.url = url_result;

            }
            else
            {
                //Url Gelmedi. Hata Var...
                _errorList.Add("Url Error");
                Console.WriteLine(url_result);
            }

            HtmlNode image_node = document.DocumentNode.SelectSingleNode(_xpath.big_ihlal_img);
            var image_result = ImageParser(image_node);
            if (!string.IsNullOrEmpty(image_result))
            {
                sum.image = image_result;

            }
            else
            {
                //Image Gelmedi. Hata Var...
                _errorList.Add("Image Error");
                Console.WriteLine(image_result);
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
                    if (!string.IsNullOrEmpty(title_result))
                    {
                        sum.title = title_result;
                    }
                    else
                    {
                        //Title Gelmedi. Hata Var...
                        _errorList.Add("Title Error");
                        Console.WriteLine(title_result);
                    }

                    string items_date = String.Format(_xpath.items_date, i);
                    HtmlNode date_node = document.DocumentNode.SelectSingleNode(items_date);
                    var date_result = ObjParser(date_node);
                    if (!string.IsNullOrEmpty(date_result))
                    {
                        sum.date = date_result;
                    }
                    else
                    {
                        //Date Gelmedi. Hata Var...
                        _errorList.Add("Date Error");
                        Console.WriteLine(date_result);
                    }

                    string items_url = String.Format(_xpath.items_url, i);
                    HtmlNode url_node = document.DocumentNode.SelectSingleNode(items_url);
                    var url_result = UrlParser(url_node);
                    if (!string.IsNullOrEmpty(url_result))
                    {
                        sum.url = url_result;

                    }
                    else
                    {
                        //Url Gelmedi. Hata Var...
                        _errorList.Add("Url Error");
                        Console.WriteLine(url_result);
                    }

                    string items_image = String.Format(_xpath.items_image, i);
                    HtmlNode image_node = document.DocumentNode.SelectSingleNode(items_image);
                    var image_result = ImageParser(image_node);
                    if (!string.IsNullOrEmpty(image_result))
                    {
                        sum.image = image_result;

                    }
                    else
                    {
                        //Image Gelmedi. Hata Var...
                        _errorList.Add("Image Error");
                        Console.WriteLine(image_result);
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

        public Result<Summary> ParseData()
        {
            int page_count = PageCount();
            if (page_count != -1)
            {
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
                if (_sumList.Count != 0 && _errorList.Count == 0)
                {
                    Result<Summary> result = new Result<Summary>();
                    result.Content = _sumList;
                    result.IsSuccess = true;
                    result.Detail = "Success";
                    return result;
                }
                else
                {
                    Result<Summary> result = new Result<Summary>();
                    result.Content = _sumList;
                    result.IsSuccess = true;
                    result.Detail = _errorList;
                    return result;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
