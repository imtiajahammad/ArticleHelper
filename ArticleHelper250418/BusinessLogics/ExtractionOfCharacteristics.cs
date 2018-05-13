using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class ExtractionOfCharacteristics
    {
        //List<DataModel> f2 = new List<DataModel>();

        double lastH = 0, curH;
        string lastTitle = "", lastFont = "", curTitle = "", curFont = "";

        Dictionary<int, string> titleListingDictionary = new Dictionary<int, string>();
        
        List<ListModel> listOfItalicLines= new List<ListModel>();
        List<ListModel> mainHeadingsList = new List<ListModel>();
        List<ListModel> subHeadingList = new List<ListModel>();
        List<ListModel> separateParaHeadingsList = new List<ListModel>();
        List<ListModel> restOfHeadingsList = new List<ListModel>();

        public ArticleModel Method(List<DataModel> f)
        {

            List<DataModel> fomedList = new List<DataModel>();
            //Boolean abstractCheck, keywordsCheck, introductionCheck, relatedWorkCheck;


            
            //foreach (DataModel s in f)
            for (int i = 0; i < f.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(f[i].dataItself))
                {

                }
                else
                {
                    TitleFinding(f[i]);
                    FilteringParagraph(f[i], i);

                }
                 
            }
            int introductionLineNumberInList = mainHeadingsList[0].lineNo;
                                                                            //        int introductionLineNumberInList = titleListingDictionary.Keys.ElementAt(0);
                                                                                        //int lastTitleLiveInList = titleListingDictionary.Last().Key;
            int keywordsLineNuberInList = FindKeywordsLineStarting(introductionLineNumberInList, f);//we have already found some headings and their line no. here we are sending the first heading no to find out the rest above of them
            //Dictionary<int,string> tt= titleListingDictionary.OrderByDescending(i => i.Key);
            mainHeadingsList.Reverse();
            mainHeadingsList.Add(new ListModel { lineNo = keywordsLineNuberInList, title = "Keywords" });
            //        titleListingDictionary.Add(keywordsLineNuberInList, "Keywords");


            int abstractLineNuberInList = FindAbstractLineStarting(keywordsLineNuberInList, f);

            mainHeadingsList.Add(new ListModel { lineNo = abstractLineNuberInList, title = "Abstract" });

            mainHeadingsList.Add(new ListModel { lineNo = 0, title = "About" });
            mainHeadingsList.Reverse();


            int lastTopicBeforeReferenceLineNumber = mainHeadingsList.Last().lineNo;
            int referenceLineNumberInList = FindReferenceLineStarting(lastTopicBeforeReferenceLineNumber, f);

            mainHeadingsList.Add(new ListModel { lineNo = referenceLineNumberInList, title = "References" });


            mainHeadingsList.Add(new ListModel { lineNo = (f.Count - 1), title = "End" });
            //titleListingDictionary.Add(abstractLineNuberInList, "Abstract");

            

            for (int z = 1; z < mainHeadingsList.Count; z++)
            {
                if (mainHeadingsList[z - 1].lineNo == mainHeadingsList[z].lineNo)
                {
                    mainHeadingsList.Remove(mainHeadingsList[z]);
                }
            }
            //IF TWO LEBEL GOT SAME LINE NUMBER , REMOVE ONE
            //TO CHECK THE PREVIOUS CHARACTERS FOR XIV OR 0-9


            //for finding all the headings except mainHeadings
            //for(int i = mainHeadingsList[3].lineNo; i < mainHeadingsList[mainHeadingsList.Count()-2].lineNo; i++)
            for(int i=0;i<listOfItalicLines.Count();i++)
            {
                if(listOfItalicLines[i].lineNo>mainHeadingsList[3].lineNo && listOfItalicLines[i].lineNo < mainHeadingsList[mainHeadingsList.Count() - 2].lineNo)
                {
                    string lineText = listOfItalicLines[i].title.Replace(" ", String.Empty);//space reduced
                    char[] lineInCharArray = lineText.ToCharArray();
                    int howManyDots = FindDotNumberInItalicLines(lineInCharArray);
                    if (howManyDots == 1)
                    {
                        bool checkCharAroundDot = CheckFullStopAroundChar(lineInCharArray);
                        if (checkCharAroundDot)
                        {
                            bool checkLastCharacter = CheckLastCharacterOfaString(lineInCharArray);
                            if (checkLastCharacter)
                            {
                                //is a sub heading
                                subHeadingList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                            }
                            else
                            {
                                //others
                                restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                            }

                        }
                        else
                        {
                            restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                        }
                    }
                    else if (howManyDots == 2)
                    {
                        //need to check for "3.1.Data Source"
                        int firstDotIndex = FindSingleFullstopIndex(lineInCharArray);//this method is also working for finding first fullstop index
                        int secondDotIndex = FindSecondDotIndesInDoubleDotLine(lineInCharArray, firstDotIndex);
                        if (firstDotIndex != 0 && secondDotIndex != 0)
                        {
                            bool checkFirstDotIndexAroundForDoubleDotLine = CheckFirstDotIndexAroundForDoubleDotLine(lineInCharArray, firstDotIndex);
                            if (checkFirstDotIndexAroundForDoubleDotLine)
                            {
                                bool checkSecondDotIndexAroundForDoubleDotLine = CheckSecondDotIndexAroundForDoubleDotLine(lineInCharArray, firstDotIndex);
                                if (checkSecondDotIndexAroundForDoubleDotLine)
                                {
                                    //is a sub heading
                                    subHeadingList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });

                                }
                                else
                                {
                                    //others
                                    restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                                }
                            }
                            else
                            {
                                //others
                                restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                            }
                        }
                    }
                    else if (howManyDots == 0)
                    {
                        if (lineInCharArray.Count() > 2)
                        {
                            //need to check for "1)Dicision Tree Induction"
                            bool checkSecondCharBracket = CheckIfSecondCharIsBracket(lineInCharArray);
                            if (checkSecondCharBracket)
                            {
                                bool checkLastCharacter = CheckLastCharacterOfaString(lineInCharArray);
                                if (checkLastCharacter)
                                {
                                    //is separate para heading
                                    separateParaHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                                }
                                else
                                {
                                    //others
                                    restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                                }
                            }

                            else
                            {
                                if (f[listOfItalicLines[i].lineNo].fontName.ToUpper().Contains("BOLD") && f[listOfItalicLines[i].lineNo].fontName.ToUpper().Contains("ITALIC"))
                                {
                                    //is separate para heading
                                    separateParaHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                                }
                                else
                                {
                                    //others
                                    restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                                }
                            }
                        }
                        else
                        {
                            //others
                            restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                        }

                    }
                    else
                    {
                        // is not any of sub heading and so ignore it
                        restOfHeadingsList.Add(new ListModel { title = listOfItalicLines[i].title, lineNo = listOfItalicLines[i].lineNo });
                    }
                }

            }
            
            
            
            //Console.WriteLine(lastTitle);
            //Console.WriteLine(lastFont);
            //Console.WriteLine(lastH);
            Dictionary<string, string> aDictionary = new Dictionary<string, string>();
            for (int a = 0; a < mainHeadingsList.Count - 1; a++)
            {
                string key = mainHeadingsList[a].title;
                string para = "";
                for (int b = mainHeadingsList[a].lineNo; b < mainHeadingsList[a + 1].lineNo; b++)
                {
                    //  if (!(f[b].dataItself.ToUpper().Contains("TABLE") || f[b].dataItself.ToUpper().Contains("FIGURE")))
                    //{
                    if (b == mainHeadingsList[a].lineNo)
                    {
                        if( a == 1 || a==2  )
                        {
                            string filteredFirstLine = "";
                            char[] keyInCharacters = key.ToCharArray();
                            char[] firstLineInCharacters = f[b].dataItself.ToCharArray();
                            int ii = keyInCharacters.Count() - 1;

                            while (!char.IsUpper(firstLineInCharacters[ii]) && !char.IsLower(firstLineInCharacters[ii+1]))//changed firstLineInCharacters[ii]+1
                            {
                                ii++;
                            }
                            for (int i = ii; i < firstLineInCharacters.Count(); i++)
                            {
                                //check if capital or not
                                filteredFirstLine += firstLineInCharacters[i];
                            }
                            para += filteredFirstLine;
                        }
                        

                    }
                    else
                    {
                        if(a==0|| (a == mainHeadingsList.Count() - 2))
                        {
                            para += f[b].dataItself;
                            para += "<br />";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(f[b].dataItself))
                            {
                                para += "<br />";
                            }
                            else if (string.IsNullOrWhiteSpace(f[b].dataItself)       /*f[b].Equals(" ")*/  )
                            {
                                para += "<br />";
                            }

                            para += f[b].dataItself;
                        }
                        
                    }


                    //}
                        
                }
                // Console.WriteLine(key);
                // Console.WriteLine(para);
                aDictionary.Add(key, para);

            }
            ArticleModel aArticleModel = new ArticleModel();
            aArticleModel.articleTitle = lastTitle;
            aArticleModel.articleSeparateParagraphs = aDictionary;
            return aArticleModel;
        }

        private bool CheckFirstDotIndexAroundForDoubleDotLine(char[] lineInCharArray,int firstDotIndex)
        {
            if(char.IsNumber(lineInCharArray[firstDotIndex - 1]) && char.IsNumber(lineInCharArray[firstDotIndex + 1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckSecondDotIndexAroundForDoubleDotLine(char[] lineInCharArray, int secondDotIndex)
        {
            if (char.IsNumber(lineInCharArray[secondDotIndex - 1]) && char.IsLetter(lineInCharArray[secondDotIndex + 1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int FindSecondDotIndesInDoubleDotLine(char[] lineInCharArray, int firstDotIndex)
        {
            for (int i = firstDotIndex+1; i < lineInCharArray.Count(); i++)
            {
                if (lineInCharArray[i] == 46)
                {
                    return i;
                }

            }
            return 0;
        }

        private bool CheckIfSecondCharIsBracket(char[] lineInCharArray)
        {
            
            if( lineInCharArray[1]==41)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckLastCharacterOfaString(char[] lineInCharArray)
        {
            if (char.IsLetterOrDigit(lineInCharArray[lineInCharArray.Count() - 1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckFullStopAroundChar(char[] lineInChar)
        {
            int fullstopIndex = FindSingleFullstopIndex(lineInChar);
            if (fullstopIndex != 0)
            {
                try
                {

                
                if(char.IsLetterOrDigit(lineInChar[fullstopIndex - 1]) && char.IsLetter(lineInChar[fullstopIndex+1])) //new change && char.IsLetter(lineInChar[fullstopIndex+1]) at 4.29am 2/4/18
                {
                    return true;
                }
                else
                {
                    return false;
                }
                }
                catch(Exception e)
                {
                    return false;
                }

            }

            return false;
        }

        private int FindSingleFullstopIndex(char[] lineInChar)
        {
            for(int i = 0; i < lineInChar.Count(); i++)
            {
                if (lineInChar[i] == 46)
                {
                    return i;
                }
                
            }
            return 0;

        }

        private int FindDotNumberInItalicLines(char[] lineInChar)
        {
            
            int fullStopsNumber=0;
            for (int i = 0; i < lineInChar.Count(); i++)
            {
                if (lineInChar[i] == 46)
                {
                    fullStopsNumber++;
                }
            }
            return fullStopsNumber;
        }

        private int FindReferenceLineStarting(int lastTopicBeforeReferenceLineNumber, List<DataModel> f)
        {
            for (int x = lastTopicBeforeReferenceLineNumber; x < f.Count; x++)
            {
                if (f[x].dataItself.ToUpper().Contains("REFERENCES") || f[x].dataItself.ToUpper().Contains("REFERENCE"))
                {
                    return x;
                }
            }
            return 0;
        }

        private int FindKeywordsLineStarting(int introductionLineNumberInList, List<DataModel> f)
        {
            for (int x = 0; x < introductionLineNumberInList; x++)
            {
                if (f[x].dataItself.ToUpper().Contains("KEYWORDS") || f[x].dataItself.ToUpper().Contains("KEYWORD-") || f[x].dataItself.ToUpper().Contains("KEYWORD--"))
                {
                    return x;
                }
            }
            return 0;
        }

        private int FindAbstractLineStarting(int keywordsLineNuberInList, List<DataModel> f)
        {
            for (int x = 0; x < keywordsLineNuberInList; x++)
            {
                //if (f[x].dataItself.Contains("Abstract"))
                if (f[x].dataItself.ToUpper().Contains("ABSTRACT") || f[x].dataItself.ToUpper().Contains("ABSTRACT-") || f[x].dataItself.ToUpper().Contains("ABSTRACT--"))
                {
                    return x;
                }
            }
            return 0;
        }

        private void TitleFinding(DataModel s)
        {
            curH = s.fontSize;
            curTitle = s.dataItself;
            curFont = s.fontName;
            if (lastH != 0)
            {
                if (curH > lastH)
                {
                    lastH = curH;
                    lastTitle = s.dataItself;
                    lastFont = s.fontName;
                }
                else if (curH == lastH)
                {
                    lastH = curH;
                    lastTitle += s.dataItself;
                    lastFont = s.fontName;
                }

            }

            else
            {
                lastH = curH;
                lastTitle = s.dataItself;
                lastFont = s.fontName;
            }

        }

        private void FilteringParagraph(DataModel s, int i)
        {
            
            if (!(s.dataItself.ToUpper().Contains("TABLE") || s.dataItself.ToUpper().Contains("FIGURE")))
            {
                if(s.fontName.Contains("Italic") || s.fontName.Contains("ITALIC"))
                {
                    listOfItalicLines.Add(new ListModel() { lineNo = i, title = s.dataItself });
                }
                string text = s.dataItself;
                string text2 = text.Replace(" ", String.Empty);//space reduced
                char[] textArray = text2.ToCharArray();// converted to array
                char[] textArray2 = text.ToCharArray();
                char lastChar = textArray[textArray.Length - 1];
                //        if (!lastChar.Equals("."))
                if (lastChar != 46)//step 1
                {
                    bool OneFullStopCheck = NumberingFullStopCheckFunction(textArray);
                    if (OneFullStopCheck)//step 2
                    {
                        //string text3 = text2.Replace(".", String.Empty);
                        //find index of fullstop3
                        int fullStopIndex = 0;
                        for (int n = 0; n < textArray2.Length; n++)
                        {
                            if (textArray2[n] == 46)
                            {
                                fullStopIndex = n;
                            }
                        }
                        // bool numberingCharacterCheck = numberingCharacterCheckFunction(fullStopIndex,textArray);



                        //  if (numberingCharacterCheck) //check if there are only X I V  0-9 or not
                        //{
                        string finalString = "";
                        for (int o = (fullStopIndex + 1); o < textArray2.Length; o++)
                        {
                            finalString += textArray2[o];
                        }
                        char[] finalStringArray = finalString.ToCharArray();
                        bool capitalCheck = capitalCheckFunction(finalStringArray);
                        if (capitalCheck)//step 3
                        {
                            bool numberingCharacterCheck = numberingCharacterCheckFunction(fullStopIndex, textArray);
                            if (numberingCharacterCheck) //check if there are only X I V  0-9 or not
                            {



                                ListModel aListModel = new ListModel();
                                aListModel.lineNo = i;
                                aListModel.title = finalString;
                                mainHeadingsList.Add(aListModel);
                               // Console.WriteLine(finalString + "    at no. " + i);
                            }
                        }

                    }
                }

            }

        }

        private bool numberingCharacterCheckFunction(int fullStopIndex, char[] textArray)
        {
            for (int h = 0; h < fullStopIndex; h++)
            {
                if (textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !(48 <= textArray[h]) && !(textArray[h] <= 57))
                {
                    return false;
                }
            }
            return true;
        }

        private bool capitalCheckFunction(char[] m)
        {
            bool capitalCheck = false;
            for (int x = 0; x < m.Length; x++)
            {
                if (!char.IsUpper(m[x]))
                {
                    if (m[x] != 32)
                    {
                        capitalCheck = false;
                        return capitalCheck;
                    }
                    else
                    {
                        capitalCheck = true;
                    }

                }
                else
                {
                    capitalCheck = true;
                }
            }

            return capitalCheck;
        }


        private bool NumberingFullStopCheckFunction(char[] m)
        {
            int numberOfFullStops = 0;
            for (int x = 0; x < m.Length; x++)
            {
                //if (m[x].Equals(46))
                if (m[x] == 46)
                {
                    numberOfFullStops++;
                }
            }


            if (numberOfFullStops == 0)
            {
                return false;
            }
            else if (numberOfFullStops > 1)
            {
                return false;
            }
            else if (numberOfFullStops == 1)
            {
                return true;
            }
            return false;
        }
    }
}