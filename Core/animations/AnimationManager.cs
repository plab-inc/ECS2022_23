using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.animations;

public class AnimationManager
{
    
    protected float Timer;
    public Animation CurrentAnimation { get; set; }
    protected int CurrentFrame;
    private bool _animationFinished = true;

    public void Play(Animation animation)
    {
        if (_animationFinished == false)
        {
            return;
        }
        if (CurrentAnimation == animation)
        {
            return;
        }

        Timer = 0;
        CurrentAnimation = animation;
        CurrentFrame = 0;
        if (!CurrentAnimation.IsLooped) _animationFinished = false;
    }

    public void Stop()
    {
        _animationFinished = true;
        CurrentAnimation = null;
        CurrentFrame = 0;
        Timer = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (CurrentAnimation == null) return;
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if(Timer > CurrentAnimation.FrameSpeed)
        {
            Timer = 0f;

            CurrentFrame++;

            if (CurrentFrame >= CurrentAnimation.FrameCount)
                if (CurrentAnimation.IsLooped)
                {   
                    CurrentFrame = 0;
                }
                else
                {
                    Stop();
                }
        }
    }
    
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        if (CurrentAnimation == null) return;
        var frame = CurrentFrame + CurrentAnimation.StartFrame.X;
        
        if (CurrentAnimation.FlipX == true)
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, new Rectangle( (int)frame * 16, (int)CurrentAnimation.StartFrame.Y*16, 16, 16),
                Color.White,
                0.0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0f);
        } else if (CurrentAnimation.FlipY == true)
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, new Rectangle((int)frame * 16, (int)CurrentAnimation.StartFrame.Y*16, 16, 16),
                Color.White,
                0.0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.FlipVertically, 0f);
        }
        else
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, new Rectangle((int)frame * 16, (int)CurrentAnimation.StartFrame.Y*16, 16, 16),
                Color.White,
                0.0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
        }
    }
}