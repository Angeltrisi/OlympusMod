using OlympusMod.Content.Items.ChefClass;
using OlympusMod.Core.ChefClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace OlympusMod.Content.Projectiles.ChefClass
{
    public abstract class ChefProjectile : ModProjectile
    {
        public virtual IEnumerable<FlavorType> GetFlavors()
        {
            yield return FlavorType.Bland;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X / 16f;
            Projectile.velocity.Y += 0.1f;
        }
        public IngredientRecipe CreateIngredientRecipe()
        {
            return new IngredientRecipe([], [], Type);
        }
    }
}
