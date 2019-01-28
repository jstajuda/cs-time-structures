using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeStructures
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        #region Properties and fields
        private byte hours;
        private byte minutes;
        private byte seconds;

        public byte Hours => hours;
        public byte Minutes => minutes;
        public byte Seconds => seconds;
        #endregion

        #region Constructors
        public Time(byte hh, byte mm, byte ss)
        {
            hours = (byte)(hh % 24);
            minutes = (byte)(mm % 60);
            seconds = (byte)(ss % 60);
        }
        public Time(byte hh, byte mm) : this(hh, mm, default(byte)) { }
        public Time(byte hh) : this(hh, default(byte), default(byte)) { }
        public Time(string timeString)
        {
            hours = default(byte);
            minutes = default(byte);
            seconds = default(byte);

            Time timeFromString = Time.CreateFromString(timeString);
            if(timeFromString != null)
            {
                hours = timeFromString.Hours;
                minutes = timeFromString.Minutes;
                seconds = timeFromString.Seconds;
            }
        }
        #endregion

        #region Create from string
        public static Time CreateFromString(string timeString)
        {
            if (String.IsNullOrWhiteSpace(timeString)) throw new ArgumentException();

            string pattern = @"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$";
            if( !Regex.IsMatch(timeString, pattern) ) throw new ArgumentException();
            
            return ConstructTimeObject( timeString.Split(':') );                    
        }

        private static Time ConstructTimeObject(string[] parts)
        {
            switch (parts.Length)
            {
                case 1:
                    return new Time(byte.Parse(parts[0]), default(byte), default(byte));
                case 2:
                    return new Time(byte.Parse(parts[0]), byte.Parse(parts[1]), default(byte));
                case 3:
                    return new Time(byte.Parse(parts[0]), byte.Parse(parts[1]), byte.Parse(parts[2]));
                default:
                    return new Time();
            }
        }
        #endregion

        #region Addition and Difference
        public Time Plus(TimePeriod timePeriod)
        {
            long secondsLeft = timePeriod.Seconds;

            int timePeriodHours = (int)(secondsLeft / 3600);
            secondsLeft %= 3600;

            int timePeriodMinutes = (int)(secondsLeft / 60);
            secondsLeft %= 60;

            byte timeSeconds = (byte)(seconds + secondsLeft);
            byte secondsOverflow = (byte)(timeSeconds / 60);
            timeSeconds %= 60;

            byte timeMinutes = (byte)(minutes + timePeriodMinutes);
            timeMinutes += secondsOverflow;
            byte minutesOverflow = (byte)(timeMinutes / 60);
            timeMinutes %= 60; 

            byte timeHours = (byte)(hours + timePeriodHours);
            timeHours += minutesOverflow;
            timeHours %= 24;

            return new Time(timeHours, timeMinutes, timeSeconds);
        }

        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        public static Time operator +(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        public static TimePeriod Difference(Time timeOne, Time timeTwo)
        {
            long seconds;

            if(timeOne >= timeTwo)
            {
                seconds = (timeOne.Hours - timeTwo.Hours) * 3600 +
                            (timeOne.Minutes - timeTwo.Minutes) * 60 +
                            (timeOne.Seconds - timeTwo.Seconds);
                return new TimePeriod(seconds);
            }

            return Difference(timeTwo, timeOne);
        }
        #endregion

        #region Equality
        public bool Equals(Time other)
        {
            return
                other.hours == hours &&
                other.minutes == minutes &&
                other.seconds == seconds;
        }

        public static bool operator ==(Time x, Time y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Time x, Time y)
        {
            return !x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                (obj is Time) &&
                Equals((Time)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Comparison
        public int CompareTo(Time other)
        {
            if (hours.CompareTo(other.hours) == 0)
                if (minutes.CompareTo(other.minutes) == 0)
                    return seconds.CompareTo(other.seconds);
                else return minutes.CompareTo(other.minutes);
            else return hours.CompareTo(other.hours);
        }

        public static bool operator > (Time x, Time y)
        {
            return x.CompareTo(y) == 1;
        }

        public static bool operator < (Time x, Time y)
        {
            return x.CompareTo(y) == -1;
        }

        public static bool operator >= (Time x, Time y)
        {
            return x.CompareTo(y) >= 0;
        }

        public static bool operator <= (Time x, Time y)
        {
            return x.CompareTo(y) <= 0;
        }
        #endregion

        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }

    }
}
