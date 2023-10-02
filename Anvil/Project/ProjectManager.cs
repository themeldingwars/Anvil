using Newtonsoft.Json;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Anvil
{
    public class ProjectManager
    {
        public Project CurrentProject { get; private set; }

        public void LoadProject(string path)
        {
            var json       = File.ReadAllText(path);
            CurrentProject = JsonConvert.DeserializeObject<Project>(json);

            Logging.LogInfo(LogCategories.Default, "Loaded project ({name}) from {path}", CurrentProject.Name, path);
        }

        public void SaveProject(string path, Project proj = null)
        {
            var json = JsonConvert.SerializeObject(proj ?? CurrentProject);
            File.WriteAllText(path, json);
        }

        public void CreateProject(string name, string gameInstallPath, string projectPath)
        {
            var proj = new Project()
            {
                Name            = name,
                GameInstallPath = gameInstallPath
            };

            var path = Path.Combine(projectPath, $"{name}.anvil");
            SaveProject(path, proj);
            CurrentProject = proj;
        }
    }
}
