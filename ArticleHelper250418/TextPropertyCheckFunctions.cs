using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class TextPropertyCheckFunctions
    {

        public bool CapitalCheckFunction(char[] m)
        {
            bool capitalCheck = false;
            for (int x = 0; x < m.Length; x++)
            {
                if (!char.IsUpper(m[x]))
                {
                    //if (m[x] != 32)
                    //{
                    capitalCheck = false;
                    return capitalCheck;
                   // }
                   // else
                  //  {
                        capitalCheck = true;
                  //  }

                }
                else
                {
                    
                    capitalCheck = true;
                }
            }

            return capitalCheck;
        }

        public bool NumberingFullStopCheckFunction(char[] m)
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
        public bool NoFullStopCheckFunction(char[] m)
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
                return true;
            }
            else
            {
                return false;
            }
           
            
        }
        public bool twoFullStopCheck(char[] m)
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


            if (numberOfFullStops == 2)
            {
                return true;
            }
            else if (numberOfFullStops <= 1 || numberOfFullStops == 0 || numberOfFullStops > 2)
            {
                return false;
            }

            return false;
        }

        public bool NumberingCharacterCheckFunctionForNumbers(int fullStopIndex, char[] textArray)//onlu for 1.INRODUCTION 2.SOMETHING etc.
        {
            for (int h = 0; h < fullStopIndex; h++)
            {
                //if (textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !(48 <= textArray[h]) && !(textArray[h] <= 57))
                if (!(48 <= textArray[h] && textArray[h] <= 57))
                {
                    return false;
                }
            }
            return true;
        }
        public bool NumberingCharacterCheckFunction(int fullStopIndex, char[] textArray)//if the numbering are X, I, V, x, v, i, 0-9
        {
            for (int h = 0; h < fullStopIndex; h++)
            {
                //if (textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !(48 <= textArray[h]) && !(textArray[h] <= 57))
                if(textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !( 48 <= textArray[h] && textArray[h] <= 57 ) )
                {//not X, I, V, x, v, i, 0-9
                    return false;
                }
            }
            return true;
        }
        public bool HeadingNumberingCharacterCheckFunctionForCriteriaThreeClass(int fullStopIndex, char[] textArray)//if the numbering are X, I, V, x, v, i, 0-9
        {
            for (int h = 0; h < fullStopIndex; h++)
            {
                //if (textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !(48 <= textArray[h]) && !(textArray[h] <= 57))
                if (textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86)
                {//not X, I, V,// x, v, i, 0-9
                    return false;
                }
            }
            return true;
        }
        public bool NumberingCharacterCheckFunctionForSubheadings(int fullStopIndex, char[] textArray)//if the numbering are A-Z
        {
            for (int h = 0; h < fullStopIndex; h++)
            {
                //textArray[h] != 88 && textArray[h] != 73 && textArray[h] != 86 && textArray[h] != 120 && textArray[h] != 118 && textArray[h] != 105 && !(48 <= textArray[h]) && !(textArray[h] <= 57)
                if (textArray[h] >= 65 && textArray[h] <= 90)
                {
                    return true;
                }
            }
            return false;
        }

        public int FindDotNumberInItalicLines(char[] lineInChar)
        {

            int fullStopsNumber = 0;
            for (int i = 0; i < lineInChar.Count(); i++)
            {
                if (lineInChar[i] == 46)
                {
                    fullStopsNumber++;
                }
            }
            return fullStopsNumber;
        }


        public int FindSingleFullstopIndex(char[] lineInChar)
        {
            for (int i = 0; i < lineInChar.Count(); i++)
            {
                if (lineInChar[i] == 46)
                {
                    return i;
                }

            }
            return -1;

        }
        public int FindSingleCloseParenthesisIndex(char[] lineInChar)
        {
            for (int i = 0; i < lineInChar.Count(); i++)
            {
                if (lineInChar[i] == 41)
                {
                    return i;
                }

            }
            return -1;

        }

        public bool CheckFullStopAroundChar(char[] lineInChar)//to check " number.letter"
        {
            int fullstopIndex = FindSingleFullstopIndex(lineInChar);
            if (fullstopIndex != 0)
            {
                try
                {
                    if (char.IsLetterOrDigit(lineInChar[fullstopIndex - 1]) && char.IsLetter(lineInChar[fullstopIndex + 1])) //new change && char.IsLetter(lineInChar[fullstopIndex+1]) at 4.29am 2/4/18
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return false;
        }

        public bool CheckLastCharacterOfaString(char[] lineInCharArray)
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
        public bool CheckLastCharacterOfaStringForCriteriaTwo(char[] lineInCharArray)
        {
            if (char.IsLetterOrDigit(lineInCharArray[lineInCharArray.Count() - 1]))
            {
                return true;
            }
            else if (lineInCharArray[lineInCharArray.Count() - 1] == 41)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfSecondCharIsBracket(char[] lineInCharArray)
        {

            if (lineInCharArray[1] == 41)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckFirstDotIndexAroundForDoubleDotLine(char[] lineInCharArray, int firstDotIndex)// to check "number.number"
        {
            if (char.IsNumber(lineInCharArray[firstDotIndex - 1]) && char.IsNumber(lineInCharArray[firstDotIndex + 1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSecondDotIndexAroundForDoubleDotLine(char[] lineInCharArray, int secondDotIndex)//check "number.letter"
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

        public int FindSecondDotIndexInDoubleDotLine(char[] lineInCharArray, int firstDotIndex)
        {
            for (int i = firstDotIndex + 1; i < lineInCharArray.Count(); i++)
            {
                if (lineInCharArray[i] == 46)
                {
                    return i;
                }

            }
            return 0;
        }

        public string CutOutNumberingPortion(int fullStopIndex, char[] originalDataInArray)
        {
            string finalHeadingString = "";
            for (int o = (fullStopIndex + 1); o < originalDataInArray.Length; o++)
            {
                finalHeadingString += originalDataInArray[o];
            }
            //char[] finalStringArray = finalString.ToCharArray();
            return finalHeadingString;
        }

        public bool CheckCharactersForAuthorsName(char[] name)
        {
            //bool status = false;
            for (int x = 0; x < name.Length; x++)
            {
                if( (name[x] >= 65 && name[x] <= 90) || (name[x] >= 97 && name[x] <= 122) || (name[x] == 97) || (name[x] == 32) )//A-Z //a-z // a //fullstop
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}