using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public class UiElement : UiComponent
{
    private readonly Texture2D _texture;

    public UiElement(Rectangle sourceRec, Texture2D texture, UiLabel uiLabel)
    {
        SourceRec = sourceRec;
        _texture = texture;
        UiLabel = uiLabel;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, DestinationRec, SourceRec, Color.White);
    }
}