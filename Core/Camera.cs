using ECS2022_23;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLevelGenerator.Core;

public class Camera
{
    public Matrix Transform { get; private set; }

    
    public void Follow(Entity target)
    {
        var position = Matrix.CreateTranslation(
            -target.Position.X - (target.Rectangle.Width / 2f),
            -target.Position.Y - (target.Rectangle.Height / 2f),
            0);

        var offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2f,
            Game1.ScreenHeight / 2f,
            0);

        Transform = position * offset;
    }
    public void Follow(Vector2 target)
    {
        var position = Matrix.CreateTranslation(
            -target.X,
            -target.Y,
            0);

        var offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2f,
            Game1.ScreenHeight / 2f,
            0);

        Transform = position * offset;
    }
}