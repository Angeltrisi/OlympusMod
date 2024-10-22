using Microsoft.Xna.Framework.Graphics;
using ReLogic;
using Terraria.GameContent;
using Terraria.UI;

namespace OlympusMod.Content.UI
{
    public abstract class OlympusUIElement : UIElement
    {
        public void SetProperties(float top, float left, float width, float height)
        {
            Top.Set(top, 0f);
            Left.Set(left, 0f);
            Width.Set(width, 0f);
            Height.Set(height, 0f);
        }
        public void DrawDebugRectangle(SpriteBatch sb, Color color)
        {
            sb.Draw(TextureAssets.MagicPixel.Value, GetDimensions().ToRectangle(), color);
        }
    }
}
