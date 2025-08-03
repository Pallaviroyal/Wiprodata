using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloworld1
{
    internal class Wipro
    {
        static void Main()
        {
            string input = "wipro";
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine("Reversed:"+new string(charArray));
        }
    }
}
