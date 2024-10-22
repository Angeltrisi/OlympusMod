using OlympusMod.Utilities;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using Terraria.ID;

namespace OlympusMod.Core.ChefClass
{
    public class ChefPlayer : ModPlayer
    {
        public static ModKeybind SwitchActiveRecipeKey { get; private set; } = null;
        // change this to whatever once more are needed. this automatically handles all UI related stuff too.
        public const int AMOUNT_OF_RECIPE_SLOTS = 9;

        public RecipeContainer[] recipeSlots;
        public int recipeSlotsVisible;
        public int selectedRecipeSlot = 0;

        public bool HasFullIngredients { get { return recipeSlots.Any(r => r.HasFullIngredients); } }
        public float ChanceNotToConsumeIngredients = 0f;
        public override void Load()
        {
            SwitchActiveRecipeKey = KeybindLoader.RegisterKeybind(Mod, "SwitchActiveRecipeKey", Keys.Q);
        }
        public override void Initialize()
        {
            recipeSlots = new RecipeContainer[AMOUNT_OF_RECIPE_SLOTS];
            for (int i = 0; i < recipeSlots.Length; i++)
            {
                recipeSlots[i] = new RecipeContainer();
            }
            recipeSlotsVisible = 3;
        }
        public override void SaveData(TagCompound tag)
        {
            // instead of directly saving the recipe slots amount, save bools that add onto the recipe slots amount.
            for (int i = 0; i < recipeSlots.Length; i++)
            {
                RecipeContainer r = recipeSlots[i];
                r ??= new RecipeContainer();
                for (int j = 0; j < 3; j++)
                {
                    tag.Add($"ingredientItem{i}.{j}", r[j]);
                }
            }
        }
        public override void LoadData(TagCompound tag)
        {
            // hopefully this wack ahh implementation doesn't break stuff
            for (int i = 0; i < recipeSlots.Length; i++)
            {
                RecipeContainer r = recipeSlots[i];
                for (int j = 0; j < 3; j++)
                {
                    r[j] = tag.Get<Item>($"ingredientItem{i}.{j}");
                }
            }
        }
        public override void ResetEffects()
        {
            recipeSlotsVisible = 3;
            ChanceNotToConsumeIngredients = 0f;
        }
        private bool TrySwitchRecipe()
        {
            return Player.IsLocalPlayer() && SwitchActiveRecipeKey.JustPressed;
        }
        private void SwitchRecipe()
        {
            selectedRecipeSlot = ++selectedRecipeSlot % recipeSlotsVisible;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        public override void PostUpdate()
        {
            if (TrySwitchRecipe())
            {
                SwitchRecipe();
            }
        }
        public bool GetCurrentSpecificDish(out int dishType)
        {
            if (!HasFullIngredients)
            {
                dishType = -1;
                return false;
            }
            RecipeContainer rS = recipeSlots[selectedRecipeSlot];
            if (IngredientLoader.ContainsRecipe(out int result, rS) && rS.Active)
            {
                dishType = result;
                return true;
            }
            else if (OlympusConfig.Instance.ChefUseFallbackRecipe)
            {
                for (int i = 0; i < recipeSlots.Length; i++)
                {
                    RecipeContainer r = recipeSlots[i];
                    if (!r.Active) continue;
                    if (IngredientLoader.ContainsRecipe(out int result0, r))
                    {
                        dishType = result0;
                        return true;
                    }
                }
            }
            dishType = -1;
            return false;
        }
        public void ConsumeIngredients(params int[] ingredientTypes)
        {
            // todo: make this work with recipecontainer
            for (int i = 0; i <  ingredientTypes.Length; i++)
            {
                if (Main.rand.NextFloat() > ChanceNotToConsumeIngredients)
                    Player.inventory.First(x => x.type == ingredientTypes[i]).stack--;
            }
        }
    }
}
