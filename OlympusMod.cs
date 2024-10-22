global using Terraria.ModLoader;
global using OlympusMod.Content.Projectiles.ChefClass.Ingredients;
global using Microsoft.Xna.Framework;
using OlympusMod.Core.ChefClass;

namespace OlympusMod
{
    public class OlympusMod : Mod
	{
        public static OlympusMod Instance;
        public OlympusMod() => Instance = this;
        public const string BlankTexture = "OlympusMod/Content/BlankTexture";
        public static DamageClass CookClass { get { return ModContent.GetInstance<ChefClass>(); } }
        public override void Load()
        {
            
        }
        public override void Unload()
        {
            Instance = null;
        }
    }
}
