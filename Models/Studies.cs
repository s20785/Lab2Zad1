using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }

        public Studies(string course, string mode)
        {
            this.name = courseCleaner(course);
            this.mode = mode;
        }

        private string courseCleaner(string course)
        {
            //Tu oczywiście można by rozbudować walidację o różne przypadki ale wiadomo o co chodzi
            int pos = course.IndexOf("dzienne");
            if (pos > -1)
                return course.Remove(pos).Trim();
            pos = course.IndexOf("zaoczne");
            if (pos > -1)
                return course.Remove(pos).Trim();
            pos = course.IndexOf("internetowe magisterskie");
            if (pos > -1)
                return course.Remove(pos).Trim();
            return course;
        
        }
    }
}
