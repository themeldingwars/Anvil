using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil
{
    public class Project
    {
        public string Name;
        public string GameInstallPath;

        public override string ToString()
        {
            var str = $"{Name} at {GameInstallPath}";
            return str ;
        }
    }
}
