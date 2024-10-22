using OlympusMod.Utilities;

namespace OlympusMod.Content.Items.ChefClass.Ingredients
{
    public class JellyChunk : Ingredient
    {
        public override FlavorType[] Flavors => [FlavorType.Salty];
        public override void SetDefaults()
        {
            Item.DefaultToIngredient(32, 32, ModContent.ProjectileType<JellyChunkPro>(), 5f, 32, 16);
        }
    }
}
