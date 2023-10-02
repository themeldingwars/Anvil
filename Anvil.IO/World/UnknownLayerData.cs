using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World
{
    // A generic block of data on a layer, should be used if the type isn't known
    public class LayerDataUnknown : LayerData
    {
        public byte[] Data;

        public int Pack(Span<byte> buffer)
        {
            Data.AsSpan().CopyTo(buffer);
            return Data.Length;
        }

        public int Unpack(ReadOnlySpan<byte> data)
        {
            Data = new byte[data.Length];
            data.CopyTo(Data);
            return data.Length;
        }

        public int GetPackedSize() => Data.Length;
    }
}
