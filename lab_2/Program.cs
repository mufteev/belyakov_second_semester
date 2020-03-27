using lab_2_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    class Program
    {
        static void Main()
        {
            Console.ReadLine();

            foreach (string item in Utils_2.GetFilesAndFolders(AppDomain.CurrentDomain.BaseDirectory))
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
