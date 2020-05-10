using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{

    public class StatusResponse
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = string.Empty;

        public object Result { get; set; } = null;
    }
}
