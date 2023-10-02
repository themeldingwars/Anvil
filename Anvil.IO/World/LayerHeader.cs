using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World
{
    public struct LayerHeader
    {
        public const ulong LAYER_MARKER_VALUE = 0x12ED5A12ED5B12ED;
        public const int SIZE                 = 16;

        public ulong LayerMarker;
        public int LayerId;
        public int Length;

        public static LayerHeader Read(ReadOnlySpan<byte> data)
        {
            var header = MemoryMarshal.Cast<byte, LayerHeader>(data[..16])[0];
            return header;
        }
    }
}
