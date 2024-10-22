using Microsoft.Xna.Framework;
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
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 32;
            Item.width = Item.height = 30;
        }
        public sealed override bool CanShoot(Player player)
        {
            return player.GetChefPlayer().HasFullIngredients;
        }
        public sealed override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int shouldShootDishType = ModContent.ProjectileType<GenericDish>();
            ChefPlayer cook = player.GetChefPlayer();

            Ingredient[] ingredients = cook.recipeSlots[cook.selectedRecipeSlot].items.Select(i => i.ModItem as Ingredient).ToArray();

            if (cook.GetCurrentSpecificDish(out int dishType))
                shouldShootDishType = dishType;
            Projectile pro = Projectile.NewProjectileDirect(source, position, velocity, shouldShootDishType, damage, knockback, player.whoAmI);
            if (pro.ModProjectile is GenericDish dish)
            {
                dish.ingredients = ingredients;
            }
            cook.ConsumeIngredients(ingredients.Select(i => i.Type).ToArray());

            return false;
        }
    }
}
