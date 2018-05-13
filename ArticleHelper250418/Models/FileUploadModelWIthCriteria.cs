
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418.Models
{
    public class FileUploadModelWIthCriteria
    {
        public IEnumerable<HttpPostedFileBase> File { get; set; }
        public int criteriaNo { get; set; }
    }
}