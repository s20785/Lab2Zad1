using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class ActiveStudy
    {
        public string name;
        public int numberOfStudents;

        public ActiveStudy(string name, int numberOfStudents)
        {
            this.name = name;
            this.numberOfStudents = numberOfStudents;
        }

    }
}
