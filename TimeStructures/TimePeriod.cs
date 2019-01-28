using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeStructures
{
    /// <summary>
    /// Provides representation for time period (in seconds)
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        #region Properties and fields
        private long seconds;

        /// <summary>
        /// Sets time period length (in seconds)
        /// </summary>
        public long Seconds => seconds;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        /// <param name="minutes">Number of minutes (0-59)</param>
        /// <param name="seconds">Number of seconds (0-59)</param>
        public TimePeriod(byte hours, byte minutes, byte seconds)
        {
            if (seconds > 59 || minutes > 59) throw new ArgumentOutOfRangeException();
            this.seconds = hours * 3600 + minutes * 60 + seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        /// <param name="minutes">Number of minutes (0-59)</param>
        public TimePeriod(byte hours, byte minutes)
        {
            if (minutes > 59) throw new ArgumentOutOfRangeException();
            seconds = hours * 3600 + minutes * 60;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="seconds">Duration in seconds</param>
        public TimePeriod(long seconds)
        {
            if(seconds < 0) throw new ArgumentOutOfRangeException();
            this.seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="timeOne">First point in time</param>
        /// <param name="timeTwo">Second point in time</param>
        public TimePeriod(Time timeOne, Time timeTwo)
        {
            seconds = Time.Difference(timeOne, timeTwo).Seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="timeString">Input time string (hhhh:mm:ss)</param>
        public TimePeriod(string timeString)
        {
            seconds = default(long);

            TimePeriod periodFromString = TimePeriod.CreateFromString(timeString);
            if(periodFromString != null)
            {
                seconds = periodFromString.Seconds;
            }
        }
        #endregion

        #region Create from string
        /// <summary>
        /// Returns a new instance of the <see cref="TimePeriod"/> struct.
        /// </summary>
        /// <param name="timeString">Input time string (hhhh:mm:ss)</param>
        /// <returns>Time period from given parameters.</returns>
        public static TimePeriod CreateFromString(string timeString)
        {
            if (String.IsNullOrWhiteSpace(timeString)) throw new ArgumentException();

            string pattern = @"^(?:(?:(\d?\d?\d?\d):)?([0-5]?\d):)?([0-5]?\d)$";
            if (!Regex.IsMatch(timeString, pattern)) throw new ArgumentException();

            return ConstructTimePeriodObject(timeString.Split(':'));
        }

        private static TimePeriod ConstructTimePeriodObject(string[] parts)
        {
            switch (parts.Length)
            {
                case 1:
                    return new TimePeriod(byte.Parse(parts[0]), default(byte), default(byte));
                case 2:
                    return new TimePeriod(byte.Parse(parts[0]), byte.Parse(parts[1]), default(byte));
                case 3:
                    return new TimePeriod(byte.Parse(parts[0]), byte.Parse(parts[1]), byte.Parse(parts[2]));
                default:
                    return new TimePeriod();
            }
        }
        #endregion

        #region Addition
        /// <summary>
        /// Increases current time period with another time period.
        /// </summary>
        /// <param name="timePeriod">Time period to add.</param>
        /// <returns>New time period being sum of current and given time periods.</returns>
        public TimePeriod Plus(TimePeriod timePeriod)
        {
            return new TimePeriod(seconds + timePeriod.Seconds);
        }

        /// <summary>
        /// Increases one time period with another time period.
        /// </summary>
        /// <param name="periodOne">Time period to increase.</param>
        /// <param name="periodTwo">Time period to add.</param>
        /// <returns>New time period being sum of two time periods.</returns>
        public static TimePeriod Plus(TimePeriod periodOne, TimePeriod periodTwo)
        {
            return new TimePeriod(periodOne.Seconds + periodTwo.Seconds);
        }

        public static TimePeriod operator +(TimePeriod periodOne, TimePeriod periodTwo)
        {
            return periodOne.Plus(periodTwo);
        }
        #endregion

        #region Equality
        public bool Equals(TimePeriod other)
        {
            return other.seconds == seconds;
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                   (obj is TimePeriod) &&
                   Equals((TimePeriod)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(TimePeriod x, TimePeriod y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(TimePeriod x, TimePeriod y)
        {
            return !x.Equals(y);
        }
        #endregion

        #region Comparison
        public int CompareTo(TimePeriod other)
        {
            return Seconds.CompareTo(other.Seconds);
        }

        public static bool operator >(TimePeriod x, TimePeriod y)
        {
            return x.CompareTo(y) == 1;
        }

        public static bool operator <(TimePeriod x, TimePeriod y)
        {
            return x.CompareTo(y) == -1;
        }

        public static bool operator >=(TimePeriod x, TimePeriod y)
        {
            return x.CompareTo(y) >= 0;
        }

        public static bool operator <=(TimePeriod x, TimePeriod y)
        {
            return x.CompareTo(y) <= 0;
        }
        #endregion

        public override string ToString()
        {
            long secondsLeft = seconds;

            int hours = (int)(secondsLeft / 3600);
            secondsLeft %= 3600;

            int minutes = (int)(secondsLeft / 60);
            secondsLeft %= 60;

            return $"{hours:000}:{minutes:00}:{secondsLeft:00}";
        }
    }
}
