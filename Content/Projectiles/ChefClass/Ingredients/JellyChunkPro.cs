using NVorbis.Contracts;
using OlympusMod.Content.Items.ChefClass;
using OlympusMod.Core.ChefClass;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace OlympusMod.Content.Projectiles.ChefClass.Ingredients
{
    public class JellyChunkPro : ChefProjectile
    {
        public override IEnumerable<FlavorType> GetFlavors()
        {
            yield return FlavorType.Salty;
        }
        public override void SetDefaults()
        {
            Projectile.height = Projectile.width = 12;
            Projectile.DamageType = OlympusMod.CookClass;
            DrawOffsetX = -6;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 velocity = -Projectile.velocity.RotatedByRandom(1f);
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.t_Slime, velocity.X, velocity.Y, 0, Color.DodgerBlue);
                d.scale *= 1.5f;
            }
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
        }
    }
}
