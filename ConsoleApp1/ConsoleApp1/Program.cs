using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public const string w = "wdw";
        public readonly int dw = 3;

        public Program()
        {
            dw = 8;
        }

        public static void Main()
        {
            Program wg = new Program(); 
            Console.WriteLine(wg.dw);
            Console.ReadLine();
        }
    }
}
