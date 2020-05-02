using HtmlAgilityPack;
using Parser.IHelper;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Helper
{
    public class Parsers : IParsers
    {
        public Result ImageParser(HtmlNode node)
        {
            Result resultObj = new Result();
            try
            {
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
            catch (Exception e)
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = e.ToString();
                resultObj.Content = null;
                return resultObj;
            }

        }

        public Result ObjParser(HtmlNode node)
        {
            Result resultObj = new Result();

            try
            {
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
            catch (Exception e)
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = e.ToString();
                resultObj.Content = null;
                return resultObj;
            }
        }

        public Result TitleParser(HtmlNode node)
        {
            Result resultObj = new Result();
            try
            {
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
                };
            }
            catch (Exception e)
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = e.ToString();
                resultObj.Content = null;
                return resultObj;
            }
        }

        public Result UrlParser(HtmlNode node)
        {
            Result resultObj = new Result();    
            try
            {
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
            catch (Exception e)
            {
                resultObj.IsSuccess = false;
                resultObj.Detail = e.ToString();
                resultObj.Content = null;
                return resultObj;
            }
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
    }
}
