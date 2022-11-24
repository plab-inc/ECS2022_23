using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.ui;

public class UiElement : Component
{
    private Texture2D _texture;
 
    public UiElement(Rectangle sourceRec, Texture2D texture, UiLabels uiLabel) : base(sourceRec)
    {
        _texture = texture;
        this.UiLabel = uiLabel;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, DestinationRec, SourceRec, Color.White);
    }
    
}

