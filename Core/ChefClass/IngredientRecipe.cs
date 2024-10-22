using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OlympusMod.Content.Items.ChefClass;
using OlympusMod.Utilities;
using Terraria;

namespace OlympusMod.Core.ChefClass
{
    // class for creating and registering specific recipes
    public class IngredientRecipe(int[] ingredients, Condition[] conditions, int result)
    {
        public int[] Ingredients { get; set; } = ingredients;
        public Condition[] Conditions { get; set; } = conditions;
        public int Result { get; } = result;
        public IngredientRecipe AddIngredient(int type)
        {
            return new IngredientRecipe([.. Ingredients, type], Conditions, Result);
        }
        public IngredientRecipe AddCondition(Condition condition)
        {
            return new IngredientRecipe(Ingredients, [.. Conditions, condition], Result);
        }
        public void Register()
        {
            IngredientLoader.ingredientRecipes.Add(this);
        }
    }
}
