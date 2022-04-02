using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Logger
    {

        public static void log(string message)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine(message);
            };


        }
    }
}
