﻿using System;
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
        public int ID { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("time")]
        public int Time { get; set; } // X
        [Name("power")]
        public int Power { get; set; } // Y

    }
}
