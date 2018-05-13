using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections;
using ArticleHelper250418;

namespace ArticleHelper250418
{
    public class TextWithFontExtractionStategy: iTextSharp.text.pdf.parser.ITextExtractionStrategy
    {
        List<DataModel> listOfData = new List<DataModel>();
        //HTML buffer
        private StringBuilder result = new StringBuilder();

        //Store last used properties
        private Vector lastBaseLine;
        private string lastFont;
        private float lastFontSize;
        private string lastDataFontStyle;
        private string lastDataItself;
        private string dataItself;

        //http://api.itextpdf.com/itext/com/itextpdf/text/pdf/parser/TextRenderInfo.html
        private enum TextRenderMode
        {
            FillText = 0,
            StrokeText = 1,
            FillThenStrokeText = 2,
            Invisible = 3,
            FillTextAndAddToPathForClipping = 4,
            StrokeTextAndAddToPathForClipping = 5,
            FillThenStrokeTextAndAddToPathForClipping = 6,
            AddTextToPaddForClipping = 7
        }



        public void RenderText(iTextSharp.text.pdf.parser.TextRenderInfo renderInfo)
        {

            //to check the components , you can use to print line by line
            //DataModel dataModel = new DataModel(); //1.1.18
            string curDataItself = renderInfo.GetText();
            //Console.WriteLine(curDataItself);
            string curDataFontStyle = "";
            string curFont = renderInfo.GetFont().PostscriptFontName;  // http://itextsupport.com/apidocs/itext5/5.5.9/com/itextpdf/text/pdf/parser/TextRenderInfo.html#getFont--

            if ((renderInfo.GetTextRenderMode() == 2/*(int)TextRenderMode.FillThenStrokeText*/))
            {
                curDataFontStyle = "BOLD";

                curFont += "-Bold";
            }

            //This code assumes that if the baseline changes then we're on a newline
            Vector curBaseline = renderInfo.GetBaseline().GetStartPoint();
            Vector topRight = renderInfo.GetAscentLine().GetEndPoint();
            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(curBaseline[Vector.I1], curBaseline[Vector.I2], topRight[Vector.I1], topRight[Vector.I2]);
            Single curFontSize = rect.Height;
            //dataModel.fontSize = curFontSize;


            if (lastBaseLine != null && lastBaseLine[Vector.I2] != curBaseline[Vector.I2])
            {
                DataModel aDataModel = new DataModel();
                aDataModel.dataItself = lastDataItself;
                aDataModel.fontSize = lastFontSize;
                aDataModel.dataFontStyle = lastDataFontStyle;
                aDataModel.fontName = lastFont;
                listOfData.Add(aDataModel);


                lastDataItself = curDataItself;
                lastFont = curFont;
                lastDataFontStyle = curDataFontStyle;
                lastFontSize = curFontSize;

            }
            else
            {
                lastDataItself += curDataItself;
                lastFont = curFont;
                lastFontSize = curFontSize;
                lastDataFontStyle = curDataFontStyle;


            }

            if (lastBaseLine == null)
            {
                lastDataItself = curDataItself;
                lastFont = curFont;
                lastFontSize = curFontSize;
                lastDataFontStyle = curDataFontStyle;
            }

            this.lastBaseLine = curBaseline;

        }







        public string GetResultantText()
        {
            //If we wrote anything then we'll always have a missing closing tag so close it here
            if (result.Length > 0)
            {
                result.Append("</span>");
            }
            /*Program pp = new Program();
            pp.Method(listOfData);
            //listOfData.Clear();
            List<DataModel> listee = new List<DataModel>();
            listee = listOfData;
            for(int i=(listee.Count-1);i>65;i--)
            {
                Console.WriteLine(listee[i].dataItself +"  "  + listee[i].fontSize);
                Console.WriteLine();
                Console.WriteLine();
            }*/




            return result.ToString();
        }


        //Not needed
        public void BeginTextBlock() { }
        public void EndTextBlock()
        {

        }
        public void RenderImage(ImageRenderInfo renderInfo) { }
        public List<DataModel> GetGiving()
        {

            //giving = list;
            return listOfData;
        }
    }
}