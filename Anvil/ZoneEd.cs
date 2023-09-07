using ImGuiNET;
using ImTool;
using ImTool.Scene3D;
using System.Collections.Generic;
using System.Numerics;

namespace Anvil
{
    public class ZoneEdTab : Tab
    {
        public override string Name { get; } = "ZoneEd";
        //public override string WorkspaceName => "ZoneEd";
        //protected override WorkspaceFlags Flags { get; } = WorkspaceFlags.HideTabBar;
        private AnvilTool Anvil;
        private World EditorWorld;
        private ZoneEdScene MainView;
        private Dictionary<string, ZoneEdScene> Viewports = new Dictionary<string, ZoneEdScene>();

        public ZoneEdTab(AnvilTool anvil)
        {
            Anvil       = anvil;
            CreateWorld();
            MainView    = AddViewport("Main View");
            EditorWorld.Init(MainView);
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
            ImGui.DockBuilderDockWindow("Main View", rightTopId);
        }

        protected override unsafe void SubmitContent()
        {
            EditorWorld.Tick();

            foreach (var viewport in Viewports)
            {
                viewport.Value.DrawWindow(viewport.Key);
            }

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

        private ZoneEdScene AddViewport(string name)
        {
            var view = new ZoneEdScene(Anvil.Window, EditorWorld);
            EditorWorld.RegisterViewport(view);
            view.GetCamera().Transform.Position = new Vector3(-9.887855f, 7.343468f, 11.137617f);
            view.GetCamera().Transform.Rotation = new Quaternion(0.0794969f, 0.9064547f, -0.1976312f, 0.36463875f);

            Viewports.Add(name, view);
            return view;
        }

        private void CreateWorld()
        {
            EditorWorld = new World(Anvil.Window);
        }
    }
}
