using ImTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
            Window.AddWindowButton("Load Project", () => { });

            Window.OnSubmitUIExtension += SubmitUiExtension;
        }

        protected void SubmitUiExtension()
        {
            FileBrowser.Draw();
            NewProjectDialog.Draw();
        }

        protected override void Unload()
        {

        }
    }
}
