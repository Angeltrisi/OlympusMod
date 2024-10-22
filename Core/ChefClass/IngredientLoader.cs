using OlympusMod.Content.Items.ChefClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Renderers;

namespace OlympusMod.Core.ChefClass
{
    public class IngredientLoader : HookGroup
    {
        public static readonly List<Ingredient> ingredients = [];
        public static readonly List<IngredientRecipe> ingredientRecipes = [];
        private static readonly Dictionary<Type, Ingredient> ingredientsByType = [];
        private static readonly Dictionary<int, Ingredient> ingredientsByID = [];
        public static int IngredientType<T>() where T : Ingredient
        {
            var type = typeof(T);
            if (ingredientsByType.TryGetValue(type, out Ingredient value))
            {
                return value.IngredientType;
            }
            return 0;
        }
        public static Ingredient GetInstance(int id)
        {
            if (ingredientsByID.TryGetValue(id, out Ingredient value))
            {
                return value;
            }
            return null;
        }
        public static T GetInstance<T>() where T : Ingredient
        {
            return ingredients.FirstOrDefault(n => n is T) as T;
        }
        public static bool ContainsRecipe(out int result, RecipeContainer r) => ContainsRecipe(out result, r[0].type, r[1].type, r[2].type);
        public static bool ContainsRecipe(out int result, params int[] ingredients)
        {
            IngredientRecipe attemptResult = ingredientRecipes.FirstOrDefault(i => i.Ingredients.All(ingredients.Contains), null);
            result = attemptResult is null ? -1 : attemptResult.Result;
            return attemptResult is not null;
        }
        public override void Load()
        {
            if (Main.dedServ) return;
            foreach (Type t in OlympusMod.Instance.Code.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Ingredient))))
            {
                Ingredient instance = (Ingredient)Activator.CreateInstance(t);
                instance.IngredientType = ingredients.Count;
                ingredientsByType[t] = instance;
                ingredientsByID[instance.IngredientType] = instance;
                ingredients.Add(instance);
            }
        }
        public override void Unload()
        {
            if (Main.dedServ) return;
            ingredients?.Clear();
            ingredientRecipes?.Clear();
            ingredientsByType?.Clear();
            ingredientsByID?.Clear();
        }
    }
}
