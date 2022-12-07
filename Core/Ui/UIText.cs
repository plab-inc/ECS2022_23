using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public class UiText : Component
{
    public SpriteFont Font { get; set; }
    public string Text { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public UiText(Rectangle sourceRec, SpriteFont font, string text, UiLabels uiLabel) : base(sourceRec)
    {
        Font = font;
        Text = text;
        UiLabel = uiLabel;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Font, Text, new Vector2(DestinationRec.X, DestinationRec.Y), Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}