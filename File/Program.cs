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

            DateTime start = DateTime.Now;

            int j = 0;

            var pointList = new List<Point>();
            var l = pointList;

            string type = "";

            for (int i = 0; i<f.Length; i++)
            {
                if (type == "ele")
                {
                    string ele = "";
                    for (; f[i] != '<'; i++)
                    {
                        ele += f[i];
                    }
                    //Console.WriteLine(ele);
                    l[j].Ele = Double.Parse(ele, System.Globalization.CultureInfo.InvariantCulture);
                    l[j].Ind = j;
                    j++;
                    //if (j == 10)
                      //  break;
                    //if (j==3)
                    //    Console.WriteLine(j);
                    //Console.ReadKey();
                }
                if (f[i]=='<')
                {
                    type = "";// new string (f[i],1);
                    for (i++; (f[i] != '>')/*&&( f[i] != ' ')*/; i++)
                    {
                        if (type == "trkpt")
                        {
                            string lat = "";
                            string lon = "";
                            
                            for (i++; f[i] != '>'; i++)
                            {
                                if (f[i] == 'l')
                                {
                                    string subtype = "";
                                    for (; f[i] != '>'; i++)
                                    {
                                        if (subtype == "lat=")
                                        {
                                            for (i++; f[i] != '"'; i++)
                                            {
                                                lat += f[i];
                                            }
                                            //Console.WriteLine(lat);
                                            break;
                                        }
                                        if (subtype == "lon=")
                                        {
                                            for (i++; f[i] != '"'; i++)
                                            {
                                                lon += f[i];
                                            }
                                            //Console.WriteLine(lon);
                                            break;
                                        }
                                        else
                                        {
                                            subtype += f[i];
                                        }
                                    }
                                }
                            }
                            i--;
                            l.Add(new Point());
                            //l[j].Lat = Convert.ToDouble(lat);
                            l[j].Lat = Double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
                            l[j].Lon = Double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);

                            //Console.WriteLine(l[j].Lat);
                            //i++;

                        }
                        else
                        {
                            type += f[i];
                        }
                    }
                }
            }

            TimeSpan dur = start - DateTime.Now;
            Console.WriteLine($"Reading: {dur}");

            double distanceOfAverage = 0.1;
            double sumAsc = 0;
            double sumAveAsc = 0;

            start = DateTime.Now;
            l[0].AveEle = l[1].Ele;
            for (j = 1;j<l.Count;j++)
            {
                l[j].Section = Point.Distance(l[j], l[j - 1]);
                

                double sumOfEle = 0;
                int numOfPoints = 0;
                for (int k=j;k>=0;k--)
                {
                    if (Point.Distance(l[j], l[k]) < distanceOfAverage)
                    {
                        sumOfEle += l[k].Ele;
                        numOfPoints++;
                    }
                    else break;
                }
                for (int k = j+1; k < l.Count; k++)
                {
                    if (Point.Distance(l[j], l[k]) < distanceOfAverage)
                    {
                        sumOfEle += l[k].Ele;
                        numOfPoints++;
                    }
                    else break;
                }

                l[j].AveEle = sumOfEle / numOfPoints;

                l[j].Asc = (l[j].Ele - l[j - 1].Ele);
                sumAsc += (l[j].Asc > 0 ? l[j].Asc : 0);

                l[j].AveAsc = (l[j].AveEle - l[j - 1].AveEle);
                sumAveAsc += (l[j].AveAsc > 0 ? l[j].AveAsc : 0);


            }
            dur = start - DateTime.Now;
            Console.WriteLine($"Processing: {dur}");
            Console.WriteLine($"Asc: {sumAsc}");
            Console.WriteLine($"AveAsc: {(int)sumAveAsc}");

            start = DateTime.Now;

            string newContent = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><gpx><trk><trkseg>";
            foreach (Point p in l)
            {
                newContent += "<trkpt";
                newContent += " lat=\"";
                newContent += p.Lat.ToString().Replace(',','.');
                newContent += "\" lon=\"";
                newContent += p.Lon.ToString().Replace(',', '.');
                newContent += "\">";
                newContent += "<ele>";
                newContent += p.AveEle.ToString().Replace(',', '.');
                newContent += "</ele></trkpt>";
            }
            newContent += "</trkseg></trk></gpx>";

            //inne
            //FileStream fs = new FileStream(path+newFileName+ext, FileMode.Create);
            //fs.Close();
            //Console.Write("File has been created and the Path is D:\\csharpfile.txt");
            //Console.ReadKey();

            Console.WriteLine();

            FileInfo newFi = new FileInfo(path + newFileName + ext);

            FileStream fsToWrite = newFi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //get the StreamWriter
            StreamWriter sw = new StreamWriter(fsToWrite);
            //write some text using StreamWriter
            sw.WriteLine(newContent);
            sw.Close();

            //close all Stream objects
            fsToRead.Close();
            fsToWrite.Close();

            dur = start - DateTime.Now;
            Console.WriteLine($"Writing: {dur}");
        }
    }
}