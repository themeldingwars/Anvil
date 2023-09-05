using ImGuiNET;
using ImTool;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vulkan;

namespace Anvil
{
    public class NewProjectDialog
    {
        public string Name        = "";
        public string GameInstall = "";
        public string ProjectDir  = "";

        public Action<string, string, string> OnCreate;
        public Action OnCanel;
        private bool ShouldShow = false;

        public void Show(bool show = true)
        {
            ShouldShow = show;

            Name        = "";
            GameInstall = "";
            ProjectDir  = "";
        }

        public unsafe void Draw()
        {
            if (ShouldShow)
            {
                ImGui.OpenPopup("New Project", ImGuiPopupFlags.AnyPopup);
            }

            ImGui.SetNextWindowSize(new Vector2(500, 150));
            if (ImGui.BeginPopupModal("New Project", ref ShouldShow, ImGuiWindowFlags.NoResize))
            {
                var size = ImGui.GetWindowSize();

                ImGui.Text("Project Name ");
                ImGui.SameLine();
                ImGui.InputText("###ProjectName", ref Name, 200);

                ImGui.Text("Game Install   ");
                ImGui.SameLine();
                ImGui.InputText("###GameInstall", ref GameInstall, 1024);
                ImGui.SameLine();
                if (ImGui.Button("...###a"))
                {
                    FileBrowser.SelectDir((path) =>
                    {
                        GameInstall = path;
                    });
                }

                ImGui.SetNextItemWidth(450);
                ImGui.Text("Project Dir      ");
                ImGui.SameLine();
                ImGui.InputText("###ProjectDir", ref ProjectDir, 1024);
                ImGui.SameLine();
                if (ImGui.Button("...###b"))
                {
                    FileBrowser.SelectDir((path) =>
                    {
                        ProjectDir = path;
                    });
                }

                var buttonWidth = size.X / 2 - 10;
                ImGui.Spacing();
                ImGui.PushStyleColor(ImGuiCol.Button, ImToolColors.RGBAToBGR(ImGui.ColorConvertU32ToFloat4(0x007700FF)));
                if (ImGui.Button("Create", new Vector2(buttonWidth, 0)))
                {
                    OnCreate?.Invoke(Name, GameInstall, ProjectDir);
                }
                ImGui.PopStyleColor();

                ImGui.SameLine();

                if (ImGui.Button("Cancel", new Vector2(buttonWidth, 0)))
                    ShouldShow = false;

                FileBrowser.Draw();
                ImGui.EndPopup();
            }
        }
    }
}
