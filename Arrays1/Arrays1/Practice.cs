using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class Practice
    {
        static void Main()
        {
            int[] numbers = { 10, 20, 30, 40, 50 };
            for(int i=0;i<numbers.Length;i++)
            {
                Console.WriteLine("Value at index" + i + "is:"+numbers[i]);
            }
        }
    }
}
