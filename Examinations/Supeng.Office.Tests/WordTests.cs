using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Office.Tests
{
    [TestClass]
    public class WordTests
    {
        [TestMethod]
        public void ReplaceTests()
        {
            using (var doc = new DocXWordOperation("D:\\test.docx"))
            {
               //Console.WriteLine(doc.FindAndReplace("a.", "")); 
            }
        }
    }
}
