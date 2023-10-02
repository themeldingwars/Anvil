using Aero.Gen.Attributes;

namespace Anvil.IO.Zone
{
    [Aero]
    public partial class Header
    {
        public uint Magic;
        public int Version;
        public ulong Timestamp;
        [AeroString(typeof(int))]
        public string Name;

        public override string ToString()
        {
            var str = $"Magic: {Magic}, Version: {Version}, Timestamp: {Timestamp}, Name: {Name[..^1]}";
            return str;
        }
    }
}
