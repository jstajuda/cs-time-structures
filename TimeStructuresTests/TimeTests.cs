using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeStructures;

namespace TimeStructuresTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void DefaultConstructorInstantiateWithDefaultValues()
        {
            var expected = "00 : 00 : 00";

            var time = new Time();
            var actual = time.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
