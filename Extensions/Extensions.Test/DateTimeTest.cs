using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Test
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        public void Elapset15sec()
        {
            Assert.AreEqual(15, DateTime.Now.AddSeconds(-15).Elapsed().Seconds);
        }

        [TestMethod]
        public void ElapsetMinus15sec()
        {
            Assert.AreEqual(-15, DateTime.Now.AddSeconds(15).Elapsed().Seconds);
        }

        [TestMethod]
        public void GetBirthday()
        {
            DateTime birthday = new DateTime(1992, 10, 12);

            int age = birthday.Age();
        }

        [TestMethod]
        public void CheckIfValidDateTime()
        {
            string nonDate = "Foo";
            string someDate = "Jan 1 2010";

            bool isDate = nonDate.IsDate(); //false
            isDate = someDate.IsDate(); //true
        }

        [TestMethod]
        public void MakeFriendlyDateTimeFormat()
        {
            DateTime dt = new DateTime(2008, 2, 10, 8, 48, 20);
            Console.WriteLine(dt.ToFriendlyDateString());
        }
    }
}
