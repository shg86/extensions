using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Test
{
    [TestClass]
    public class StringTest
    {
        private enum DummyEnum
        {
            one,
            two,
            three
        }


        [TestMethod]
        public void StringToDummyEnum()
        {
            Assert.AreEqual(DummyEnum.one, "one".ToEnum<DummyEnum>());
        }

        [TestMethod]
        public void IgnoreCaseStringToDummyEnum()
        {
            Assert.AreEqual(DummyEnum.one, "one".ToEnum<DummyEnum>(true));
        }

        [TestMethod]
        public void RetrieveBetween()
        {
            string input = "anothertest";

            string result = input.RemoveLeft(7);

        }

        [TestMethod]
        public void EnsureStartsWith()
        {
            string input = ".txt";

            string result = input.EnsureStartsWith(".");
        }

        [TestMethod]
        public void RemoveSpecialCharacters()
        {
            string input = "#!col or";

            string result = input.RemoveAllSpecialCharacters();

            Assert.AreEqual(result, "col or");
        }
    }


}
