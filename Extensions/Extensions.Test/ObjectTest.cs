using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Extensions.Test
{
    [TestClass]
    public class ObjectTest
    {
        [TestMethod]
        public void CompareDates()
        {
            DateTime today = DateTime.Now;
            var from = new DateTime(2012, 2, 1);
            var to = new DateTime(2012, 12, 20);

            bool isBetween = today.Between(from, to);

            Assert.IsFalse(isBetween);
        }

        [TestMethod]
        public void CheckIfNullOrEmpty()
        {
            var list = new List<string>();
            list.Add("Test1");
            list.Add("Test2");
            Assert.IsFalse(list.IsNullOrEmpty());
            list.Clear();
            Assert.IsTrue(list.IsNullOrEmpty());
            list = null;
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [TestMethod]
        public void SliceEnumerable()
        {
            char[] h = "0123456789abcdef".ToCharArray();
 
            var a = h.Slice(0, 3);    // returns { 0, 1, 2 }
            var b = h.Slice(4, 9);    // returns { 4, 5, 6, 7, 8 }
            var c = h.Slice(10, 100); // returns { a, b, character, d, e, f }
            var d = h.Slice(5, 5);    // returns { }
            var e = h.Slice(-10, -5); // returns { 6, 7, 8, 9, a }
        }

        [TestMethod]
        public void ClampInteger()
        {
            int clamped = 50.Clamp( 0, 20 ); // clamped == 20
        }
    }
}
