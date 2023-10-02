using Anvil.IO.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.ZoneEd.Inspectors
{
    public class InspectorBase
    {
        protected Layer Layer;

        public InspectorBase(Layer layer)
        {
            SetLayer(layer);
        }

        public virtual void SetLayer(Layer layer)
        {
            Layer = layer;
        }

        public virtual void Draw()
        {

        }
    }
}
