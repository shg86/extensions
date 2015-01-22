using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Test
{
    [TestClass]
    public class DecimalIntTest
    {
        [TestMethod]
        public void PercentageOfIntInt()
        {
			Assert.AreEqual(33.0M, 100.PercentageOf(33));
        }

        [TestMethod]
        public void CheckIfPrime()
        {
            bool b = 7.IsPrime(); // true
        }

        [TestMethod]
        public void GetByteSize()
        {
            var kb1 = 1.KB();
            var mb1 = 1.MB();
            var gb1 = 1.GB();
            var tb1 = 1.TB();
        }

        public void GetSquared()
        {
            int result = 5.Squared();
        }
    }
}
