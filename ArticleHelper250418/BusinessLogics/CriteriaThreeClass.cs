using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class CriteriaThreeClass
    {
        public List<Object> ExtractFileContent(List<DataModel> listOfAllLines)
        {
            TextPropertyCheckFunctions aTextPropertyCheckFunctions = new TextPropertyCheckFunctions();

            const string generalFontName = "Times New Roman";
            const double generalFontSize = 8.02;

            const double titleFontSize = 19.32;

            const double aboutAthorNameFontSize = 8.89;
            const double aboutRestFontSize = 8.02;

            const double abstractFontSize = 7.24;
            const string abstractFontStyle = "BoldItalic";

            const double keywordsFontSize = 7.24;
            const string keywordsFontStyle = "BoldItalic";

            const double headingFontSize = 8.02;

            const double subHeadingFontSize = 8.02;
            const string subHeadingFontStyle = "Italic";

            const double subHeading_Portion_FontSize = 8.02;
            const string subHeading_Portion_FontStyle = "Italic";

            const double subHeadingPortion_separateHeding_FontSize = 8.02;
            const string subHeadingPortion_separateHeding_FontStyle = "BoldItalic";

            const double subHeadingPortion_part_FontSize = 8.02;
            const string subHeadingPortion_part_FontStyle = "BoldItalic";

            const double acknowledgementFontSize = 8.02;


            const double referenceFontSize = 6.47;
            //const double referenceContentFontSize = 6.42;

            //const double authorProfileFontSize = 6.42;
            //const double authorProfileContentFontSize = 6.42;






            List<int> titleIndex = new List<int>();
            List<int> authorNameIndex = new List<int>();
            List<int> authorDetailsIndex = new List<int>();
            List<int> abstractIndex = new List<int>();
            List<int> keywordIndex = new List<int>();
            List<int> headingIndex = new List<int>();
            List<int> subHeadingIndex = new List<int>();
            
            List<int> subHeading_Portion_Index = new List<int>();

            List<int> subHeadingPortion_separateHeading_Index = new List<int>();

            List<int> subHeadingPortion_part_Index = new List<int>();

            List<int> acknowledgementIndex = new List<int>();
            List<int> referenceIndex = new List<int>();
            List<int> referenceContentIndex = new List<int>();
            
            



            List<HeadingStructureModel> listOfHeadings = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadings = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadingsPortions = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadingsPortions_separateHeading = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadingsPortions_part = new List<HeadingStructureModel>();

            for (int i = 0; i < listOfAllLines.Count; i++)
            {
                if (i == 885 || i == 879 || i == 947)
                {

                }
                double ii = System.Math.Round(listOfAllLines[i].fontSize, 2);
                if (!String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself))//not to operate on null or white spaces
                {
                    if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(titleFontSize))
                    {
                        titleIndex.Add(i);
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(aboutAthorNameFontSize))
                    {
                        bool checkCharactersForAuthorsName = aTextPropertyCheckFunctions.CheckCharactersForAuthorsName(listOfAllLines[i].dataItself.ToArray());
                        if (checkCharactersForAuthorsName)
                        {
                            authorNameIndex.Add(i);
                        }

                    }

                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(keywordsFontSize) && listOfAllLines[i].fontName.Contains("Bold") )
                    {
                        if (listOfAllLines[i].dataItself.Contains("Abstract") && !listOfAllLines[i].fontName.Contains("Italic"))
                        {
                            abstractIndex.Add(i);
                            
                        }
                        else if (listOfAllLines[i].dataItself.Contains("Keywords") && listOfAllLines[i].fontName.Contains("Italic"))
                        {
                            keywordIndex.Add(i);
                        }


                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(subHeadingPortion_separateHeding_FontSize) && listOfAllLines[i].fontName.Contains(subHeadingPortion_separateHeding_FontStyle))
                    {//separate headings under portions 2 types


                        if (listOfAllLines[i].dataItself.Contains("Analysis and Summary"))
                        {
                             subHeadingPortion_part_Index.Add(i);
                            listOfSubHeadingsPortions_part.Add(new HeadingStructureModel { headingString = listOfAllLines[i].dataItself, lineNo=i});
                        }
                        else 
                        {
                            subHeadingPortion_separateHeading_Index.Add(i);
                            listOfSubHeadingsPortions_separateHeading.Add(new HeadingStructureModel { headingString=listOfAllLines[i].dataItself, lineNo=i});
                        }


                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(subHeadingPortion_separateHeding_FontSize) && listOfAllLines[i].fontName.Contains(subHeadingFontStyle) && !listOfAllLines[i].fontName.Contains("Bold"))
                    {//for sub heading and sub heading portions

                        string originalData = listOfAllLines[i].dataItself;
                        char[] originalDataInCharArray = originalData.ToCharArray();//convert into char array

                        string lineText = listOfAllLines[i].dataItself.Replace(" ", String.Empty);//space reduced
                        char[] lineInCharArray = lineText.ToCharArray();//convert into char array

                        bool lastCharacterCheck = aTextPropertyCheckFunctions.CheckLastCharacterOfaString(lineInCharArray);// to check if the string ends with a fullStop or not
                        if (lastCharacterCheck)
                        {
                            bool oneFullStopCheck = aTextPropertyCheckFunctions.NumberingFullStopCheckFunction(lineInCharArray);//heading has one fullstop, after its numbering characters. so checking if one or more
                            if (oneFullStopCheck)
                            {
                                //sub headings
                                int fullStopIndex = aTextPropertyCheckFunctions.FindSingleFullstopIndex(lineInCharArray);//to find out the full stop index
                                if (fullStopIndex > 0)//fullstop index should be after the numbering, so zero not possible
                                {
                                    bool numberingCharactersCheckOnlyCapitalAlphabets = aTextPropertyCheckFunctions.NumberingCharacterCheckFunctionForSubheadings(fullStopIndex, lineInCharArray);//to check the numbering characters are A-Z
                                    if (numberingCharactersCheckOnlyCapitalAlphabets)
                                    {
                                        string finalHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(fullStopIndex, originalDataInCharArray);//cut the numbering portion, get the heading
                                        char[] finalStringInCharArray = finalHeadingString.Replace(" ", String.Empty).ToCharArray();//original string without white spaces and convert that to array char
                                        bool capitalCheck = aTextPropertyCheckFunctions.CapitalCheckFunction(finalStringInCharArray);//check if there is any string
                                        if (!capitalCheck)
                                        {
                                            subHeadingIndex.Add(i);
                                            listOfSubHeadings.Add(new HeadingStructureModel { headingString = finalHeadingString, lineNo = i });

                                        }
                                    }
                                }
                            }
                            else
                            {
                                //portions
                                bool noFullStopCheck = aTextPropertyCheckFunctions.NoFullStopCheckFunction(lineInCharArray);
                                if (noFullStopCheck)
                                {
                                    int parenthesisIndex = aTextPropertyCheckFunctions.FindSingleCloseParenthesisIndex(lineInCharArray);
                                    if (parenthesisIndex > 0)
                                    {
                                        bool numberingCharactersCheckForNumbers= aTextPropertyCheckFunctions.NumberingCharacterCheckFunctionForNumbers(parenthesisIndex, lineInCharArray);//to check the numbering characters are 0-9
                                        if (numberingCharactersCheckForNumbers)
                                        {
                                            string finalHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(parenthesisIndex, originalDataInCharArray);//cut the numbering portion, get the heading
                                            char[] finalStringInCharArray = finalHeadingString.Replace(" ", String.Empty).ToCharArray();//original string without white spaces and convert that to array char
                                            bool capitalCheck = aTextPropertyCheckFunctions.CapitalCheckFunction(finalStringInCharArray);//check if there is any string
                                            if (!capitalCheck)
                                            {
                                                subHeading_Portion_Index.Add(i);
                                                listOfSubHeadingsPortions.Add(new HeadingStructureModel { headingString = finalHeadingString, lineNo = i });

                                            }
                                        }
                                    }
                                }

                            }
                        }

                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(headingFontSize) && !listOfAllLines[i].fontName.Contains("BoldItalic"))
                    {
                        //headings

                        string originalData = listOfAllLines[i].dataItself;
                        char[] originalDataInCharArray = originalData.ToCharArray();//convert into char array

                        string lineText = listOfAllLines[i].dataItself.Replace(" ", String.Empty);//space reduced
                        char[] lineInCharArray = lineText.ToCharArray();//convert into char array

                        bool lastCharacterCheck = aTextPropertyCheckFunctions.CheckLastCharacterOfaString(lineInCharArray);// to check if the string ends with a fullStop or not
                        if (lastCharacterCheck)
                        {
                            bool oneFullStopCheck = aTextPropertyCheckFunctions.NumberingFullStopCheckFunction(lineInCharArray);//heading has one fullstop, after its numbering characters. so checking if one or more
                            if (oneFullStopCheck)//for heading
                            {
                                int fullStopIndex = aTextPropertyCheckFunctions.FindSingleFullstopIndex(lineInCharArray);//to find out the full stop index
                                if (fullStopIndex > 0)//fullstop index should be after the numbering, so zero not possible
                                {
                                    bool numberingCharactersCheckOnlyCapitalRoman = aTextPropertyCheckFunctions.HeadingNumberingCharacterCheckFunctionForCriteriaThreeClass(fullStopIndex, lineInCharArray);//to check the numbering characters are I V X
                                    

                                    if (numberingCharactersCheckOnlyCapitalRoman)//for headings THROUGH X,I,V 
                                    {
                                        string finalHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(fullStopIndex, originalDataInCharArray);//cut the numbering portion, get the heading
                                        char[] finalStringInCharArray = finalHeadingString.Replace(" ", String.Empty).ToCharArray();//original string without white spaces and convert that to array char
                                        bool capitalCheck = aTextPropertyCheckFunctions.CapitalCheckFunction(finalStringInCharArray);//check if there is any string
                                        if (capitalCheck)
                                        {
                                            headingIndex.Add(i);
                                            listOfHeadings.Add(new HeadingStructureModel { headingString = finalHeadingString, lineNo = i });

                                        }
                                    }
                                    else
                                    {
                                        //for sub-headings 
                                        if (listOfAllLines[i].fontName.Contains("Italic"))
                                        {
                                            bool numberingCharacterCheckForSubHeading = aTextPropertyCheckFunctions.NumberingCharacterCheckFunctionForSubheadings(fullStopIndex, lineInCharArray);//checking if the numbering is on A to Z capital
                                            if (numberingCharacterCheckForSubHeading)
                                            {

                                                string finalSubHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(fullStopIndex, originalDataInCharArray);
                                                char[] finalSubHeadingStringInCharArray = finalSubHeadingString.ToArray();
                                                subHeadingIndex.Add(i);
                                                listOfSubHeadings.Add(new HeadingStructureModel { headingString = finalSubHeadingString, lineNo = i });
                                            }
                                        }

                                    }

                                }
                            }
                            else
                            {//for acknowledgement
                                if(String.Equals(originalData, "ACKNOWLEDGMENT ", StringComparison.OrdinalIgnoreCase))
                                {
                                    acknowledgementIndex.Add(i);
                                }

                            }
                        }
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(referenceFontSize))
                    {
                        if (!(listOfAllLines[i].dataItself.ToUpper().Contains("TABLE") || listOfAllLines[i].dataItself.ToUpper().Contains("Fig")))
                        {
                            bool capitalCheck= aTextPropertyCheckFunctions.CapitalCheckFunction(listOfAllLines[i].dataItself.Replace(" ", String.Empty).ToArray()); 
                            if (capitalCheck)
                            {
                                if (String.Equals(listOfAllLines[i].dataItself, "REFERENCES ", StringComparison.OrdinalIgnoreCase))
                                {
                                    referenceIndex.Add(i);
                                }

                            }
                        }
                    }
                }



            }

            //*****need to do from here
            Dictionary<string, LineStructureModel> dictionaryOfContents = new Dictionary<string, LineStructureModel>();
            Dictionary<string, LineStructureModel> dictionaryOfHeadings = new Dictionary<string, LineStructureModel>();
            Dictionary<string, LineStructureModelForSubHeadings> dictionaryOfSubHeadings = new Dictionary<string, LineStructureModelForSubHeadings>();
            Dictionary<string, LineStructureModelForSubHeadings> dictionaryOfSubHeadings_portions = new Dictionary<string, LineStructureModelForSubHeadings>();
            Dictionary<string, LineStructureModelForSubHeadings> dictionaryOfSubHeadings_portions_separateHeadings = new Dictionary<string, LineStructureModelForSubHeadings>();
            Dictionary<string, LineStructureModelForSubHeadings> dictionaryOfSubHeadings_portions_summary = new Dictionary<string, LineStructureModelForSubHeadings>();

            dictionaryOfContents.Add("Title", new LineStructureModel { lineStartNo = titleIndex.First(), lineEndNo = titleIndex.Last() });
            dictionaryOfContents.Add("Writer", new LineStructureModel { lineStartNo = authorNameIndex.First(), lineEndNo = abstractIndex.First() - 1 });
            //dictionaryOfContents.Add("WriterTwo", new LineStructureModel { lineStartNo = authorNameIndex.Last(), lineEndNo = (abstractIndex.First() - 1) });
            dictionaryOfContents.Add("Abstract", new LineStructureModel { lineStartNo = abstractIndex.First(), lineEndNo = keywordIndex.First()-1 });
            dictionaryOfContents.Add("Keywords", new LineStructureModel { lineStartNo = keywordIndex.First(), lineEndNo = headingIndex.First()-1 });
            dictionaryOfContents.Add("ACKNOWLEDGMENT", new LineStructureModel { lineStartNo = acknowledgementIndex.First() + 1, lineEndNo = referenceIndex.First()-1 });
            dictionaryOfContents.Add("REFERENCES", new LineStructureModel { lineStartNo = referenceIndex.First() + 1, lineEndNo = listOfAllLines.Count() - 1 });
            //headings
            for (int u = 0; u < listOfHeadings.Count(); u++)
            {
                if (u == (listOfHeadings.Count() - 1))
                {
                    dictionaryOfHeadings.Add(listOfHeadings[u].headingString, new LineStructureModel { lineStartNo = listOfHeadings[u].lineNo + 1, lineEndNo = (acknowledgementIndex.First() - 1) });
                }
                else
                {
                    dictionaryOfHeadings.Add(listOfHeadings[u].headingString, new LineStructureModel { lineStartNo = listOfHeadings[u].lineNo + 1, lineEndNo = listOfHeadings[u + 1].lineNo - 1 });
                }

            }

            //subheadings

            foreach (var a in dictionaryOfHeadings)
            {
                for (int i = 0; i < listOfSubHeadings.Count(); i++)
                {
                    if (a.Value.lineStartNo < listOfSubHeadings[i].lineNo && a.Value.lineEndNo > listOfSubHeadings[i].lineNo)
                    {

                        try
                        {
                            if (a.Value.lineEndNo > listOfSubHeadings[i + 1].lineNo)
                            {
                                dictionaryOfSubHeadings.Add(listOfSubHeadings[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadings[i].lineNo + 1, SHlineEndNo = listOfSubHeadings[i + 1].lineNo - 1 });
                            }
                            else
                            {
                                dictionaryOfSubHeadings.Add(listOfSubHeadings[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadings[i].lineNo + 1, SHlineEndNo = a.Value.lineEndNo });
                            }
                        }
                        catch (Exception e)
                        {
                            dictionaryOfSubHeadings.Add(listOfSubHeadings[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadings[i].lineNo + 1, SHlineEndNo = a.Value.lineEndNo });
                        }
                    }
                }
            }

            //subheadings portion

            foreach (var a in dictionaryOfSubHeadings)//subheading from heading
            {
                for (int i = 0; i < listOfSubHeadingsPortions.Count(); i++)
                {
                    if (a.Value.SHlineStartNo < listOfSubHeadingsPortions[i].lineNo && a.Value.SHlineEndNo > listOfSubHeadingsPortions[i].lineNo)
                    {

                        try
                        {
                            if (a.Value.SHlineEndNo > listOfSubHeadingsPortions[i + 1].lineNo)
                            {
                                dictionaryOfSubHeadings_portions.Add(listOfSubHeadingsPortions[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions[i].lineNo + 1, SHlineEndNo = listOfSubHeadingsPortions[i + 1].lineNo - 1 });
                            }
                            else
                            {
                                dictionaryOfSubHeadings_portions.Add(listOfSubHeadingsPortions[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions[i].lineNo + 1, SHlineEndNo = a.Value.SHlineEndNo });
                            }
                        }
                        catch (Exception e)
                        {
                            dictionaryOfSubHeadings_portions.Add(listOfSubHeadingsPortions[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions[i].lineNo + 1, SHlineEndNo = a.Value.SHlineEndNo });
                        }
                    }
                }
            }
            //subheadings portion separate headings

            foreach (var a in dictionaryOfSubHeadings_portions)//
            {
                for (int i = 0; i < listOfSubHeadingsPortions_separateHeading.Count(); i++)
                {
                    if (a.Value.SHlineStartNo < listOfSubHeadingsPortions_separateHeading[i].lineNo && a.Value.SHlineEndNo > listOfSubHeadingsPortions_separateHeading[i].lineNo)
                    {

                        try
                        {
                            if (a.Value.SHlineEndNo > listOfSubHeadingsPortions_separateHeading[i + 1].lineNo)
                            {
                                dictionaryOfSubHeadings_portions_separateHeadings.Add(listOfSubHeadingsPortions_separateHeading[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions_separateHeading[i].lineNo + 1, SHlineEndNo = listOfSubHeadingsPortions_separateHeading[i + 1].lineNo - 1 });
                            }
                            else
                            {
                                dictionaryOfSubHeadings_portions_separateHeadings.Add(listOfSubHeadingsPortions_separateHeading[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions_separateHeading[i].lineNo + 1, SHlineEndNo = a.Value.SHlineEndNo });
                            }
                        }
                        catch (Exception e)
                        {
                            dictionaryOfSubHeadings_portions_separateHeadings.Add(listOfSubHeadingsPortions_separateHeading[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadingsPortions_separateHeading[i].lineNo + 1, SHlineEndNo = a.Value.SHlineEndNo });
                        }
                    }
                }
            }

            //make a class model and separate things from paper
            Dictionary<string, string> contents = new Dictionary<string, string>();
            Dictionary<string, string> headings = new Dictionary<string, string>();
            Dictionary<string, SubHeadingDataModel> subheadings = new Dictionary<string, SubHeadingDataModel>();
            Dictionary<string, SubHeadingDataModel> subheadings_portions = new Dictionary<string, SubHeadingDataModel>();
            Dictionary<string, SubHeadingDataModel> subheadings_portions_SeparateHeadings = new Dictionary<string, SubHeadingDataModel>();


            //gathering of contents
            for (int h = 0; h < dictionaryOfContents.Count(); h++)
            {
                string key = dictionaryOfContents.ElementAt(h).Key;
                string value = "";
                for (int i = dictionaryOfContents.ElementAt(h).Value.lineStartNo; i <= dictionaryOfContents.ElementAt(h).Value.lineEndNo; i++)
                {
                    if (i == dictionaryOfContents.ElementAt(h).Value.lineStartNo)
                    {
                        if (dictionaryOfContents.ElementAt(h).Key == "Abstract" || dictionaryOfContents.ElementAt(h).Key == "Keywords")
                        {
                            string filteredFirstLine = "";
                            char[] keyInCharacters = dictionaryOfContents.ElementAt(h).Key.ToCharArray();
                            char[] firstLineInChar = listOfAllLines[i].dataItself.ToCharArray();
                            int ii = keyInCharacters.Count() - 1;
                            while (!char.IsUpper(firstLineInChar[ii]) && !char.IsLower(firstLineInChar[ii + 1]))
                            {
                                ii++;
                            }
                            for (int j = ii; j < firstLineInChar.Count(); j++)
                            {
                                filteredFirstLine += firstLineInChar[j];
                            }
                            value += filteredFirstLine;
                        }
                        else
                        {
                            if (dictionaryOfContents.ElementAt(h).Key == "Writer" || dictionaryOfContents.ElementAt(h).Key == "REFERENCES")
                            {
                                value += listOfAllLines[i].dataItself;
                                value += "<br />";
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(listOfAllLines[i].dataItself))
                                {
                                    value += "<br />";
                                }
                                else if (string.IsNullOrWhiteSpace(listOfAllLines[i].dataItself))
                                {
                                    value += "<br />";
                                }
                                value += listOfAllLines[i].dataItself;
                            }

                        }

                    }
                    else
                    {
                        if (dictionaryOfContents.ElementAt(h).Key == "WriterOne" || dictionaryOfContents.ElementAt(h).Key == "WriterTwo" || dictionaryOfContents.ElementAt(h).Key == "REFERENCES")
                        {
                            value += listOfAllLines[i].dataItself;
                            value += "<br />";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(listOfAllLines[i].dataItself))
                            {
                                value += "<br />";
                            }
                            else if (string.IsNullOrWhiteSpace(listOfAllLines[i].dataItself))
                            {
                                value += "<br />";
                            }
                            value += listOfAllLines[i].dataItself;
                        }

                    }



                }
                contents.Add(key, value);
            }

            //gathering of subHeadings portions separate headings
            foreach (var a in dictionaryOfSubHeadings_portions_separateHeadings)
            {
                string value = "";
                string subHeadingkey = a.Key;
                string heading = a.Value.parentHeadingString;
                for (int i = a.Value.SHlineStartNo; i <= a.Value.SHlineEndNo; i++)
                {
                    if (String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself) || String.IsNullOrEmpty(listOfAllLines[i].dataItself))
                    {
                        value += "<br />";
                    }
                    else
                    {
                        value += listOfAllLines[i].dataItself;
                    }

                }
                subheadings_portions_SeparateHeadings.Add(subHeadingkey, new SubHeadingDataModel { parentHeadingKey = heading, childSubheadingValue = value });
            }
            //gathering of subHeadings portions
            foreach (var a in dictionaryOfSubHeadings_portions)
            {
                string value = "";
                string subHeadingkey = a.Key;
                string heading = a.Value.parentHeadingString;
                for (int i = a.Value.SHlineStartNo; i <= a.Value.SHlineEndNo; i++)
                {
                    if (String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself) || String.IsNullOrEmpty(listOfAllLines[i].dataItself))
                    {
                        value += "<br />";
                    }
                    else
                    {
                        if(subHeadingPortion_separateHeading_Index.Contains(i) || subHeadingPortion_part_Index.Contains(i))
                        {
                            value += "<br />";
                            value += listOfAllLines[i].dataItself;
                            value += "<br />";
                        }
                        else
                        {
                            value += listOfAllLines[i].dataItself;
                        }
                        
                    }

                }
                subheadings_portions.Add(subHeadingkey, new SubHeadingDataModel { parentHeadingKey = heading, childSubheadingValue = value });
            }

            //gathering of subHeadings
            foreach (var a in dictionaryOfSubHeadings)
            {
                string value = "";
                string subHeadingkey = a.Key;
                string heading = a.Value.parentHeadingString;
                for (int i = a.Value.SHlineStartNo; i <= a.Value.SHlineEndNo; i++)
                {
                    if (String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself) || String.IsNullOrEmpty(listOfAllLines[i].dataItself))
                    {
                        value += "<br />";
                    }
                    else
                    {
                        if (subHeadingPortion_separateHeading_Index.Contains(i) || subHeadingPortion_part_Index.Contains(i) || subHeading_Portion_Index.Contains(i))
                        {
                            value += "<br />";
                            value += listOfAllLines[i].dataItself;
                            value += "<br />";
                        }
                        else
                        {
                            value += listOfAllLines[i].dataItself;
                        }
                    }

                }
                subheadings.Add(subHeadingkey, new SubHeadingDataModel { parentHeadingKey = heading, childSubheadingValue = value });
            }

            //gathering of Headings
            foreach (var a in dictionaryOfHeadings)
            {
                string value = "";
                string heading = a.Key;
                for (int i = a.Value.lineStartNo; i <= a.Value.lineEndNo; i++)
                {
                    if (String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself) || String.IsNullOrEmpty(listOfAllLines[i].dataItself))
                    {
                        value += "<br />";
                    }

                    else
                    {
                        /*if (subHeadingIndex.Contains(i))
                        {
                            value += "<br />";
                        }
                        value += listOfAllLines[i].dataItself;
                        */
                        if (subHeadingPortion_separateHeading_Index.Contains(i) || subHeadingPortion_part_Index.Contains(i) || subHeading_Portion_Index.Contains(i) || subHeadingIndex.Contains(i))
                        {
                            value += "<br />";
                            value += listOfAllLines[i].dataItself;
                            value += "<br />";
                        }
                        else
                        {
                            value += listOfAllLines[i].dataItself;
                        }
                    }

                }
                headings.Add(heading, value);
            }

            List<Object> list = new List<object>();
            list.Add(contents);
            list.Add(subheadings_portions_SeparateHeadings);
            list.Add(subheadings_portions);
            list.Add(subheadings);
            list.Add(headings);
            return list;






        }
    }
}