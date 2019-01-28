using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeStructures;

namespace TimeStructuresTests
{
    [TestClass]
    public class TimePeriodTests
    {
        [TestMethod]
        public void DefaultConstructorInitializesDefaultValues()
        {
            long expectedSeconds = 0;

            var period = new TimePeriod();

            Assert.AreEqual(expectedSeconds, period.Seconds);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, 0, "000:00:00")]
        [DataRow(1, 2, 3, 3723, "001:02:03")]
        [DataRow(25, 35, 45, 92145, "025:35:45")]
        public void ParameterizedConstructorThreeParametersPassed(int hh, int mm, int ss, long expectedSeconds, string expectedString)
        {
            byte hours = (byte)hh;
            byte minutes = (byte)mm;
            byte seconds = (byte)ss;

            TimePeriod actual = new TimePeriod(hours, minutes, seconds);

            Assert.AreEqual(expectedSeconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow(0, 0, 60)]
        [DataRow(0, 65, 0)]
        [DataRow(60, 60, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParameterizedConstructorThreeParametersThrows(int hh, int mm, int ss)
        {
            byte hours = (byte)hh;
            byte minutes = (byte)mm;
            byte seconds = (byte)ss;

            TimePeriod actual = new TimePeriod(hours, minutes, seconds);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, "000:00:00")]
        [DataRow(1, 2, 3720, "001:02:00")]
        [DataRow(25, 35, 92100, "025:35:00")]
        public void ParameterizedConstructorTwoParametersPassed(int hh, int mm, long expectedSeconds, string expectedString)
        {
            byte hours = (byte)hh;
            byte minutes = (byte)mm;

            TimePeriod actual = new TimePeriod(hours, minutes);

            Assert.AreEqual(expectedSeconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow(99, 99)]
        [DataRow(0, 65)]
        [DataRow(60, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParameterizedConstructorTwoParametersThrows(int hh, int mm)
        {
            byte hours = (byte)hh;
            byte minutes = (byte)mm;

            TimePeriod actual = new TimePeriod(hours, minutes);
        }

        [DataTestMethod]
        [DataRow(0, 0, "000:00:00")]
        [DataRow(1, 1, "000:00:01")]
        [DataRow(25, 25, "000:00:25")]
        public void ParameterizedConstructorOneParameterPassed(int ss, long expectedSeconds, string expectedString)
        {
            byte seconds = (byte)ss;

            TimePeriod actual = new TimePeriod(seconds);

            Assert.AreEqual(expectedSeconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }

        [DataTestMethod]
        [DataRow(-99)]
        [DataRow(-1)]
        [DataRow(-60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParameterizedConstructorOneParameterThrows(int ss)
        {
            long seconds = ss;

            TimePeriod actual = new TimePeriod(seconds);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "01:00:00", 3600)]
        [DataRow("00:00:00", "01:01:00", 3660)]
        [DataRow("00:00:00", "01:01:01", 3661)]
        public void ConstructorFromTwoTimeObjects(string timeOne, string timeTwo, long expectedSeconds)
        {
            var oTimeOne = new Time(timeOne);
            var oTimeTwo = new Time(timeTwo);

            var timePeriod = new TimePeriod(oTimeOne, oTimeTwo);

            Assert.AreEqual(expectedSeconds, timePeriod.Seconds);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "000:00:00", 0)]
        [DataRow("1:1:1", "001:01:01", 3661)]
        [DataRow("99:59:59", "099:59:59", 359999)]
        public void ConstructorFromString(string inputString, string expectedString, long expectedSeconds)
        {
            var actual = new TimePeriod(inputString);
            Assert.AreEqual(expectedSeconds, actual.Seconds);
            Assert.AreEqual(expectedString, actual.ToString());
        }


        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("01:02:00", "01:02:00")]
        [DataRow("01:35:55", "01:35:55")]
        public void EqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

            Assert.AreEqual(expected, timeOne == timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("01:02:00", "01:02:00")]
        [DataRow("01:35:55", "01:35:55")]
        public void NotEqualOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = false;

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

            Assert.AreEqual(expected, timeOne != timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        public void GreaterThanOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

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

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

            Assert.AreEqual(expected, timeOne >= timeTwo);
        }

        [DataTestMethod]
        [DataRow("00:00:01", "00:00:00")]
        [DataRow("01:03:00", "01:02:00")]
        [DataRow("02:35:55", "01:35:55")]
        public void LessThanOperatorCorrect(string inputStringOne, string inputStringTwo)
        {
            var expected = true;

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

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

            var timeOne = new TimePeriod(inputStringOne);
            var timeTwo = new TimePeriod(inputStringTwo);

            Assert.AreEqual(expected, timeTwo <= timeOne);
        }
    }
}
