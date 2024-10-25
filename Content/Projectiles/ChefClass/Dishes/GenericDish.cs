using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OlympusMod.Content.Items.ChefClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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
                float goOut = 8f;
                float radians = (MathF.Tau / ingredients.Length) * i;
                Vector2 offset = (Vector2.UnitY * goOut).RotatedBy(radians + Main.GlobalTimeWrappedHourly);
                Vector2 position = Projectile.Center + offset;
                Color color = Lighting.GetColor(position.ToTileCoordinates());
                Vector2 drawPosition = position - Main.screenPosition;
                Main.EntitySpriteDraw(texture, drawPosition, null, color, 0f, texture.Size() * 0.5f, 1f, SpriteEffects.None);
            }
            return false;
        }
    }
}
