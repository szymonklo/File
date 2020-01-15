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

            int counter = 0;
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
                    Console.WriteLine(ele);
                    Console.ReadKey();
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
                                            Console.WriteLine(lat);
                                            break;
                                        }
                                        if (subtype == "lon=")
                                        {
                                            for (i++; f[i] != '"'; i++)
                                            {
                                                lon += f[i];
                                            }
                                            Console.WriteLine(lon);
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
                            //l[j].Lat = Double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
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
