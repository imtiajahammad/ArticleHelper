using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class CriteriaOneClass
    {
        public List<Object> ExtractFileContent(List<DataModel> listOfAllLines)
        {
            TextPropertyCheckFunctions aTextPropertyCheckFunctions = new TextPropertyCheckFunctions();

            const string generalFontName = "Times New Roman";
            const double generalFontSize = 8.06;

            const double titleFontSize= 19.32;

            const double aboutAthorNameFontSize = 8.84;
            const double aboutRestFontSize = 8.06;

            const double abstractFontSize = 7.24;
            const string abstractFontStyle= "Bold";

            const double keywordsFontSize = 7.24;
            const string keywordsFontStyle= "BoldItalic";

            const double headingFontSize = 8.06;
            
            const double subHeadingFontSize = 8.06;
            const string subHeadingFontStyle = "Italic";

            const double referenceFontSize = 8.06;
            const double referenceContentFontSize = 6.42;

            const double authorProfileFontSize = 6.42;
            const double authorProfileContentFontSize = 6.42;





            List<int> titleIndex = new List<int>();
            List<int> authorNameIndex = new List<int>();
            List<int> authorDetailsIndex = new List<int>();
            List<int> abstractIndex = new List<int>();
            List<int> keywordIndex = new List<int>();
            List<int> headingIndex = new List<int>();
            List<int> subHeadingIndex = new List<int>();
            List<int> referenceIndex = new List<int>();
            List<int> referenceContentIndex = new List<int>();
            List<int> authorProfileIndex = new List<int>();
            List<int> conclusionIndex = new List<int>();



            List<HeadingStructureModel> listOfHeadings = new List<HeadingStructureModel>();
            List<HeadingStructureModel> listOfSubHeadings = new List<HeadingStructureModel>();

            for (int i=0;i<listOfAllLines.Count;i++)
            {
                if( i == 885 || i == 879 || i == 947)
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
                    
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(keywordsFontSize) && listOfAllLines[i].fontName.Contains(keywordsFontStyle))
                    {
                        keywordIndex.Add(i);
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(abstractFontSize) && listOfAllLines[i].fontName.Contains(abstractFontStyle) && !listOfAllLines[i].fontName.Contains("Italic"))
                    {
                        abstractIndex.Add(i);
                    }
                  /*  else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(subHeadingFontSize) && listOfAllLines[i].fontName.Contains(subHeadingFontStyle) && !listOfAllLines[i].fontName.Contains("Bold"))
                    {
                        subHeadingIndex.Add(i);
                    }*/
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(headingFontSize))
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
                                    bool numberingCharactersCheck = aTextPropertyCheckFunctions.NumberingCharacterCheckFunction(fullStopIndex, lineInCharArray);//to check the numbering characters are I V X 0-9
                                    if (numberingCharactersCheck)//for headings
                                    {
                                        string finalHeadingString = aTextPropertyCheckFunctions.CutOutNumberingPortion(fullStopIndex, originalDataInCharArray);//cut the numbering portion, get the heading
                                        char[] finalStringInCharArray = finalHeadingString.Replace(" ",String.Empty).ToCharArray();//original string without white spaces and convert that to array char
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
                            {
                                //for conclusion and reference
                                bool capitalCheckForRestHeadings = aTextPropertyCheckFunctions.CapitalCheckFunction(lineInCharArray);
                                if (capitalCheckForRestHeadings)
                                {//root.Equals(root2)
                                 //bool result = root.Equals(root2, StringComparison.OrdinalIgnoreCase);
                                    //bool areEqual = String.Equals(root, root2, StringComparison.OrdinalIgnoreCase);
                                    //int comparison = String.Compare(root, root2, ignoreCase: true);

                                    if (String.Equals(originalData,"CONCLUSION ", StringComparison.OrdinalIgnoreCase     ))
                                    {
                                        //headingIndex.Add(i);
                                        //listOfHeadings.Add(new HeadingStructureModel { headingString = originalData.Replace(" ",String.Empty), lineNo = i });
                                        conclusionIndex.Add(i);
                                    }
                                    else if (String.Equals(originalData, "REFERENCES ", StringComparison.OrdinalIgnoreCase ))
                                    {
                                        referenceIndex.Add(i);
                                    }
                                }


                            }
                        }
                    }
                    else if (System.Math.Round(listOfAllLines[i].fontSize, 2).Equals(referenceContentFontSize))
                    {
                        if (!(listOfAllLines[i].dataItself.ToUpper().Contains("TABLE") || listOfAllLines[i].dataItself.ToUpper().Contains("FIGURE")))
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
            dictionaryOfContents.Add("WriterOne", new LineStructureModel { lineStartNo = authorNameIndex.First(), lineEndNo = authorNameIndex.Last()-1});
            dictionaryOfContents.Add("WriterTwo", new LineStructureModel { lineStartNo = authorNameIndex.Last(), lineEndNo = (abstractIndex.First()-1) });
            dictionaryOfContents.Add("Abstract", new LineStructureModel { lineStartNo = abstractIndex.First(), lineEndNo = abstractIndex.Last() });
            dictionaryOfContents.Add("Keywords", new LineStructureModel { lineStartNo = keywordIndex.First(), lineEndNo = keywordIndex.Last() });
            dictionaryOfContents.Add("Conclusion", new LineStructureModel { lineStartNo =conclusionIndex[0]+1,lineEndNo=referenceIndex[0]-1});
            dictionaryOfContents.Add("References", new LineStructureModel { lineStartNo = referenceIndex.First()+1, lineEndNo = (authorProfileIndex.First() - 1) });
            dictionaryOfContents.Add("AUTHORS PROFILE", new LineStructureModel { lineStartNo = authorProfileIndex.First() + 1, lineEndNo = listOfAllLines.Count()-1 });

            //headings
            for (int u=0;u<listOfHeadings.Count();u++)
            {
                if (u == (listOfHeadings.Count()-1))
                {
                    dictionaryOfHeadings.Add(listOfHeadings[u].headingString, new LineStructureModel { lineStartNo = listOfHeadings[u].lineNo+1,lineEndNo=(conclusionIndex.First()-1) });
                }
                else
                {
                    dictionaryOfHeadings.Add(listOfHeadings[u].headingString, new LineStructureModel { lineStartNo = listOfHeadings[u].lineNo+1,lineEndNo=listOfHeadings[u+1].lineNo-1 });
                }
                
            }

            //subheadings

            foreach(var a in dictionaryOfHeadings)
            {
                for(int i = 0; i < listOfSubHeadings.Count(); i++)
                {
                    if (a.Value.lineStartNo < listOfSubHeadings[i].lineNo && a.Value.lineEndNo > listOfSubHeadings[i].lineNo)
                    {
                        
                        try
                        {
                            if (a.Value.lineEndNo > listOfSubHeadings[i + 1].lineNo)
                            {
                                dictionaryOfSubHeadings.Add(listOfSubHeadings[i].headingString, new LineStructureModelForSubHeadings {parentHeadingString=a.Key, SHlineStartNo = listOfSubHeadings[i].lineNo + 1, SHlineEndNo = listOfSubHeadings[i + 1].lineNo - 1 });
                            }
                            else
                            {
                                dictionaryOfSubHeadings.Add(listOfSubHeadings[i].headingString, new LineStructureModelForSubHeadings { parentHeadingString = a.Key, SHlineStartNo = listOfSubHeadings[i].lineNo + 1, SHlineEndNo = a.Value.lineEndNo });
                            }
                        }
                        catch(Exception e)
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
            for(int h=0;h<dictionaryOfContents.Count();h++)
            {
                string key = dictionaryOfContents.ElementAt(h).Key;
                string value = "";
                for (int i = dictionaryOfContents.ElementAt(h).Value.lineStartNo; i <= dictionaryOfContents.ElementAt(h).Value.lineEndNo; i++)
                {
                    if(i== dictionaryOfContents.ElementAt(h).Value.lineStartNo)
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
                        if(dictionaryOfContents.ElementAt(h).Key == "WriterOne" || dictionaryOfContents.ElementAt(h).Key == "WriterTwo" || dictionaryOfContents.ElementAt(h).Key == "References")
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
            foreach(var a in dictionaryOfSubHeadings)
            {
                string value = "";
                string subHeadingkey = a.Key;
                string heading = a.Value.parentHeadingString;
                for(int i=a.Value.SHlineStartNo; i<=a.Value.SHlineEndNo; i++)
                {
                    if( String.IsNullOrWhiteSpace( listOfAllLines[i].dataItself) || String.IsNullOrEmpty(listOfAllLines[i].dataItself) )
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