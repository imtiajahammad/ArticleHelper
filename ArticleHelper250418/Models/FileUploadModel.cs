using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class FileUploadModel
    {
        public IEnumerable<HttpPostedFileBase> File { get; set; }
    }
}