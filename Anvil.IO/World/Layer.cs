using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.IO.World
{
    public class Layer
    {
        public int LayerTypeId;
        public LayerType LayerType;
        public LayerData Data;
        public List<Layer> SubLayers = new();

        public int Read(ReadOnlySpan<byte> buffer, int parentId = -1)
        {
            int offset  = 0;
            var header  = LayerHeader.Read(buffer);
            LayerTypeId = header.LayerId;
            offset      = LayerHeader.SIZE;

            // Get layer type and data class
            var (layerType, layerDataClass) = GetLayerTypeAndDataClass(parentId, LayerTypeId);
            LayerType = layerType;

            // Read the layer data
            var subData   = buffer.Slice(offset, header.Length);
            var layerData = GetLayerData(subData);
            offset       += layerData.Length;

            layerDataClass.Unpack(layerData);
            Data = layerDataClass;

            //Logging.Log.Information("Read layer type: {LayerType} ({parentId}:{layerTypeId}), payload data size: {payloadDataSize}", LayerType, parentId, LayerTypeId, layerData.Length);

            // Sublayers
            while (offset + LayerHeader.SIZE < header.Length)
            {
                var subLayer     = new Layer();
                var subLayerSize = subLayer.Read(buffer[offset..], LayerTypeId);
                SubLayers.Add(subLayer);
                offset += subLayerSize;
            }

            return offset;
        }

        // Scan for the layer marker and return its offset
        private int GetLayerMarkerOffset(ReadOnlySpan<byte> buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                if (i + 8 > buffer.Length) return -1;
                var nodeMarker = MemoryMarshal.Cast<byte, ulong>(buffer.Slice(i, 8));

                if (nodeMarker.Length >= 1 && nodeMarker[0] == LayerHeader.LAYER_MARKER_VALUE)
                {
                    return i;
                }
            }

            return -1;
        }

        // Non sub layer data for this node
        private ReadOnlySpan<byte> GetLayerData(ReadOnlySpan<byte> data)
        {
            var subNodeOffset = GetLayerMarkerOffset(data);
            if (subNodeOffset != -1) return data[..subNodeOffset];

            return data;
        }

        private (LayerType layerType, LayerData layerDataType) GetLayerTypeAndDataClass(int parentLayerId, int layerId)
        {
            var result = (parentLayerId, layerId) switch
            {
                (_,         0x30000)   => (LayerType.Zone,                              new LayerDataUnknown()),
                (_,         0x20000)   => (LayerType.ZoneSkybox,                        new LayerDataUnknown()),
                (_,         0x20100)   => (LayerType.ZoneDefaultEnviroment,             new LayerDataUnknown()),
                (_,         0x02710)   => (LayerType.ZoneDefaultEnviroment_1000,        new LayerDataUnknown()),
                (_,         0x20200)   => (LayerType.ZoneMelding,                       new LayerDataUnknown()),
                (0x20200,   0x5)       => (LayerType.ZoneMeldingPerimiter,              new LayerDataUnknown()),
                (_,         0x20300)   => (LayerType.ZoneWater,                         new LayerDataUnknown()),
                (0x20300,   0x4)       => (LayerType.ZoneWaterChild,                    new LayerDataUnknown()),
                (_,         0x20400)   => (LayerType.ZoneChunkInfo,                     new LayerDataUnknown()),
                (0x20400,   0x10000)   => (LayerType.ZoneChunkInfoRange,                new LayerDataUnknown()),
                (0x20400,   0x10101)   => (LayerType.ZoneChunkInfoRef,                  new LayerDataUnknown()),
                (0x20400,   0x10100)   => (LayerType.ZoneChunkInfoRef2,                 new LayerDataUnknown()),
                (_,         0x20700)   => (LayerType.ZoneMeldingHeightMap,              new LayerDataUnknown()),
                (_,         0x20800)   => (LayerType.ZonePath,                          new LayerDataUnknown()),
                (_,         0x20900)   => (LayerType.ZoneWorldChunkImport,              new LayerDataUnknown()),
                (_,         0x21000)   => (LayerType.Zone_135168,                       new LayerDataUnknown()),
                (_,         0x21200)   => (LayerType.ZonePropEncNameRegistry,           new LayerDataUnknown()),
                (_,         0x21400)   => (LayerType.ZoneProp,                          new LayerDataUnknown()),
                (_,         0x21500)   => (LayerType.ZoneCameraSequence,                new LayerDataUnknown()),
                (_,         0x21600)   => (LayerType.ZoneTransferBounds,                new LayerDataUnknown()),
                (_,         0x21700)   => (LayerType.ZoneSubzoneRegion,                 new LayerDataUnknown()),
                _                      => (LayerType.Unknown,                           new LayerDataUnknown())
            };

            return result;
        }
    }
}
