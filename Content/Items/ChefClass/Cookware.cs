using Microsoft.Xna.Framework;
using Mono.Cecil;
using MonoMod.RuntimeDetour;
using OlympusMod.Content.Projectiles.ChefClass.Dishes;
using OlympusMod.Core.ChefClass;
using OlympusMod.Utilities;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace OlympusMod.Content.Items.ChefClass
{
    public abstract class Cookware : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override bool CanUseItem(Player player) => CanShoot(player);
        public sealed override bool CanShoot(Player player)
        {
            ChefPlayer chef = player.GetChefPlayer();
            return chef.recipeSlots[chef.selectedRecipeSlot].HasFullIngredients;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<GenericDish>();

            if (player.GetChefPlayer().GetCurrentSpecificDish(out int dishType))
                type = dishType;
        }
        public sealed override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile pro = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);

            ChefPlayer chef = player.GetChefPlayer();

            Ingredient[] ingredients = chef.recipeSlots[chef.selectedRecipeSlot].items.Select(i => i.ModItem as Ingredient).ToArray();

            if (pro.ModProjectile is GenericDish dish)
            {
                dish.ingredients = ingredients;
            }
            chef.ConsumeIngredients(chef.selectedRecipeSlot);

            return false;
        }
    }
}
