using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLevelGenerator.Core;

public abstract class Entity
{
    protected Texture2D _texture; 
    public Vector2 Position { get; set; }
    
    public Rectangle Rectangle
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
    }

    protected Entity(Texture2D texture)
    {
        _texture = texture;
        Position = new Vector2(60, 60);
    }
    
    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture,Position,Color.White);
    }
}