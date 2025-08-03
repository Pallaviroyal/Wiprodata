using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oops
{
    internal class Studentresults
    {
        static void Main(string[] args)
        {

            Student student = new Student();
            student.SetName("Pallavi");
            student.SetAge(22);
            student.SetMarks(85);
            Console.WriteLine("Student Name:"+student.GetName());
            Console.WriteLine("Student Age:"+student.GetAge());
            Console.WriteLine("Student Marks:+"+student.GetMarks());
            Console.ReadLine();
        }
    }
}
