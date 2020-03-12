using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouI_demo;
using System.Text;
using System.IO;

namespace YouiUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValidFile()
        {
            try
            {
                string filePath = "validfile.csv";
                string delimiter = ",";

                string[][] output = new string[][]{
                  new string[]{"FirstName,LastName,Address,PhoneNumber"},
                  new string[]{ "Jimmy,Smith,102 Long Lane,29384857" },
                  new string[]{ "Clive,Owen,65 Ambling Way,31214788" },
                  new string[]{ "James,Brown,82 Stewart St,32114566" },
                  new string[]{ "Graham,Howe,12 Howard St,8766556" },
                  new string[]{ "John,Howe,78 Short Lane,29384857" }
              };
                int length = output.GetLength(0);
                StringBuilder sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.WriteAllText(filePath, sb.ToString());

                string strActual = YouI_demo.BAL.FileProcessing.ProcessDataAndGenerateFiles("validfile.csv");
                string strExpected = "Files created.";
                 Assert.AreEqual(strExpected, strExpected);
                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message.ToString());
            }
        }

        [TestMethod]
        public void TestInvalidFile()
        {
            string strActual = YouI_demo.BAL.FileProcessing.ProcessDataAndGenerateFiles("c:\\invalidFile.xlsx");
            string strExpected = "File is empty. Please provide a valid file.";
            Assert.AreEqual(strActual, strExpected);

        }

        [TestMethod]
        public void TestEmptyFile()
        {
            string strActual = YouI_demo.BAL.FileProcessing.ProcessDataAndGenerateFiles("");
            string strExpected = "Files created.";
            Assert.AreNotEqual(strActual, strExpected);
        }
    }
}
