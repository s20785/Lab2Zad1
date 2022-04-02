using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelloWorld.Models
{
    [XmlRoot("uczelnia")]
    public class University
    {
        public DateTime createdAt  = DateTime.Now;

        public string author = "Łukasz Lipiec s20785";
        //public Student[] studenci;
        public List<Student> studenci;
        public List<ActiveStudy> activeStudies;

        public University(List<Student> students)
        {
            studenci = students;
            activeStudies = new List<ActiveStudy>();

            //słownik przechowujący aktywne kierunki oraz liczbę ich studentów
            IDictionary<string, int> activeStudiesDict  
                = new Dictionary<string, int>();

            foreach (Student s in studenci)
            {
                if (activeStudiesDict.ContainsKey(s.studies.name))
                {
                    activeStudiesDict[s.studies.name] += 1;
                } else
                {
                    activeStudiesDict.Add(s.studies.name, 1);
                }
            }
            //serializacjia obiektów z activestudiesdict i zapis w modelu niversity
            foreach (KeyValuePair<string, int> activeStudy in activeStudiesDict)
            {
                //Console.WriteLine(activeStudy.Key + ": " + activeStudy.Value);
                activeStudies.Add(new ActiveStudy(activeStudy.Key, activeStudy.Value));
            }

            //foreach (ActiveStudy activeStudy in activeStudies)
            //{
            //    Console.WriteLine(activeStudy);
            //}

        }
    }
}
