using Aero.Gen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World.Zone.Layers
{
    [Aero]
    public partial class ZoneMeldingPerimiter : LayerData
    {
        [AeroString(typeof(int))]
        public string Name;
        public int NumControlPoints;
        public int NumBitfieldBits;
        [AeroArray(nameof(BitfieldLen))]
        public byte[] BitField;
        public uint Unk;
        [AeroArray(typeof(uint))]
        public Perimiter[] Perimiters;

        public int BitfieldLen => (int)Math.Ceiling(NumBitfieldBits / 8.0d);
    }

    [AeroBlock]
    public struct Perimiter
    {
        [AeroString(typeof(int))]
        public string PerimiterName;
    }
}
