using HtmlAgilityPack;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.IHelper
{
    public interface IParsers
    {
        string TitleParser(HtmlNode node);
        string ObjParser(HtmlNode node);
        string UrlParser(HtmlNode node);
        string ImageParser(HtmlNode node);
    }
}
