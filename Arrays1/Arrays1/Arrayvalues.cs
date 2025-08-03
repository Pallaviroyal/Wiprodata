using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class Arrayvalues
    {
        static void Main()
        {
            int[] numbers = { 3, 4, 5, 6, 7 };
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }
            Console.WriteLine("sum of all elements in the array is :"+sum);

            
        }
    }
}
