namespace OlympusMod.Content.Items.ChefClass
{
    public enum FlavorType
    {
        Bland,
        Sweet,
        Salty,
        Sour,
        Bitter,
        Spicy,
        Umami
    }
    public abstract class Ingredient : ModItem
    {
        internal int IngredientType;
        public abstract FlavorType[] Flavors { get; }
    }
}
