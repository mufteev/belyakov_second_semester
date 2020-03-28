using lab_5_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var teams = WebHelperSports.GetSportTable("https://www.sports.ru/rfpl/table/");

            WebHelperSports.FlushToDisk(teams, "TableSport.csv");
        }
    }
}
