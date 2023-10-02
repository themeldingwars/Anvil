using Aero.Gen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World.Zone.Layers
{
    [Aero]
    public partial class ChunkZoneRange : LayerData
    {
        public uint CubeFaceId;

        public uint MinX;
        public uint MinY;

        public uint MaxX;
        public uint MaxY;
    }
}
