using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OlympusMod.Content.Items.ChefClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;

namespace OlympusMod.Content.Projectiles.ChefClass.Dishes
{
    public class GenericDish : ChefProjectile
    {
        public override string Texture => OlympusMod.BlankTexture;
        public Ingredient[] ingredients = new Ingredient[3];
        public override IEnumerable<FlavorType> GetFlavors()
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                Ingredient ingredient = ingredients[i];
                for (int j = 0; j < ingredient.Flavors.Length; j++)
                    yield return ingredient.Flavors[j];
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                Ingredient ingredient = ingredients[i];
                Texture2D texture = TextureAssets.Item[ingredient.Type].Value;

            }
            return false;
        }
    }
}
