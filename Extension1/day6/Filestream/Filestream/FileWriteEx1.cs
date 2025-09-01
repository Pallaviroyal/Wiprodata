using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Filestream
{
    internal class FileWriteEx1
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"c:/files/Demo1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("This is My First program in Files..");
            sw.WriteLine("Thank You All...");
            sw.Flush();
            sw.Close();
            fs.Close();
            Console.WriteLine("File Created Successfully...");


        }
    }
}
