using System;
using System.Collections.Generic;
using System.Text;

namespace File
{
    class GPXProcessor
    {
        
        public static void CalculateGPX(ref List<Point> l)
        {
            double distanceOfAverage = 0.1;
            double sumAsc = 0;
            double sumAveAsc = 0;
            double sumAveDes = 0;
            double totalDist = 0;

            l[0].AveEle = l[1].Ele;

            int j;
            for (j = 1; j < l.Count; j++)
            {
                l[j].Section = Point.Distance(l[j], l[j - 1]);
                totalDist += l[j].Section;

                double sumOfEle = 0;
                int numOfPoints = 0;
                for (int k = j; k >= 0; k--)
                {
                    if (Point.Distance(l[j], l[k]) < distanceOfAverage)
                    {
                        sumOfEle += l[k].Ele;
                        numOfPoints++;
                    }
                    else break;
                }
                for (int k = j + 1; k < l.Count; k++)
                {
                    if (Point.Distance(l[j], l[k]) < distanceOfAverage)
                    {
                        sumOfEle += l[k].Ele;
                        numOfPoints++;
                    }
                    else break;
                }

                //l[j].AveEle = (sumOfEle / numOfPoints);
                l[j].AveEle = Math.Round((sumOfEle / numOfPoints), 1);


                l[j].Asc = (l[j].Ele - l[j - 1].Ele);
                sumAsc += (l[j].Asc > 0 ? l[j].Asc : 0);

                l[j].AveAsc = (l[j].AveEle - l[j - 1].AveEle);

                sumAveAsc += (l[j].AveAsc >= 0 ? l[j].AveAsc : 0);
                sumAveDes += (l[j].AveAsc < 0 ? l[j].AveAsc : 0);



            }
            Console.WriteLine($"Asc: {sumAsc}");
            Console.WriteLine($"AveAsc: {(int)sumAveAsc}");
        }

        public static List<Point> DayList(List<Point> l)
        {
            double dailyDist = 0;
            double dailyAsc = 0;
            double dailyDes = 0;


            double cOA = 0.05;
            double cOD = -0.01;
            double flatDailyDist = 75;

            var dayList = new List<Point>();

            for (int k = 0, d = 0; k < l.Count; k++)
            {
                if (dailyDist + cOA * dailyAsc + cOD * dailyDes < flatDailyDist)
                {
                    dailyDist += l[k].Section;
                    if (l[k].AveAsc >= 0)
                    {
                        dailyAsc += (l[k].AveAsc);
                    }
                    else
                    {
                        dailyDes -= (l[k].AveAsc);
                    }
                }
                else
                {
                    dayList.Add(new Point());
                    dayList[d].Name = new System.Text.StringBuilder($"Day: {d + 1}, daily distance: {dailyDist:N0},  daily ascent: {dailyAsc:N0}, daily descent: {dailyDes:N0}").ToString();
                    dayList[d].Lat = l[k].Lat;
                    dayList[d].Lon = l[k].Lon;
                    dayList[d].Ele = l[k].Ele;


                    dailyDist = 0;
                    dailyAsc = 0;
                    dailyDes = 0;
                    d++;
                }

            }
            return dayList;
        }
    }
}
