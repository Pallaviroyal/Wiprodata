using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class practice4
    {
        static void Main()
        {
            int[] numbers = { 10, 4, 8, 9, 5, 3 };
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    Console.Write(numbers[i] + " ");
                }
            }
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] % 2 != 0)
                    {
                        Console.WriteLine(numbers[i] + " ");

                    }
                }
            }
        }
    }
}

