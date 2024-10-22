using Terraria.ID;

namespace OlympusMod.Content.Items.ChefClass.Cookwares
{
    public class IronSkillet : Cookware
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 32;
            Item.width = Item.height = 30;
            Item.damage = 16;
            Item.DamageType = OlympusMod.CookClass;
        }
    }
}
