using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public abstract class UiComponent
{
    public Rectangle SourceRec;
    public Rectangle DestinationRec;
    public UiLabel UiLabel;
    public Vector2 Scale { get; set; } = Vector2.One;
    
    protected int PixelSize = 16;
    public abstract void Draw(SpriteBatch spriteBatch);
}
