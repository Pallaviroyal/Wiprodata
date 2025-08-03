using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class Practice3
    {
        static void Main()
        {
            int[] numbers = { 10, 5, 8, 13, 22 };
            Console.WriteLine("Even numbers in the array:");
            Console.WriteLine("Odd numbers in the array:");
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    Console.WriteLine("even number:" + numbers[i]);
                }
                else
                {
                    Console.WriteLine("odd number:" + numbers[i]);
                }
            
                }
            }
        }
    }

