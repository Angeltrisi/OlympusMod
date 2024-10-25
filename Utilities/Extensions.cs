using OlympusMod.Core.ChefClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OlympusMod.Utilities
{
    public static class Extensions
    {
        public static void DefaultToIngredient(this Item item, int newWidth, int newHeight, int ingredientType, float shootSpeed, int useTime, int damage, SoundStyle? useSound = null)
        {
            item.knockBack = 0f;
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.Swing;
            item.width = newWidth;
            item.height = newHeight;
            item.damage = damage;
            item.shoot = ingredientType;
            item.useTime = item.useAnimation = useTime;
            item.shootSpeed = shootSpeed;
            item.DamageType = DamageClass.Melee;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.UseSound = useSound ?? SoundID.Item1;
            item.DamageType = ModContent.GetInstance<ChefClass>();
            item.maxStack = 99;
        }
        public static void DefaultToCookware(this Item item, int newWidth, int newHeight, int useTime, float shootSpeed, int damage)
        {
            item.width = newWidth;
            item.height = newHeight;

            item.useStyle = ItemUseStyleID.Shoot;
            item.useTime = item.useAnimation = useTime;

            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = shootSpeed;

            item.DamageType = OlympusMod.CookClass;
            item.damage = damage;
            item.noMelee = true;
        }
        public static bool Exists(this Item item) => item != null && !item.IsAir;
        public static ChefPlayer GetChefPlayer(this Player player) => player.GetModPlayer<ChefPlayer>();
        public static bool IsLocalPlayer(this Player player) => player.whoAmI == Main.myPlayer;
        public static bool Exists(this Player player) => player.active && !player.dead && player != null;
    }
}
