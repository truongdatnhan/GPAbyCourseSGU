using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPAbyCourseSGU
{
    public class Student
    {
        public String MSSV { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Class { get; set; }
        public String GPA { get; set; }
        public String StudentInfo => $"{MSSV} {FirstName} {LastName} {Class} {GPA}";
    }
}
