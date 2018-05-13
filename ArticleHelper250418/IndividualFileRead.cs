using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class IndividualFileRead: TextWithFontExtractionStategy
    {
        public List<DataModel> MethodToIndividualFileRead(HttpPostedFileBase file )
        {
            List<DataModel> finalList2 = new List<DataModel>();
            // reader ==> http://itextsupport.com/apidocs/itext5/5.5.9/com/itextpdf/text/pdf/PdfReader.html#pdfVersion

            //PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "abcd16.pdf"));// 2,3,11,15,16,17,19
            PdfReader reader = new PdfReader(file.InputStream);
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();//strategy==> http://itextsupport.com/apidocs/itext5/5.5.9/com/itextpdf/text/pdf/parser/TextExtractionStrategy.html

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {


                iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i/*1*/, S);
                
                /* PdfTextExtractor.GetTextFromPage(reader, 6, S) ==>>    http://itextsupport.com/apidocs/itext5/5.5.9/com/itextpdf/text/pdf/parser/PdfTextExtractor.html
                              Console.WriteLine(F);
                Console.WriteLine("Work has listed up");
                List<DataModel> listGet = S.GetGiving();
                */


                finalList2 = S.GetGiving();
                /*foreach(DataModel m in listGet)
                {
                    finalList2.Add(m);
                }*/



            }
            //   Program p = new Program();
            // finalList2 = p.Method2();
            //ExtractionOfCharacteristics aExtractionOfCharacteristics = new ExtractionOfCharacteristics();
            //aExtractionOfCharacteristics.Method(finalList2);

            //this.Close();
            return finalList2;

        }
    }
}