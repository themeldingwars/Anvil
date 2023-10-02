using ImGuiNET;
using ImTool;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anvil
{
    public class Anvil
    {

    }

    public class AnvilTool : Tool<AnvilTool, Config>
    {
        public static AnvilTool Ref;

        public AssetExplorerTab AssetExplorer;
        public ZoneEdTab ZoneEd;
        public LogWindow<LogCategories> LogWindow = new LogWindow<LogCategories>("Logs");

        private WindowButton ManageProjectWinButton;

        public ProjectManager ProjectManager;
        private NewProjectDialog NewProjectDialog;

        protected override bool Initialize(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm:ss} [{Category}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.AnvilSink()
                .CreateLogger();


            FontManager.RegisterResourceAssembly(Assembly.GetExecutingAssembly());
            FontManager.AddFont(new Font("Roboto-Regular", 18, new FontFile("Anvil.Resources.Fonts.Roboto-Regular.ttf", new Vector2(0, -1))));
            FontManager.AddFont(new Font("Roboto-Light", 18, new FontFile("Anvil.Resources.Fonts.Roboto-Light.ttf", new Vector2(0, -1))));
            FontManager.DefaultFont = "Roboto-Regular";

            NewProjectDialog = new NewProjectDialog();

            Ref = this;
            return true;
        }

        protected override void Load()
        {
            ProjectManager            = new ProjectManager();
            NewProjectDialog.OnCreate = ProjectManager.CreateProject;

            AssetExplorer = new AssetExplorerTab(this);
            Window.AddTab(AssetExplorer);

            ZoneEd = new ZoneEdTab(this);
            Window.AddTab(ZoneEd);

            Window.AddWindowButton("New Project", () => { NewProjectDialog.Show(); });
            Window.AddWindowButton("Load Project", () =>
            {
                ImGui.PushOverrideID(0);
                FileBrowser.OpenFile((path) =>
                {
                    ProjectManager.LoadProject(path);
                }, null, "*.anvil");
                ImGui.PopID();
            });

            Window.OnSubmitUIExtension += SubmitUiExtension;
        }

        protected void SubmitUiExtension()
        {
            ImGui.PushOverrideID(0);
            FileBrowser.Draw();
            ImGui.PopID();

            NewProjectDialog.Draw();
        }

        protected override void Unload()
        {

        }
    }
}
