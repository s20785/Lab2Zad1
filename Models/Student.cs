using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class Student
    {

        public string indexNumber { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies { get; set; }
    }
}
