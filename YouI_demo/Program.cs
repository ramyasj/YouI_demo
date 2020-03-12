using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouI_demo.BAL;

namespace YouI_demo
{
   public class Program
    {

       static void Main(string[] args)
        {
            Console.WriteLine("Please input the path of the file to be processed.");

           
            //Reads the path of the file
            string fPath = Console.ReadLine();

            //Process the file from the give file path
            string strResult = FileProcessing.ProcessDataAndGenerateFiles(fPath);

            Console.WriteLine(strResult);
            Console.Read();

        }

       
    }
}
