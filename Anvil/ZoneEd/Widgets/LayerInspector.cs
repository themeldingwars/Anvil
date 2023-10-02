using Anvil.IO.World;
using Anvil.ZoneEd.Inspectors;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.ZoneEd.Widgets
{
    public class LayerInspector
    {
        public Layer Layer;
        public InspectorBase Inspector;

        public void SetLayer(Layer layer)
        {
            Layer = layer;
            Inspector = CreateInspectorForLayer(layer);
        }

        public void Draw()
        {
            if (ImGui.Begin("Inspector"))
            {
                Inspector?.Draw();
                ImGui.End();
            }
        }

        private InspectorBase CreateInspectorForLayer(Layer layer)
        {
            var inspector = layer.LayerType switch
            {
                _ => new UnknownInspector(layer)
            };

            return inspector;
        }
    }
}
