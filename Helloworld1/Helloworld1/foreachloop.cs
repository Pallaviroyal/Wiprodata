using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloworld1
{
    internal class foreachloop
    {
        static void Main()
        {
            string[] fruits = { "Appale", "Banana", "Cherry" };
            foreeach(string fruit in fruits)
        {
                Console.WriteLine("Fruits:" + fruits);

            }
        }
    }
}