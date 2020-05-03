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
        public string ImageParser(HtmlNode node)
        {
            
            try
            {
                if (node != null)
                {
                    var data_image_url = node.GetAttributeValue("src", string.Empty);

                    if (String_ControlContent(data_image_url))
                    {
                        data_image_url = data_image_url.Replace("amp;", "");
                        return "https://www.kvkk.gov.tr" + data_image_url;
                         
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Image Parser Error : " + e);
                return null;
            }

        }

        public string ObjParser(HtmlNode node)
        {
            try
            {
                if (node != null)
                {
                    var data_obj = node.InnerHtml;

                    if (String_ControlContent(data_obj))
                    {
                        return data_obj;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Object Parser Error : " + e);
                return null;
            }
        }

        public string TitleParser(HtmlNode node)
        {
          
            try
            {
                if (node != null)
                {
                    var data_title = node.InnerHtml;

                    if (String_ControlContent(data_title))
                    {
                        data_title = data_title.Replace(" - ", " – ").Replace("\"","");
                        return data_title.Split(" – ")[1];
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Title Parser Error : " + e);
                return null;
            }
        }

        public string UrlParser(HtmlNode node)
        {
             
            try
            {
                if (node != null)
                {
                    var data_url = node.GetAttributeValue("href", string.Empty);

                    if (String_ControlContent(data_url))
                    {
                        data_url = data_url.Replace("amp;", "");
                        return "https://www.kvkk.gov.tr" + data_url;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Url Parser Error : " + e);
                return null;
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
