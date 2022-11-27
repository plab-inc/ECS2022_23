﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Animations;

public class AnimationManager
{
    private float _timer;
    private Animation CurrentAnimation { get; set; }
    private int _currentFrame;
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

        _timer = 0;
        CurrentAnimation = animation;
        _currentFrame = 0;
        if (!CurrentAnimation.IsLooped) _animationFinished = false;
    }

    private void Stop()
    {
        _animationFinished = true;
        CurrentAnimation = null;
        _currentFrame = 0;
        _timer = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (CurrentAnimation == null) return;
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if(_timer > CurrentAnimation.FrameSpeed)
        {
            _timer = 0f;

            _currentFrame++;

            if (_currentFrame >= CurrentAnimation.FrameCount)
                if (CurrentAnimation.IsLooped)
                {   
                    _currentFrame = 0;
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
        var sourceRec = new Rectangle((int)(_currentFrame + CurrentAnimation.StartFrame.X) * CurrentAnimation.Width, (int)CurrentAnimation.StartFrame.Y * CurrentAnimation.Height, CurrentAnimation.Width, CurrentAnimation.Height);
        var scale = new Vector2(1, 1);
        //var rotation = CurrentAnimation.Rotation;
        var rotation = 0f;
        var center = Vector2.Zero;
        if (CurrentAnimation.Rotation != 0)
        {
            //center = new Vector2(position.X + sourceRec.Width / 2f, position.Y + sourceRec.Height / 2f);
        }
       
        if (CurrentAnimation.FlipX == true)
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, rotation, center, scale, SpriteEffects.FlipHorizontally, 0f);
        } else if (CurrentAnimation.FlipY == true)
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, rotation, center, scale, SpriteEffects.FlipVertically, 0f);
        }
        else
        {
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, rotation, center, scale, SpriteEffects.None, 0f);
        }
    }
}