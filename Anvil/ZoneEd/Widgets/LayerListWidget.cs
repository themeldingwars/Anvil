using Anvil.IO.World;
using ImGuiNET;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.ZoneEd.Widgets
{
    public class LayerListWidget
    {
        public Action<Layer> OnLayerSelected;
        public Layer SelectedLayer = null;

        public void Draw(EditableZone EditZone)
        {
            if (ImGui.Begin("Layers"))
            {
                DrawLayersList(EditZone);
                ImGui.End();
            }
        }

        private void DrawLayersList(EditableZone EditZone)
        {
            if (EditZone == null)
                return;

            if (ImGui.TreeNode($"Zone ({EditZone.Zone.Header.Name[..^1]})"))
            {
                int idx = 0;
                DrawLayerEntry(EditZone.Zone.Root, ref idx);
                ImGui.TreePop();
            }
        }

        private void DrawLayerEntry(Layer layer, ref int idx)
        {
            foreach (var subLayer in layer.SubLayers)
            {
                var name         = subLayer.LayerType == LayerType.Unknown ? $"{subLayer.LayerType} ({layer.LayerTypeId})" : $"{subLayer.LayerType}";
                var hasSubLayers = subLayer.SubLayers != null && subLayer.SubLayers.Count > 0;
                var treeFlags    = (hasSubLayers ? ImGuiTreeNodeFlags.None : ImGuiTreeNodeFlags.Leaf) | ImGuiTreeNodeFlags.OpenOnArrow;
                treeFlags |= (subLayer == SelectedLayer ? ImGuiTreeNodeFlags.Selected : ImGuiTreeNodeFlags.None);

                var isLayerOpen = ImGui.TreeNodeEx($"{name}###{idx++}", treeFlags);
                var isClicked   = ImGui.IsItemClicked();

                if (ImGui.IsItemClicked())
                {
                    SelectedLayer = subLayer;
                    OnLayerSelected?.Invoke(subLayer);
                }

                if (isLayerOpen)
                {
                    DrawLayerEntry(subLayer, ref idx);
                    ImGui.TreePop();
                }
            }
        }
    }
}
