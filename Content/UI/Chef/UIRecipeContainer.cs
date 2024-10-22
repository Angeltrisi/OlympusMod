using Microsoft.Xna.Framework.Graphics;
using OlympusMod.Content.Items.ChefClass;
using OlympusMod.Content.UI.UIElements;
using OlympusMod.Core.ChefClass;
using OlympusMod.Core.Loaders;
using OlympusMod.Utilities;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace OlympusMod.Content.UI.Chef
{
    public class UIRecipeContainer(int index) : OlympusUIElement
    {
        public IngredientSlot first;
        public IngredientSlot second;
        public IngredientSlot third;
        public override void OnInitialize()
        {
            first = new(index, 0);
            second = new(index, 1);
            third = new(index, 2);
            Append(first);
            Append(second);
            Append(third);
            base.OnInitialize();
        }
        public override void Update(GameTime gameTime)
        {
            Rectangle bounds = GetDimensions().ToRectangle();
            int yOff = bounds.Height / 3;
            first.UpdateProperties(32f, 0f, 0f);
            second.UpdateProperties(32f, 0f, yOff);
            third.UpdateProperties(32f, 0f, yOff * 2);
            base.Update(gameTime);
        }
    }
    public class IngredientSlot(int recipeContainer, int itemIndexInRecipeContainer) : OlympusItemSlot
    {
        private const string ToUIPathSlot = "OlympusMod/Content/UI/UIElements/BlankSlot";
        private const string ToBlueSlot = ToUIPathSlot + "Blue";
        private const string ToRedSlot = ToUIPathSlot + "Red";
        private const string ToCyanSlot = ToUIPathSlot + "Cyan";
        public override ref Item item => ref Main.LocalPlayer.GetChefPlayer().recipeSlots[recipeContainer].items[itemIndexInRecipeContainer];
        public override float ItemDrawSizeLimit => 32f;
        public override Func<Item, bool> isValid => (item) => item.ModItem is Ingredient;
        public override string Texture => recipeContainer % 3 == 0 ? ToCyanSlot : recipeContainer % 2 == 0 ? ToRedSlot : ToBlueSlot;
        public override bool CanBeInteractedWith() => UILoader.GetUIState<RecipeBookGui>().recipeContainersOpen;
        public override void PostDraw(SpriteBatch spriteBatch)
        {
            Texture2D highlightTex = ModContent.Request<Texture2D>("OlympusMod/Content/UI/UIElements/BasicSlotHighlight").Value;
            ChefPlayer chef = Main.LocalPlayer.GetChefPlayer();
            if (chef.selectedRecipeSlot == recipeContainer)
                spriteBatch.Draw(highlightTex, GetDimensions().ToRectangle(), Color.White);
        }
    }
}
