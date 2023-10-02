using Aero.Gen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World.Zone.Layers
{
    [Aero]
    public partial class ChunkRef2 : LayerData
    {
        public int X;
        public int Y;
        public int RecordId;
    }
}
