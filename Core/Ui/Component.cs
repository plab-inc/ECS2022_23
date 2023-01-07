using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public abstract class Component
{
    public Rectangle SourceRec;
    public Rectangle DestinationRec;
    public UiLabel UiLabel;

    protected Component(Rectangle sourceRec)
    {
        SourceRec = sourceRec;
    }

    public abstract void Draw(SpriteBatch spriteBatch);
}
