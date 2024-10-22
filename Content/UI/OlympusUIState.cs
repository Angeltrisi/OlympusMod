using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;

namespace OlympusMod.Content.UI
{
    public abstract class OlympusUIState : UIState
    {
        protected internal virtual UserInterface UserInterface { get; set; }
        public abstract int InsertionIndex(List<GameInterfaceLayer> layers);
        public virtual bool Visible { get; set; } = false;
        public virtual InterfaceScaleType Scale { get; set; } = InterfaceScaleType.UI;
        public virtual void Unload() { }
    }
}
