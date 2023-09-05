using ImGuiNET;
using ImTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Anvil
{
    public class AssetExplorerTab : WorkspaceTab
    {
        public override string Name { get; } = "Asset Explorer";
        protected override WorkspaceFlags Flags { get; } = WorkspaceFlags.HideTabBar;
        public override string WorkspaceName => "Asset Explorer";
        public override ImGuiDockNodeFlags DockSpaceFlags => ImGuiDockNodeFlags.CentralNode;

        private AnvilTool Anvil;
        private AssetBrowser AssetBrowser;
        private AssetPreview AssetPreview;

        public AssetExplorerTab(AnvilTool anvil)
        {
            Anvil        = anvil;
            AssetBrowser = new AssetBrowser();
            AssetPreview = new AssetPreview();
        }

        public override void Load()
        {

        }

        protected override void CreateDockSpace(Vector2 size)
        {
            ImGui.DockBuilderSplitNode(DockSpaceID, ImGuiDir.Left, 0.2f, out var leftId, out var rightId);
            ImGui.DockBuilderSplitNode(rightId, ImGuiDir.Down, 0.2f, out var rightBottomId, out var rightTopId);
            ImGui.DockBuilderSplitNode(leftId, ImGuiDir.Down, 0.3f, out var leftBottomId, out var leftTopId);

            ImGui.DockBuilderDockWindow("Assets", leftTopId);
            ImGui.DockBuilderDockWindow("Asset Preview", leftBottomId);
            ImGui.DockBuilderDockWindow("Logs###AssetExplorer", rightBottomId);
            //ImGui.DockBuilderDockWindow("Workspace", rightTopId);
        }

        public override void Unload()
        {

        }

        protected override unsafe void SubmitContent()
        {
            AssetBrowser.Draw();
            AssetPreview.Draw();

            Anvil.LogWindow.Name = "Logs###AssetExplorer";
            Anvil.LogWindow.DrawWindow();
        }

        protected override void SubmitWorkspaceContent()
        {
            
        }
    }
}
