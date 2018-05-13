using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class ArrayListUpload
    {
        public int TitleUploadMethod(ArticleModel aList)
        {
            int titleId = 0;
            DatabaseFunctions aDatabaseFunctions = new DatabaseFunctions();
            bool isTitleExistAlready = aDatabaseFunctions.IsTitleExist(aList.articleTitle);
            if (!isTitleExistAlready)
            {
               bool isSavedSuccessfull=aDatabaseFunctions.titleSaveToDatabase(aList.articleTitle);
                if (isSavedSuccessfull)
                 {
                   titleId = aDatabaseFunctions.GetLastTitleIdInserted();       
                 }
                else
                 {
                    titleId= (-2);
                 }
             }
            else
             {
                    titleId= (-1);
             }
            return titleId; 
        }



        public int ArticleDataUploadMethod(int titleId,Dictionary<string,string> aList)
        {
            DatabaseFunctions aDatabaseFunctions = new DatabaseFunctions();
            int articleInsertedResult=aDatabaseFunctions.GetArticleDataInserted(titleId, aList);

            return articleInsertedResult;
        }


    }
}