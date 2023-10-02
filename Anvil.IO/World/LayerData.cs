using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World
{
    // Base class for layer data, should have nothing
    public interface LayerData
    {
        public int Pack(Span<byte> buffer);
        public int Unpack(ReadOnlySpan<byte> data);
        public int GetPackedSize();
    }
}
