using ECS2022_23.Core.animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.entities.items;

public class Weapon : Item
{
    public Animation Animation { get; }

    public Weapon(Texture2D texture, Vector2 startPos, Animation animation) : base(texture)
    {
        Animation = animation;
        AddAnimation("Attack", animation);
    }
    
    public override void Update(GameTime gameTime)
    {
        AnimationManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position);
    }
    
}