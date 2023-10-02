using Anvil.IO.World;
using Microsoft.CodeAnalysis.Diagnostics;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.Zone
{
    public class Zone
    {
        public Header Header = new Header();
        public Layer Root    = new();

        public Zone()
        {

        }

        public Zone(string path)
        {
            Load(path);
        }

        public void Load(string filePath)
        {
            var data = File.ReadAllBytes(filePath);
            Load(data);
        }

        public void Load(ReadOnlySpan<byte> data)
        {
            var sw = Stopwatch.StartNew();
            var offset = Header.Unpack(data);
            Logging.Log.Information("Header: {Header}", Header);

            Root.Read(data[offset..]);
            sw.Stop();

            Logging.Log.Information("Loaded zone: {zoneName} in {loadTime}", Header.Name[..^1], sw.Elapsed);
        }
    }
}
