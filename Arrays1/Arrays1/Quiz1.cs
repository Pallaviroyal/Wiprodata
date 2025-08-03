using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays1
{
    internal class Quiz1
    {
        static void Main()
        {
            Console.WriteLine("Enter the number of rows in the jagged arrat:");
            int rows = Convert.ToInt32(Console.ReadLine());
            int[][] arr = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Enter number of elements in row{i + 1}:");
                int cols = Convert.ToInt32(Console.ReadLine());
                arr[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine($"Enetr element[{i}][{j}]: ");
                }
            }
            Console.WriteLine();

        }
    }
}
