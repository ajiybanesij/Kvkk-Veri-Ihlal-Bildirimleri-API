using HtmlAgilityPack;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.IHelper
{
    public interface IParsers
    {
        Result TitleParser(HtmlNode node);
        Result ObjParser(HtmlNode node);
        Result UrlParser(HtmlNode node);
        Result ImageParser(HtmlNode node);
    }
}
