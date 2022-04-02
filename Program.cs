using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HelloWorld.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TODO active studies w JSONIE
            //0. Walidacja argumentów
            {
                if (args.Length != 3)
                {
                    Logger.log("Wystąpił ArgumentException: Nieprawidłowa liczba wymaganych argumentów!");
                    throw new ArgumentException("Nieprawidłowa liczba wymaganych argumentów!");
                }
                bool fileExists = File.Exists(args[0]);
                if (!fileExists)
                {
                    Logger.log("Wystąpił FileNotFoundException: Plik " + args[0] + " nie istnieje");
                    throw new ArgumentException("Plik " + args[0] + " nie istnieje");
                }
                Uri uriResult;
                bool result = Uri.TryCreate(args[1], UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeFile);
                if (!result)
                {
                    Logger.log("Wystąpił ArgumentException: Podana ścieżka jest niepoprawna");
                    throw new ArgumentException("Podana ścieżka jest niepoprawna");
                }
                if (args[2] != "xml" && args[2] != "json")
                {
                    Logger.log("Wystąpił ArgumentException: Nieobsługiwany format danych");
                    throw new ArgumentException("Nieobsługiwany format danych");
                }

            }
            string inputPath = args[0];
            string outputPath = args[1];
            string format = args[2];

            //1. Tworzenie kolekcji studentów
            // Założenie - może być osoba z takim samym imieniem i nazwiskiem, więc unikalną wartością jest studencka ska
            List<Student> students = new List<Student>();
            HashSet<string> indexNumbers = new HashSet<string>();
            using (StreamReader sr = new StreamReader(inputPath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    bool hasMissingValue = false;
                    foreach (string field in fields)
                    {
                        if (String.IsNullOrEmpty(field))
                        {
                            hasMissingValue = true;
                            break;
                        }
                    }

                    if (hasMissingValue)
                    {
                        Logger.log("Błąd! Niepełne dane studenta");
                    }
                    else
                    {
                        bool addingSuccess = indexNumbers.Add(fields[4]);
                        if (!addingSuccess)
                        {
                            Logger.log("Błąd! Duplikat!");
                        }
                        else
                        {
                            var s = new Student
                            {
                                fName = fields[0],
                                lName = fields[1],
                                studies = new Studies(fields[2], fields[3]),
                                indexNumber = fields[4],
                                birthdate = DateTime.Parse(fields[5]).ToString("dd'.'MM'.'yyyy"),
                                email = fields[6],
                                mothersName = fields[7],
                                fathersName = fields[8],
                            };
                            students.Add(s);
                        }
                    }
                }
            }

            // 2. Serializacja
            if (format == "json")
            {
                //string strJson = JsonConvert.SerializeObject(students);

                var root = new
                {
                    uczelnia = new University(students)
                    {
                        
                    }
                };

                string json = JsonConvert.SerializeObject(root);
                string prettyJson = JToken.Parse(json).ToString(Formatting.Indented);
                //string strJson = JsonConvert.SerializeObject(university);
                File.WriteAllText("plik.json", prettyJson);
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(File.OpenWrite("plik.xml"), students);
            }


        }
    }
}
