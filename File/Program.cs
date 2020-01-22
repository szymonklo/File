using System;
using System.Collections.Generic;
using System.IO;

namespace File
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prepring Opening
            string path = @"C:\Users\Szymon\Documents\GPX\P1\";
            string fileName = "p3";
            string ext = ".gpx";
            
            //Opening
            string fileContent = GPXReader.Open(path, fileName, ext);
            var pointList = new List<Point>();

            //Reading
            DateTime start = DateTime.Now;
            GPXReader.Read(fileContent, ref pointList);
            TimeSpan dur = start - DateTime.Now;
            Console.WriteLine($"Reading: {dur}");

            //Processing
            start = DateTime.Now;
            GPXProcessor.CalculateGPX(ref pointList);
            dur = start - DateTime.Now;
            Console.WriteLine($"Processing: {dur}");

            //Processing - dayList
            var dayList = GPXProcessor.DayList(pointList);

            //Formating - trkp
            start = DateTime.Now;
            var newContent = GPXCreator.FormatGPXtrkp(pointList);

            //Formating - wpt
            var newContentWPT = GPXCreator.FormatGPXwpt(dayList);

            //Saving - trkp
            string newFileName = fileName + "_new";
            GPXCreator.Save(path, newFileName, ext, newContent);

            //Saving - wpt
            string newFileNameWPT = newFileName + "WPT";
            GPXCreator.Save(path, newFileNameWPT, ext, newContentWPT);
            dur = start - DateTime.Now;
            Console.WriteLine($"Formating and Saving: {dur}");
        }
    }
}