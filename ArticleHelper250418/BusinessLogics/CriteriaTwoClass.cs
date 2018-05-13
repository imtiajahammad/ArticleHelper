using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class CriteriaTwoClass
    {
        public List<Object> ExtractFileContent(List<DataModel> listOfAllLines)
        {
            TextPropertyCheckFunctions aTextPropertyCheckFunctions = new TextPropertyCheckFunctions();

            const string generalFontName = "Times New Roman";
            const double generalFontSize = 8.89;

            const double titleFontSize = 12.94;

            const double aboutAthorNameFontSize = 9.66;
            const double aboutAthorEmailFontSize = 8.11;
            const double aboutRestFontSize = 8.89;

            const double abstractFontSize = 8.11;
            const string abstractFontStyle = "Italic";

            const double keywordsFontSize = 8.11;
            const string keywordsFontStyle = "Italic";

            const double headingFontSize = 9.66;
            const string headingFontStyle = "Bold";

            const double subHeadingFontSize = 8.89;
            const string subHeadingFontStyle = "Bold";

            const double acknowledgementFontSize = 8.89;
            const string acknowledgementFontStyle = "Bold";

            const double referenceFontSize = 9.65;
            const string referenceFontStyle = "Bold";
            const double referenceContentFontSize = 8.89;

            const double authorProfileFontSize = 9.65;
            const string authorProfileFontStyle = "Bold";
            const double authorProfileContentFontSize = 8.89;






            List<int> titleIndex = new List<int>();
            List<int> authorNameIndex = new List<int>();//-
            List<int> authorDetailsIndex = new List<int>();//-
            List<int> abstractIndex = new List<int>();
            List<int> keywordIndex = new List<int>();
            List<int> headingIndex = new List<int>();
            List<int> subHeadingIndex = new List<int>();
            List<int> acknowledgementIndex = new List<int>();
            List<int> referenceIndex = new List<int>();
            List<int> referenceContentIndex = new List<int>();
            List<int> authorProfileIndex = new List<int>();
            List<int> conclusionIndex = new List<int>();



            List<HeadingStructureModel> listOfHeadings = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadings = new List<HeadingStructureModel>();

            for (int i = 0; i < listOfAllLines.Count; i++)
            {
                if (i == 9 || i == 14 || i == 947)
                {

                }
                double ii = System.Math.Round(listOfAllLines[i].fontSize, 2);
                if (!String.IsNullOrWhiteSpace(listOfAllLines[i].dataItself))//not to operate on null or white spaces
                {
                    if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(titleFontSize))
                    {
                        titleIndex.Add(i);
                    }

                    //find abstract and keywords
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(keywordsFontSize) && listOfAllLines[i].fontName.Contains(keywordsFontStyle) && !listOfAllLines[i].fontName.Contains("Bold"))
                    {
                        if (listOfAllLines[i].dataItself.Contains("Abstract"))
                        {
                            abstractIndex.Add(i);
                        }
                        else if(listOfAllLines[i].dataItself.Contains("Keywords"))
                        {
                            keywordIndex.Add(i);
                        }

                        
                    }

                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(headingFontSize) && listOfAllLines[i].fontName.Contains(headingFontStyle))
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
                            if (oneFullStopCheck)
                            {
                                int fullStopIndex = aTextPropertyCheckFunctions.FindSingleFullstopIndex(lineInCharArray);//to find out the full stop index
                                if (fullStopIndex > 0)//fullstop index should be after the numbering, so zero not possible
                                {
                                    bool numberingCharactersCheck = aTextPropertyCheckFunctions.NumberingCharacterCheckFunctionForNumbers(fullStopIndex, lineInCharArray);//to check the numbering characters are  0-9
                                    if (numberingCharactersCheck)//for headings
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
                                    else//added new at 1.05.18 @10.44pm
                                    {
                                        authorNameIndex.Add(i);
                                    }
                                }
                            }
                            else
                            {
                                //for acknowledgement,  reference and authorsProfile
                                bool capitalCheckForRestHeadings = aTextPropertyCheckFunctions.CapitalCheckFunction(lineInCharArray);
                                if (capitalCheckForRestHeadings)
                                {
                                    if (String.Equals(originalData, "ACKNOWLEDGMENT ", StringComparison.OrdinalIgnoreCase))
                                    {
                                        //headingIndex.Add(i);
                                        //listOfHeadings.Add(new HeadingStructureModel { headingString = originalData.Replace(" ",String.Empty), lineNo = i });
                                        acknowledgementIndex.Add(i);
                                    }
                                    else if (String.Equals(originalData, "REFERENCES ", StringComparison.OrdinalIgnoreCase))
                                    {
                                        referenceIndex.Add(i);
                                    }
                                    else if (String.Equals(originalData, "AUTHORS BIBLIOGRAPHY ", StringComparison.OrdinalIgnoreCase))
                                    {
                                        authorProfileIndex.Add(i);
                                    }

                                }
                                else
                                {
                                        authorNameIndex.Add(i);//if problem creates, check for no fullstop
                                }


                            }
                        }
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(subHeadingFontSize) && listOfAllLines[i].fontName.Contains(subHeadingFontStyle) &&  !listOfAllLines[i].fontName.Contains("Italic"))
                    {
                        // for sub headings, copied only, modify needed

                        string originalData = listOfAllLines[i].dataItself;
                        char[] originalDataInCharArray = originalData.ToCharArray();//convert into char array

                        string lineText = listOfAllLines[i].dataItself.Replace(" ", String.Empty);//space reduced
                        char[] lineInCharArray = lineText.ToCharArray();//convert into char array

                        bool lastCharacterCheck = aTextPropertyCheckFunctions.CheckLastCharacterOfaStringForCriteriaTwo(lineInCharArray);// to check if the string ends with a fullStop or not
                        if (lastCharacterCheck)
                        {
                            bool twoFullStopCheck = aTextPropertyCheckFunctions.twoFullStopCheck(lineInCharArray);//heading has one fullstop, after its numbering characters. so checking if one or more
                            if (twoFullStopCheck)
                            {
                                int firstFullStopIndex = aTextPropertyCheckFunctions.FindSingleFullstopIndex(lineInCharArray);//to find out the first full stop index
                                if (firstFullStopIndex > 0)//fullstop index should be after the numbering, so zero not possible
                                {
                                    bool firstFullStopCharactersAroundCheck = aTextPropertyCheckFunctions.CheckFirstDotIndexAroundForDoubleDotLine(lineInCharArray, firstFullStopIndex);
                                    if (firstFullStopCharactersAroundCheck)
                                    {
                                        int secondFullStopIndex = aTextPropertyCheckFunctions.FindSecondDotIndexInDoubleDotLine(lineInCharArray, firstFullStopIndex);
                                        if (secondFullStopIndex > firstFullStopIndex)
                                        {
                                            bool secondFullStopCharacterAroundCheck = aTextPropertyCheckFunctions.CheckSecondDotIndexAroundForDoubleDotLine(lineInCharArray, secondFullStopIndex);
                                            if (secondFullStopCharacterAroundCheck)
                                            {
                                                string finalHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(secondFullStopIndex, originalDataInCharArray);//cut the numbering portion, get the heading
                                                char[] finalStringInCharArray = finalHeadingString.Replace(" ", String.Empty).ToCharArray();//original string without white spaces and convert that to array char
                                                bool capitalCheck = aTextPropertyCheckFunctions.CapitalCheckFunction(finalStringInCharArray);//check if there is any string capital or not
                                                if (!capitalCheck)
                                                {
                                                    subHeadingIndex.Add(i);
                                                    listOfSubHeadings.Add(new HeadingStructureModel { headingString = finalHeadingString, lineNo = i });
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(subHeadingFontSize) && listOfAllLines[i].fontName.Contains(subHeadingFontStyle))
                    {
                        if (!(listOfAllLines[i].dataItself.ToUpper().Contains("TABLE")))
                        {
                            bool capitalCheckForAuthorsProfile = aTextPropertyCheckFunctions.CapitalCheckFunction(listOfAllLines[i].dataItself.Replace(" ", String.Empty).ToArray()); // Replace(" ", String.Empty).ToArray());//Replace(" ", String.Empty)
                            if (capitalCheckForAuthorsProfile)
                            {
                                if (String.Equals(listOfAllLines[i].dataItself, "AUTHORS PROFILE ", StringComparison.OrdinalIgnoreCase))
                                {
                                    authorProfileIndex.Add(i);
                                }

                            }
                        }
                    }
                }
            }

            //do patching
            Dictionary<string, LineStructureModel> dictionaryOfContents = new Dictionary<string, LineStructureModel>();
            Dictionary<string, LineStructureModel> dictionaryOfHeadings = new Dictionary<string, LineStructureModel>();
            Dictionary<string, LineStructureModelForSubHeadings> dictionaryOfSubHeadings = new Dictionary<string, LineStructureModelForSubHeadings>();

            dictionaryOfContents.Add("Title", new LineStructureModel { lineStartNo = titleIndex.First(), lineEndNo = titleIndex.Last() });
            dictionaryOfContents.Add("WriterOne", new LineStructureModel { lineStartNo = authorNameIndex.First(), lineEndNo = authorNameIndex.Last() - 1 });
            dictionaryOfContents.Add("WriterTwo", new LineStructureModel { lineStartNo = authorNameIndex.Last(), lineEndNo = (abstractIndex.First() - 1) });
            dictionaryOfContents.Add("Abstract", new LineStructureModel { lineStartNo = abstractIndex.First(), lineEndNo = keywordIndex.First()-1 });
            dictionaryOfContents.Add("Keywords", new LineStructureModel { lineStartNo = keywordIndex.First(), lineEndNo = headingIndex.First()-1 });
            dictionaryOfContents.Add("ACKNOWLEDGMENT", new LineStructureModel { lineStartNo = acknowledgementIndex.First() + 1, lineEndNo = referenceIndex.First() - 1 });
            dictionaryOfContents.Add("References", new LineStructureModel { lineStartNo = referenceIndex.First() + 1, lineEndNo = (authorProfileIndex.First() - 1) });
            dictionaryOfContents.Add("AUTHORS PROFILE", new LineStructureModel { lineStartNo = authorProfileIndex.First() + 1, lineEndNo = listOfAllLines.Count() - 1 });

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

            //make a class model and separate things from paper
            Dictionary<string, string> contents = new Dictionary<string, string>();
            Dictionary<string, string> headings = new Dictionary<string, string>();
            Dictionary<string, SubHeadingDataModel> subheadings = new Dictionary<string, SubHeadingDataModel>();


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
                            if (dictionaryOfContents.ElementAt(h).Key == "WriterOne" || dictionaryOfContents.ElementAt(h).Key == "WriterTwo" || dictionaryOfContents.ElementAt(h).Key == "References")
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
                        if (dictionaryOfContents.ElementAt(h).Key == "WriterOne" || dictionaryOfContents.ElementAt(h).Key == "WriterTwo" || dictionaryOfContents.ElementAt(h).Key == "References")
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
                        value += listOfAllLines[i].dataItself;
                    }

                }
                subheadings.Add(subHeadingkey, new SubHeadingDataModel { parentHeadingKey = heading, childSubheadingValue = value });
            }
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
                        if (subHeadingIndex.Contains(i))
                        {
                            value += "<br />";
                        }
                        value += listOfAllLines[i].dataItself;
                    }

                }
                headings.Add(heading, value);
            }

            List<Object> list = new List<object>();
            list.Add(contents);
            list.Add(subheadings);
            list.Add(headings);
            return list;





        }
    }
}