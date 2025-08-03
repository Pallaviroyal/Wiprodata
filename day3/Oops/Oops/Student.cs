using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Oops
{
    internal class Student
    {
        private string name;
        private int age;
        private int marks;
        public void SetName(string studentName)
        {
            name = studentName;
        }
        public string GetName()
        {
            return name;
        }
        public void SetAge(int studentAge)
        {
            if (studentAge > 0)
                age = studentAge;
        }
        public int GetAge()
        {
            return age;
        }
        public void SetMarks(int studentMarks)
        {
            if (studentMarks >= 0 && studentMarks <= 100)
                marks = studentMarks;
        }
            public int GetMarks()
            {
                return marks;
            }
        }
    }


