using Microsoft.Xna.Framework.Graphics;
using OlympusMod.Core.Loaders;
using System;
using System.Collections.Generic;
using OlympusMod.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using OlympusMod.Core.ChefClass;
using Terraria.GameContent.Bestiary;
using Terraria.UI.Chat;
using Terraria.GameContent;
using OlympusMod.Utilities;

namespace OlympusMod.Content.UI.Chef
{
    public class RecipeBookGui : OlympusUIState
    {
        private RecipeBookButton recipeBookButton;
        public UIRecipeContainerGroup recipeContainers;
        public bool recipeContainersOpen = false;
        public override bool Visible => Main.playerInventory;
        public override int InsertionIndex(List<GameInterfaceLayer> layers)
        {
            return layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        }
        public override void OnInitialize()
        {
            recipeBookButton = new RecipeBookButton();
            recipeContainers = new UIRecipeContainerGroup();
            
            Append(recipeBookButton);
            Append(recipeContainers);
            base.OnInitialize();
        }
        public override void Update(GameTime gameTime)
        {
            recipeContainers.SetProperties(300, 600, 32f * ChefPlayer.AMOUNT_OF_RECIPE_SLOTS, 32f * 3);
            recipeBookButton.UpdateProperties(32f, 570, 275);
            Recalculate();
            base.Update(gameTime);
        }
    }
    public class RecipeBookButton : UIElement
    {
        public const string buttonTex = "OlympusMod/Content/UI/Chef/RecipeBook";
        public const string highlight = buttonTex + "_Highlight";
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D baseTexture = ModContent.Request<Texture2D>(buttonTex).Value;
            Texture2D highlightTexture = ModContent.Request<Texture2D>(highlight).Value;
            Texture2D drawTex = IsMouseHovering ? highlightTexture : baseTexture;
            Rectangle bounds = GetDimensions().ToRectangle();
            spriteBatch.Draw(drawTex, bounds, Color.White * (UILoader.GetUIState<RecipeBookGui>().recipeContainersOpen ? 0.5f : 1f));
            var font = FontAssets.MouseText.Value;
            Vector2 drawCurrentSelectedRecipePosition = new(bounds.X + 0, bounds.Y - 0);
            int drawRecipeIndex = Main.LocalPlayer.GetChefPlayer().selectedRecipeSlot + 1;
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, drawRecipeIndex.ToString(), drawCurrentSelectedRecipePosition, Color.Yellow, 0f, Vector2.Zero, Vector2.One);
        }
        public void UpdateProperties(float dimension, float left, float top)
        {
            Width.Set(dimension, 0f);
            Height.Set(dimension, 0f);
            Left.Set(left, 0f);
            Top.Set(top, 0f);
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                string lookFor = UILoader.GetUIState<RecipeBookGui>().recipeContainersOpen ? "Close" : "Open";
                UICommon.TooltipMouseText(Language.GetOrRegister(OlympusMod.Instance.GetLocalizationKey($"UI.{nameof(RecipeBookButton)}.MouseHoverName.{lookFor}")).Value);
            }
            base.Update(gameTime);
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            RecipeBookGui gui = UILoader.GetUIState<RecipeBookGui>();
            gui.recipeContainers.Visible = gui.recipeContainersOpen = !gui.recipeContainersOpen;
            base.LeftClick(evt);
        }
    }
    public class UIRecipeContainerGroup : OlympusUIElement
    {
        private UIRecipeContainer[] recipeContainers;
        public bool Visible { get; set; } = false;
        public override void OnInitialize()
        {
            recipeContainers = new UIRecipeContainer[ChefPlayer.AMOUNT_OF_RECIPE_SLOTS];
            for (int i = 0; i < recipeContainers.Length; i++)
            {
                recipeContainers[i] = new UIRecipeContainer(i);
                Append(recipeContainers[i]);
            }
            base.OnInitialize();
        }
        public override void Update(GameTime gameTime)
        {
            if (!Visible)
                return;
            Rectangle bounds = GetDimensions().ToRectangle();
            int widthElem = bounds.Width / recipeContainers.Length;
            for (int i = 0; i < recipeContainers.Length; i++)
            {
                recipeContainers[i].SetProperties(0f, widthElem * i, 32f, 32f * 3f);
            }
            if (IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;
            Recalculate();
            base.Update(gameTime);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            base.DrawSelf(spriteBatch);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            base.Draw(spriteBatch);
        }
    }
}
