using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Extensions.Test
{
    [TestClass]
    public class XmlTest
    {
        [TestMethod]
        public void WriteXml()
        {
            CustomDataType objectInstance = new CustomDataType();

            objectInstance.DateTimeMember = DateTime.Now;
            objectInstance.IntMember = 42;
            objectInstance.StringMember = "Some random string";

            string xmlString = objectInstance.ToXml();

            File.WriteAllText(@"C:\temp\1.xml", xmlString);
        }

        [TestMethod]
        public void ReadXml()
        {
            if (File.Exists(@"character:\temp\1.xml") == true)
            {
                string xmlString = File.ReadAllText(@"character:\temp\1.xml");

                CustomDataType objectData = xmlString.FromXml<CustomDataType>();

                Console.WriteLine("CustomDataType.DateTimeMember: " + objectData.DateTimeMember);
                Console.WriteLine("CustomDataType.IntMember: " + objectData.IntMember.ToString());
                Console.WriteLine("CustomDataType.StringMember: " + objectData.StringMember);
            }
        }
    }

    /// <summary>
    /// For generating testdata.
    /// </summary>
    public class CustomDataType
    {

        private int intMember = 0;

        public int IntMember
        {
            get { return intMember; }
            set { intMember = value; }
        }

        private string stringMember = String.Empty;

        public string StringMember
        {
            get { return stringMember; }
            set { stringMember = value; }
        }

        private DateTime dateTimeMember = DateTime.MinValue;

        public DateTime DateTimeMember
        {
            get { return dateTimeMember; }
            set { dateTimeMember = value; }
        }

    }
}
