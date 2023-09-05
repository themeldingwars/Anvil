using ImGuiNET;
using ImTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Veldrid.ImageSharp;
using Vortice.Mathematics;

namespace Anvil
{
    public class AssetPreview
    {
        private ImageSharpTexture testImage;

        public AssetPreview()
        {
            Stream resFilestream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Anvil.Resources.Icons.Ratchet.png");
            testImage = new ImageSharpTexture(resFilestream);
        }

        public void Draw()
        {
            if (ImGui.Begin("Asset Preview"))
            {
                var size = ImGui.GetContentRegionAvail();
                float ratio = Math.Max(testImage.Width / size.X, testImage.Height / size.Y);
                ImGui.Image(AnvilTool.Ref.Window.GetOrCreateTextureBinding(testImage), new Vector2(testImage.Width / ratio, testImage.Height / ratio));
                ImGui.End();
            }
        }
    }
}
