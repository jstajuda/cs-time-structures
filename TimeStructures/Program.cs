using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            //var time = new Time(12, 14, 55);
            //var time2 = new Time(12, 13, 59);
            //Console.WriteLine(time.Equals(time2));
            //Console.WriteLine(time != time2);
            //Console.WriteLine(time.CompareTo(time2));
            //Console.WriteLine(time <= time2);
            //Console.WriteLine(time >= time2);
            //var period = Time.Difference(time, time2);
            //Console.WriteLine(period);
            //period = new TimePeriod(1);
            //Console.WriteLine(period);
            //period = TimePeriod.CreateFromString("72:46:17");
            //Console.WriteLine(period);
            //var period2 = new TimePeriod(73, 46, 17);
            //Console.WriteLine(period2);
            //Console.WriteLine(period.Equals(period2));
            //Console.WriteLine(period.CompareTo(period2));
            //Console.WriteLine(period > period2);
            //Console.WriteLine(period <= period2);
            //Console.WriteLine(period.Plus(period2));
            //Console.WriteLine(period + period2);
            //Console.WriteLine(TimePeriod.Plus(period, period2));

            Time czas = new Time();
            var temp = new Time(1, 1, 1);

            var time = new Time(0, 59, 59);
            var timePeriod = new TimePeriod(30, 02, 02);
            Console.WriteLine(time.Plus(timePeriod));

            //var time3 = new Time("00:0:12");
            //Console.WriteLine(time3);

            //var period2 = new TimePeriod("1:20:30");
            //Console.WriteLine(period2);

            //var times = new Time("25");
            //Console.WriteLine(times);
            Console.ReadKey();
        }
    }
}
