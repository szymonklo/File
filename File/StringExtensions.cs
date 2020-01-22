using System;
using System.Collections.Generic;
using System.Text;

namespace File
{
    static class StringExtensions
    {
        public static (string, int) FindStringBetweenDelimiters (this string str, int startIndex, string delimiter1, string delimiter2)
        {
            int indexOfStringBeginning;
            int indexOfStringEnd;
            if (str.IndexOf(delimiter1, startIndex) != -1)
                indexOfStringBeginning = str.IndexOf(delimiter1, startIndex) + delimiter1.Length;
            else
                return (string.Empty, -1);
            if (str.IndexOf(delimiter2, indexOfStringBeginning) != -1)
                indexOfStringEnd = str.IndexOf(delimiter2, indexOfStringBeginning);
            else
                return (string.Empty, -1);
            return (str.Substring(indexOfStringBeginning, indexOfStringEnd - indexOfStringBeginning), indexOfStringEnd + delimiter2.Length);
        }


    }
}