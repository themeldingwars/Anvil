using Anvil.IO.World;
using Anvil.ZoneEd.Inspectors;
using ImGuiNET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.ZoneEd.Widgets
{
    public class LayerInspector
    {
        public Layer Layer;
        public InspectorBase Inspector;
        public string JsonView = null;

        public void SetLayer(Layer layer)
        {
            Layer = layer;
            Inspector = CreateInspectorForLayer(layer);

            if (JsonView != null)
            {
                JsonView = JsonConvert.SerializeObject(Layer.Data, Formatting.Indented);
            }
        }

        public void Draw()
        {
            if (ImGui.Begin("Inspector"))
            {
                if (Layer != null)
                {
                    ImGui.Text($"{Layer.LayerType} ({Layer.LayerTypeId})");
                    ImGui.SameLine();
                    if (ImGui.Button("Json", new Vector2(50, 0)))
                    {
                        JsonView = JsonView == null ? JsonConvert.SerializeObject(Layer.Data, Formatting.Indented) : null;
                    }

                    if (JsonView != null)
                    {
                        ImGui.TextWrapped(JsonView);
                    }
                    else
                    {
                        Inspector?.Draw();
                    }
                }
                ImGui.End();
            }
        }

        private InspectorBase CreateInspectorForLayer(Layer layer)
        {
            InspectorBase inspector = layer.Data switch
            {
                LayerDataUnknown => new UnknownInspector(layer),
                _                => new GenericInspector(layer)
            };

            return inspector;
        }
    }
}
