using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO
{
    public static class Logging
    {
        public static Serilog.ILogger Log
        {
            get
            {
                var log = Serilog.Log.ForContext("Category", "Anvil.IO");
                return log;
            }
        }
    }
}
