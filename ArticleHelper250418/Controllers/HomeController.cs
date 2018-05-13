
using ArticleHelper250418.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleHelper250418.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SpecificCriteriaPaperShowThirdOne()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetUploadedPdfFiles()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetUploadedPdfFiles(FileUploadModel files)
        {
            List<ArticleModel> aArticleModelList = new List<ArticleModel>();
            List<UploadDataModel> aUploadDataModelList = new List<UploadDataModel>();
            foreach (var item in files.File)
            {

                IndividualFileRead aIndividualFileRead = new IndividualFileRead();
                List<DataModel> listOfAllLines = new List<DataModel>();
                listOfAllLines = aIndividualFileRead.MethodToIndividualFileRead(item);  //got all data in lines
                ExtractionOfCharacteristics aExtractionOfCharacteristics = new ExtractionOfCharacteristics();





                ArticleModel aArticleModel = new ArticleModel();
                aArticleModel = aExtractionOfCharacteristics.Method(listOfAllLines);//got the data in form of ArticleModel(title,list)
                aArticleModelList.Add(aArticleModel);
                //25/04/2018
                //database portion
                UploadDataModel aUploadDataModel = new UploadDataModel();
                aUploadDataModel.artitleTitle = aArticleModel.articleTitle;

                // portion starts for uploading in the database
                ArrayListUpload aArrayListUpload = new ArrayListUpload();

                int titleId = aArrayListUpload.TitleUploadMethod(aArticleModel);
                if (titleId == (-1))
                {
                    //isAlreadyExists
                    aUploadDataModel.uploadProcessMessage = "Article data is already existed";//"Title is already exists";
                }
                else if (titleId == (-2))
                {
                    //notSavedSuccessfully
                    aUploadDataModel.uploadProcessMessage = "Not saved successfully";
                }
                else if (titleId == 0)
                {
                    //noExecutionHappened
                    aUploadDataModel.uploadProcessMessage = "no execution happened, try again";
                }
                else
                {
                    //saveRestDataWithID
                    int articleSavedresult = aArrayListUpload.ArticleDataUploadMethod(titleId, aArticleModel.articleSeparateParagraphs);
                    if (articleSavedresult == -3 || articleSavedresult == 0)
                    {

                        aUploadDataModel.uploadProcessMessage = "Article data not saved successfully";
                    }
                    else
                    {
                        aUploadDataModel.uploadProcessMessage = "Article data saved successfully";
                    }
                }
                aUploadDataModelList.Add(aUploadDataModel);
            }


            TempData["list"] = aUploadDataModelList;
            return RedirectToAction("GetAllUploadedTitlesResults", "Home"/*new { ListOfUploadedTitles=aUploadDataModelList }*/);
        }








        [HttpGet]
        public ActionResult GetUploadedPdfFilesWithCriteria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SpecificCriteriaPaperShow(FileUploadModelWIthCriteria files)
        {
            List<Object> list = new List<object>();
            Dictionary<string,string> mergedlist = new Dictionary<string, string>();

            foreach (var item in files.File)
            {
                IndividualFileRead aIndividualFileRead = new IndividualFileRead();
                List<DataModel> listOfAllLines = new List<DataModel>();
                
                listOfAllLines = aIndividualFileRead.MethodToIndividualFileRead(item);  //got all data in lines
                if (files.criteriaNo == 1)
                {
                    CriteriaOneClass aCriteriaOneClass = new CriteriaOneClass();
                    list = aCriteriaOneClass.ExtractFileContent(listOfAllLines);
                    ViewBag.contents = list[0];
                    Dictionary<string, string> contents = new Dictionary<string, string>();
    //                contents = (Dictionary<string,string>)list[0];
                    foreach (var a in (Dictionary<string, string>)list[0])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
  //                  ViewBag.headings = list[1];
                    foreach (var a in (Dictionary<string, SubHeadingDataModel>)list[1])
                    {
                        mergedlist.Add(a.Key, a.Value.childSubheadingValue);
                    }
//                    ViewBag.subHeadings = list[2];
                    foreach (var a in (Dictionary<string, string>)list[2])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
                    ViewBag.list = mergedlist;


                }
                else if (files.criteriaNo == 2)
                {

                    CriteriaTwoClass aCriteriaOneClass = new CriteriaTwoClass();
                    list = aCriteriaOneClass.ExtractFileContent(listOfAllLines);
                    //ViewBag.contents = list[0];
                    foreach(var a in (Dictionary<string, string>)list[0])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
                    //ViewBag.subHeadings = list[1];
                    foreach (var a in (Dictionary<string, SubHeadingDataModel>)list[1])
                    {
                        mergedlist.Add(a.Key, a.Value.childSubheadingValue);
                    }
                    //ViewBag.headings = list[2];
                    foreach (var a in (Dictionary<string, string>)list[2])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
                    ViewBag.list = mergedlist;


                }
                else if (files.criteriaNo == 3)
                {
                    CriteriaThreeClass aCriteriaThreeClass = new CriteriaThreeClass();
                    list = aCriteriaThreeClass.ExtractFileContent(listOfAllLines);
                    ViewBag.contents = list[0];
                    foreach (var a in (Dictionary<string, string>)list[0])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
                    ViewBag.subHeadings_portions_SeparateHeadings = list[1];
                    foreach (var a in (Dictionary<string, SubHeadingDataModel>)list[1])
                    {
                        mergedlist.Add(a.Key, a.Value.childSubheadingValue);
                    }
                    ViewBag.subHeadings_portions = list[2];
                    foreach (var a in (Dictionary<string, SubHeadingDataModel>)list[2])
                    {
                        mergedlist.Add(a.Key, a.Value.childSubheadingValue);
                    }
                    ViewBag.subHeadings = list[3];
                    foreach (var a in (Dictionary<string, SubHeadingDataModel>)list[3])
                    {
                        mergedlist.Add(a.Key, a.Value.childSubheadingValue);
                    }
                    ViewBag.headings = list[4];
                    foreach (var a in (Dictionary<string, string>)list[4])
                    {
                        mergedlist.Add(a.Key, a.Value);
                    }
                    ViewBag.list = mergedlist;

                }
            }
            return View();
            
        }





        private double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));

        }

        private int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];

        }

        //[HttpGet]
        public ActionResult GetAllUploadedTitlesResults(/*List<UploadDataModel> ListOfUploadedTitles*/)
        {
            List<UploadDataModel> ListOfUploadedTitles = TempData["list"] as List<UploadDataModel>;
            //var model = TempData["list"] as List<QuestionClass.Tablefields>;

            ViewBag.ListOFResults = ListOfUploadedTitles;
            //TempData["list2"] = ListOfUploadedTitles;
            return View();
        }

        public ActionResult GetAllTitlesFromDatabase()
        {
            DatabaseFunctions aDatabaseFunctions = new DatabaseFunctions();
            List<TitleModel> listOfAllTitlesWtihIDs = aDatabaseFunctions.GetAllTitlesWithId();
            ViewBag.ListOfAllTitlesWtihIDs = listOfAllTitlesWtihIDs;
            return View();
        }


        [HttpGet]
        public ActionResult ViewIndividualPaper(int titleId)
        {
            DatabaseFunctions aDatabaseFunctions = new DatabaseFunctions();
            ArticleModel aArticleModel = new ArticleModel();
            aArticleModel = aDatabaseFunctions.GetIndividualPaper(titleId);
            ViewBag.Article = aArticleModel;
            return View();
        }

        public ActionResult CompareTwoPaper(int aTitleId, int bTitleId)
        {
            DatabaseFunctions aDatabaseFunctions = new DatabaseFunctions();
            ArticleModel articleModelForFirstone = aDatabaseFunctions.GetIndividualPaper(aTitleId);
            ArticleModel articleModelForSecondone = aDatabaseFunctions.GetIndividualPaper(bTitleId);
            //List<ArticleModel> ListOfUploadedTitles = TempData["list2"] as List<ArticleModel>;

            return View();
        }
    }
}