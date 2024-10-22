using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace OlympusMod.Content.UI.UIElements
{
    public abstract class OlympusItemSlot : OlympusUIElement
    {
        public abstract ref Item item { get; }
        public abstract Func<Item, bool> isValid { get; }
        public abstract string Texture { get; }
        public abstract float ItemDrawSizeLimit {  get; }
        public bool NoItem => item is null || item.IsAir;
        public bool IsMouseOver => ContainsPoint(Main.MouseScreen);
        public void UpdateProperties(float dimension, float left, float top)
        {
            Width.Set(dimension, 0f);
            Height.Set(dimension, 0f);
            Left.Set(left, 0f);
            Top.Set(top, 0f);
        }
        public virtual void PostClickItemIn(ref Item slotItem)
        {

        }
        public virtual void PostClickItemOut(ref Item mouseItem)
        {

        }
        public virtual void PostDraw(SpriteBatch spriteBatch)
        {

        }
        public virtual bool CanBeInteractedWith()
        {
            return true;
        }
        public sealed override void LeftClick(UIMouseEvent evt)
        {
            if (!CanBeInteractedWith())
                return;
            if (!Main.mouseItem.IsAir && isValid(Main.mouseItem) && NoItem)
            {
                item = Main.mouseItem.Clone();
                Main.mouseItem.TurnToAir();
                SoundEngine.PlaySound(SoundID.Grab);
            }
            else if (Main.mouseItem.IsAir && !NoItem)
            {
                Main.mouseItem = item.Clone();
                item.TurnToAir();
                SoundEngine.PlaySound(SoundID.Grab);
            }
            else if (!Main.mouseItem.IsAir && isValid(Main.mouseItem) && !NoItem)
            {
                if (Main.mouseItem.type != item.type)
                {
                    var temp = item.Clone();
                    item = Main.mouseItem.Clone();
                    Main.mouseItem = temp;
                }
                else
                {
                    var joined = item.stack + Main.mouseItem.stack;
                    if (joined > item.maxStack)
                    {
                        item.stack = item.maxStack;
                        Main.mouseItem.stack = joined - item.maxStack;
                    }
                    else
                    {
                        item.stack = joined;
                        Main.mouseItem.TurnToAir();
                    }
                }
                SoundEngine.PlaySound(SoundID.Grab);
            }
            base.LeftClick(evt);
        }
        public sealed override void RightClick(UIMouseEvent evt)
        {
            if (!CanBeInteractedWith())
                return;
            if (Main.mouseItem.IsAir && !NoItem)
            {
                var temp = item.Clone();
                temp.stack = 1;
                Main.mouseItem = temp;

                item.stack--;

                if (item.stack <= 0)
                    item.TurnToAir();

                SoundEngine.PlaySound(SoundID.MenuTick);
            }
            else if (!Main.mouseItem.IsAir && !NoItem && Main.mouseItem.type == item.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
            {
                Main.mouseItem.stack++;
                item.stack--;

                if (item.stack <= 0)
                    item.TurnToAir();

                SoundEngine.PlaySound(SoundID.MenuTick);
            }
            base.RightClick(evt);
        }
        public override void Update(GameTime gameTime)
        {
            if (!CanBeInteractedWith())
                return;
            if (IsMouseOver)
                Main.LocalPlayer.mouseInterface = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var tex = ModContent.Request<Texture2D>(Texture).Value;
            spriteBatch.Draw(tex, GetDimensions().ToRectangle(), Color.White);
            if (!NoItem)
            {
                ItemSlot.DrawItemIcon(item, 21, spriteBatch, GetDimensions().Position() + new Vector2(GetDimensions().Width / 2, GetDimensions().Height / 2), 1f, ItemDrawSizeLimit, Color.White);
                if (item.stack > 1)
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, item.stack.ToString(), GetDimensions().Position() + new Vector2(4f, 26f) * Main.inventoryScale, Color.White, 0f, Vector2.Zero, new Vector2(Main.inventoryScale) * 1.5f, -1f, Main.inventoryScale);
                //ItemSlot.Draw(spriteBatch, ref item, 21, GetDimensions().Position());
                if (IsMouseOver)
                {
                    Main.HoverItem = item.Clone();
                    Main.hoverItemName = "a";
                }
            }
            if (IsMouseOver)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            PostDraw(spriteBatch);
        }
    }
}
