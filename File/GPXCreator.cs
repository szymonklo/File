using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace File
{
    class GPXCreator
    {
        public static string FormatGPXtrkp(List<Point> l)
        {
            System.Text.StringBuilder newContent = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF - 8\"?><gpx creator=\"File\" version=\"1.0\"><trk><trkseg>");

            foreach (Point p in l)
            {
                newContent.Append("<trkpt");//.Append(" lat=\"").Append(p.Lat.ToString().Replace(',', '.')).Append("\" lon=\"").Append(p.Lon.ToString().Replace(',', '.')).Append("\">").Append("<ele>").Append(p.AveEle.ToString().Replace(',', '.')).Append("</ele></trkpt>");
                newContent.Append(" lat=\"");
                newContent.Append(p.Lat.ToString().Replace(',', '.'));
                newContent.Append("\" lon=\"");
                newContent.Append(p.Lon.ToString().Replace(',', '.'));
                newContent.Append("\">");
                newContent.Append("<ele>");
                newContent.Append(p.AveEle.ToString().Replace(',', '.'));
                newContent.Append("</ele></trkpt>");
            }
            //newContent += "</trkseg></trk></gpx>";
            newContent.Append("</trkseg></trk></gpx>");

            return newContent.ToString();
        }
        public static string FormatGPXwpt(List<Point> dayList)
        {
            System.Text.StringBuilder newContentWPT = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF - 8\"?><gpx creator=\"File\" version=\"1.0\">");
            foreach (Point p in dayList)
            {
                newContentWPT.Append("<wpt");
                newContentWPT.Append(" lat=\"");
                newContentWPT.Append(p.Lat.ToString().Replace(',', '.'));
                newContentWPT.Append("\" lon=\"");
                newContentWPT.Append(p.Lon.ToString().Replace(',', '.'));
                newContentWPT.Append("\">");
                newContentWPT.Append("<name>");
                newContentWPT.Append(p.Name);
                newContentWPT.Append("</name>");
                newContentWPT.Append("<ele>");
                newContentWPT.Append(p.AveEle.ToString().Replace(',', '.'));
                newContentWPT.Append("</ele></wpt>");
                Console.WriteLine(p.Name);
                //Console.ReadKey();
            }
            newContentWPT.Append("</gpx>");

            return newContentWPT.ToString();
        }
        public static void Save(string path, string fileName, string ext, string content)
        {
            FileInfo newFi = new FileInfo(path + fileName + ext);

            FileStream fsToWrite = newFi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //get the StreamWriter
            StreamWriter sw = new StreamWriter(fsToWrite);
            //write some text using StreamWriter
            sw.WriteLine(content);
            sw.Close();
            fsToWrite.Close();

        }
    }
}
