using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeStructures;

namespace TimeStructuresTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void DefaultConstructorInitializesDefaultValues()
        {
            byte expectedHours = 0;
            byte expectedMinutes = 0;
            byte expectedSeconds = 0;

            var time = new Time();
            var actualHours = time.Hours;
            var actualMinutes = time.Minutes;
            var actualSeconds = time.Seconds;

            Assert.AreEqual(expectedHours, actualHours);
            Assert.AreEqual(expectedMinutes, actualMinutes);
            Assert.AreEqual(expectedSeconds, actualSeconds);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, "00:00:00")]
        [DataRow(1, 2, 3, "01:02:03")]
        [DataRow(25, 35, 45, "01:35:45")]
        public void ParameterizedConstructorThreeParametersPassed(int hh, int mm, int ss, string expectedString)
        {
            byte hours = (byte)(hh % 24);
            byte minutes = (byte)(mm % 60);
            byte seconds = (byte)(ss % 60);

            Time actual = new Time(hours, minutes, seconds);

            Assert.AreEqual(hours, actual.Hours);
            Assert.AreEqual(minutes, actual.Minutes);
            Assert.AreEqual(seconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow(0, 0, "00:00:00")]
        [DataRow(1, 2, "01:02:00")]
        [DataRow(25, 35, "01:35:00")]
        public void ParameterizedConstructorTwoParametersPassed(int hh, int mm, string expectedString)
        {
            byte hours = (byte)(hh % 24);
            byte minutes = (byte)(mm % 60);
            byte seconds = 0;

            Time actual = new Time(hours, minutes);

            Assert.AreEqual(hours, actual.Hours);
            Assert.AreEqual(minutes, actual.Minutes);
            Assert.AreEqual(seconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow(0, "00:00:00")]
        [DataRow(1, "01:00:00")]
        [DataRow(25, "01:00:00")]
        public void ParameterizedConstructorOneParameterPassed(int hh, string expectedString)
        {
            byte hours = (byte)(hh % 24);
            byte minutes = 0;
            byte seconds = 0;

            Time actual = new Time(hours);

            Assert.AreEqual(hours, actual.Hours);
            Assert.AreEqual(minutes, actual.Minutes);
            Assert.AreEqual(seconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("01:02:00", "01:02:00")]
        [DataRow("01:35:55", "01:35:55")]
        [DataRow("1", "01:00:00")]
        [DataRow("1:2", "01:02:00")]
        [DataRow("1:2:3", "01:02:03")]
        public void ParameterizedConstructorStringPassed(string inputString, string expectedString)
        {
            Time actual = new Time(inputString);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("67")]
        [DataRow("12:60")]
        [DataRow("12:15:65")]
        [DataRow("12:15:ab")]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterizedConstructorStringPassedThrows(string inputString)
        {
            Time actual = new Time(inputString);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("01:02:00", "01:02:00")]
        [DataRow("01:35:55", "01:35:55")]
        public void EqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeOne == timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("01:02:00", "01:02:00")]
        [DataRow("01:35:55", "01:35:55")]
        public void NotEqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = false;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeOne != timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        public void GreaterThanOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeOne > timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        [DataRow("02:35:55", "02:35:55")]
        public void GreaterOrEqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeOne >= timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        public void LessThanOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeTwo < timeOne);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        [DataRow("02:35:55", "02:35:55")]
        public void LessOrEqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new Time(inputStringOne);
            var timeTwo = new Time(inputStringTwo);

            Assert.AreEqual(expected, timeTwo <= timeOne);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("01:03:00", "01:02:00", "02:05:00")]
        [DataRow("02:35:55", "01:35:55", "04:11:50")]
        [DataRow("00:59:59", "30:02:02", "07:02:01")]
        public void AddTimePeriodCorrect(string inputStringOne, string inputStringTwo, string expectedString)
        {
            var time = new Time(inputStringOne);
            var timePeriod = new TimePeriod(inputStringTwo);
            var actual = time + timePeriod;

            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("01:03:00", "01:02:00", "00:01:00")]
        [DataRow("12:30:50", "00:00:55", "12:29:55")]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        public void SubstractTimePeriodCorrect(string inputStringOne, string inputStringTwo, string expectedString)
        {
            var time = new Time(inputStringOne);
            var timePeriod = new TimePeriod(inputStringTwo);
            var actual = time - timePeriod;

            Assert.AreEqual(expectedString, actual.ToString());
        }
    }
}
