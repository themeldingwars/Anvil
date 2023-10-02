using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World
{
    public enum LayerType
    {
        Unknown,
        Root,
        Zone,
        ZoneSkybox,
        ZoneDefaultEnviroment,
        ZoneDefaultEnviroment_1000,
        ZoneMelding,
        ZoneMeldingPerimiter,
        ZoneWater,
        ZoneWaterChild,
        ZoneChunkInfo,
        ZoneChunkInfoRange,
        ZoneChunkInfoRef,
        ZoneChunkInfoRef2,
        ZoneMeldingHeightMap,
        ZonePath,
        ZoneWorldChunkImport,
        Zone_135168,
        ZonePropEncNameRegistry,
        ZoneProp,
        ZoneCameraSequence,
        ZoneTransferBounds,
        ZoneSubzoneRegion
    }
}
