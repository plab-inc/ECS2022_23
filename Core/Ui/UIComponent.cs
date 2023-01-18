using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public abstract class UiComponent
{
    public Rectangle DestinationRec;

    protected int PixelSize = 16;
    public Rectangle SourceRec;
    public UiLabel UiLabel;
    public Vector2 Scale { get; set; } = Vector2.One;
    public abstract void Draw(SpriteBatch spriteBatch);
}