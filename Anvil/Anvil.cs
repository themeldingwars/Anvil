using ImTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil
{
    public class Anvil
    {

    }

    public class AnvilTool : Tool<AnvilTool, Config>
    {
        protected override bool Initialize(string[] args)
        {
            return true;
        }

        protected override void Load()
        {
            Window.AddWindowButton("Test button", () =>
            {
                Console.WriteLine("Test window button clicked :>");
            });
        }

        protected override void Unload()
        {

        }
    }
}
