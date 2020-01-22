using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace File
{
    class GPXReader
    {
        public static string Open(string path, string fileName, string ext)
        {
            //Create FileInfo object for DummyFile.txt
            FileInfo fi = new FileInfo(path + fileName + ext);

            //open DummyFile.txt for read operation
            FileStream fsToRead = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //open DummyFile.txt for write operation
            //FileStream fsToWrite = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            //get the StreamReader

            StreamReader sr = new StreamReader(fsToRead);
            //read all texts using StreamReader object

            //czy to nie ma być na końcu?????????????????????????
            
            string fileContent = sr.ReadToEnd();
            sr.Close();
            fsToRead.Close();
            return fileContent;

        }
        public static void Read(string f, ref List<Point> l)
        {
            string type = "";
            int j = 0;

            for (int i = 0; i < f.Length; i++)
            {
                if (type == "ele")
                {
                    string ele = "";
                    for (; f[i] != '<'; i++)
                    {
                        ele += f[i];
                    }
                    l[j].Ele = Double.Parse(ele, System.Globalization.CultureInfo.InvariantCulture);
                    l[j].Ind = j;
                    j++;
                }
                if (f[i] == '<')
                {
                    type = "";
                    for (i++; (f[i] != '>'); i++)
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
                                            break;
                                        }
                                        if (subtype == "lon=")
                                        {
                                            for (i++; f[i] != '"'; i++)
                                            {
                                                lon += f[i];
                                            }
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
                            l[j].Lat = Double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
                            l[j].Lon = Double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            type += f[i];
                        }
                    }
                }
            }
        }
    }
}
