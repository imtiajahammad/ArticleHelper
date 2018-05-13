using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class ArticleModel
    {
        public string articleTitle { get; set; }
        public Dictionary<string,string> articleSeparateParagraphs { get; set; }
    }
}