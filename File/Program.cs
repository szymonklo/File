using System;
using System.Collections.Generic;
using System.IO;

namespace File
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Szymon\Documents\GPX\P1\";
            string fileName = "p1";
            string ext = ".gpx";
            string newFileName = fileName + "_new";
            //Create FileInfo object for DummyFile.txt
            FileInfo fi = new FileInfo(path+fileName+ext);

            //open DummyFile.txt for read operation
            FileStream fsToRead = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //open DummyFile.txt for write operation
            //FileStream fsToWrite = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //get the StreamReader

            StreamReader sr = new StreamReader(fsToRead);
            //read all texts using StreamReader object
            string fileContent = sr.ReadToEnd();
            string f = fileContent;
            sr.Close();

            int j = 0;

            var pointList = new List<Point>();
            var l = pointList;

            for (int i = 0; i<f.Length; i++)
            {
                string type = "";
                if (f[i]=='<')
                {
                    type = "";// new string (f[i],1);
                    for (i++;f[i]!='>';i++)
                    {
                        type += f[i];
                        if (type == "trkpt lat=\"")
                        {
                            string lat = "";
                            for (i++; f[i] != '"'; i++)
                            {
                                lat += f[i];
                            }
                            l.Add (new Point());
                            //l[j].Lat = Convert.ToDouble(lat);
                            l[j].Lat = Double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
                            Console.WriteLine(l[j].Lat);
                        }
                    }
                    Console.WriteLine(type);
                    Console.ReadKey();
                }
            }

            //inne
            //FileStream fs = new FileStream(path+newFileName+ext, FileMode.Create);
            //fs.Close();
            //Console.Write("File has been created and the Path is D:\\csharpfile.txt");
            //Console.ReadKey();

            FileInfo newFi = new FileInfo(path + newFileName + ext);

            FileStream fsToWrite = newFi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //get the StreamWriter
            StreamWriter sw = new StreamWriter(fsToWrite);
            //write some text using StreamWriter
            sw.WriteLine("Another line from streamwriter");
            sw.Close();

            //close all Stream objects
            fsToRead.Close();
            fsToWrite.Close();
        }
    }
}
