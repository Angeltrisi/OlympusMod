using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace OlympusMod.Core
{
    public class OlympusConfig : ModConfig
    {
        public static OlympusConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        public bool ChefUseFallbackRecipe;
        public override void OnLoaded()
        {
            Instance = this;
        }
    }
}
