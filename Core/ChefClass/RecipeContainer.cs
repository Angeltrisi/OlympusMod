using OlympusMod.Content.Items.ChefClass;
using OlympusMod.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace OlympusMod.Core.ChefClass
{
    public class RecipeContainer : IEnumerable<Item>
    {
        public Item[] items = new Item[3];
        public bool Active;
        public bool HasFullIngredients { get { return items.All(i => i.Exists()); } }
        public Item this[int i]
        {
            get => items[i];
            set => items[i] = value;
        }
        public IEnumerator<Item> GetEnumerator()
        {
            for (int i = 0; i < items.Length; i++)
                yield return items[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
