using Anvil.Aero;
using Anvil.IO.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.ZoneEd.Inspectors
{
    public class GenericInspector : InspectorBase
    {
        public AeroInspector Inspector;

        public GenericInspector(Layer layer) : base(layer) { }

        public override void Draw()
        {
            Inspector.Draw();
        }

        public override void SetLayer(Layer layer)
        {
            base.SetLayer(layer);

            Inspector = new AeroInspector();
            Inspector.BuildList(Layer.Data);
        }
    }
}
