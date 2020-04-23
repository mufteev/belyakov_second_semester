using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace lab_7
{
    public class CSVtype
    {
        [Name("id")]
        public long ID { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("time")]
        public int Time { get; set; }
        [Name("power")]
        public int Power { get; set; }

    }
}
