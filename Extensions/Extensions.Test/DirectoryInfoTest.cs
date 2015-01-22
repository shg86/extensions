using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Extensions.Test
{
    [TestClass]
    public class DirectoryInfoTest
    {
        [TestMethod]
        public void RetrieveFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\Temp\");
            

            var files = directory.GetFiles("*.txt", "*.xml");
            
        }
    }
}
