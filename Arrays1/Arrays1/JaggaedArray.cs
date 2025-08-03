using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class JaggaedArray
    {
        static void Main()
        {
            int[][] arr = new int[3][];
            arr[0] = new int[] { 1, 2, 3 };
            arr[1] = new int[] { 4, 5 };
            arr[2] = new int[] { 6, 7, 8 };
            Console.WriteLine("Even numbers in the jagged array:");
            for (int i = 0; i<arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] % 2 == 0) 
                    {
                        Console.WriteLine(arr[i][j] + " ");
                    }
                }
            }
            Console.WriteLine();

        }
    }
}
