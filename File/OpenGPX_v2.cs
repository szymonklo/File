using System;
using System.Collections.Generic;
using System.Text;

namespace File
{
    class OpenGPX_v2
    {
        public static void ReadGPX_v2(string f, ref List<Point> l)
        {
            //algorytm używający metod klasy string i własnej extension method - czas wykonania 2,25s
            
            int i = 0;
            int j = 0;

            while (i <= f.Length)
            {
                if (j == 26754)
                    Console.WriteLine("ostatni?");
                l.Add(new Point());

                if (f.FindStringBetweenDelimiters(i, "lat=\"", "\"").Item2 != -1)
                {
                    l[j].Lat = Double.Parse(f.FindStringBetweenDelimiters(i, "lat=\"", "\"").Item1, System.Globalization.CultureInfo.InvariantCulture);
                    i = f.FindStringBetweenDelimiters(i, "lat=\"", "\"").Item2;
                }
                else break;

                if (f.FindStringBetweenDelimiters(i, "lon=\"", "\"").Item2 != -1)
                {
                    l[j].Lon = Double.Parse(f.FindStringBetweenDelimiters(i, "lon=\"", "\"").Item1, System.Globalization.CultureInfo.InvariantCulture);
                    i = f.FindStringBetweenDelimiters(i, "lon=\"", "\"").Item2;
                }
                else break;


                if (f.FindStringBetweenDelimiters(i, "<ele>", "</ele>").Item2 != -1)
                {
                    l[j].Ele = Double.Parse(f.FindStringBetweenDelimiters(i, "<ele>", "</ele>").Item1, System.Globalization.CultureInfo.InvariantCulture);
                    i = f.FindStringBetweenDelimiters(i, "<ele>", "</ele>").Item2;
                }
                else break;

                j++;
            }

            
        }
    }
}
