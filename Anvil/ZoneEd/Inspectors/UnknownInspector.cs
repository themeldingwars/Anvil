using Anvil.IO.World;
using ImTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ImTool.HexView;

namespace Anvil.ZoneEd.Inspectors
{
    public class UnknownInspector : InspectorBase
    {
        private HexView HexView = new HexView();

        public UnknownInspector(Layer layer) : base(layer) { }

        public override void SetLayer(Layer layer)
        {
            base.SetLayer(layer);
            HexView.ShowSideParsedValues = false;
            HexView.ShowParsedValuesInTT = false;
            var unkLayer = layer.Data as LayerDataUnknown;

            if (layer.Data != null)
            {
                HexView.SetData(unkLayer.Data, Array.Empty<HighlightSection>());
            }
        }

        public override void Draw()
        {
            HexView.Draw();
        }
    }
}
