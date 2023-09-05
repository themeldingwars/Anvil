using ImGuiNET;
using ImTool;
using System.Numerics;

namespace Anvil
{
    public class ZoneEdTab : WorkspaceTab
    {
        public override string Name { get; } = "ZoneEd";
        public override string WorkspaceName => "ZoneEd";
        protected override WorkspaceFlags Flags { get; } = WorkspaceFlags.HideTabBar;
        private AnvilTool Anvil;

        public ZoneEdTab(AnvilTool anvil)
        {
            Anvil = anvil;
        }

        public override void Load()
        {

        }

        public override void Unload()
        {

        }

        protected override void CreateDockSpace(Vector2 size)
        {
            ImGui.DockBuilderSplitNode(DockSpaceID, ImGuiDir.Left, 0.2f, out var leftId, out var rightId);
            ImGui.DockBuilderSplitNode(rightId, ImGuiDir.Down, 0.2f, out var rightBottomId, out var rightTopId);

            ImGui.DockBuilderDockWindow("Layers", leftId);
            ImGui.DockBuilderDockWindow("Logs###ZoneEd", rightBottomId);
            //ImGui.DockBuilderDockWindow("Workspace", rightTopId);
        }

        protected override unsafe void SubmitContent()
        {

        }

        protected override void SubmitWorkspaceContent()
        {
            if (ImGui.Begin("Layers"))
            {
                ImGui.End();
            }

            Anvil.LogWindow.Name = "Logs###ZoneEd";
            Anvil.LogWindow.DrawWindow();
        }

        protected override void SubmitMainMenu()
        {
            if (ImGui.BeginMenu("File"))
            {

                ImGui.EndMenu();
            }
        }
    }
}
