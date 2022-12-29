﻿using System;
using System.Diagnostics;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public class AnimationManager
{
    private float _timer;
    private Animation CurrentAnimation { get; set; }
    private int _currentFrame;
    private Vector2 _scale = new Vector2(1,1);
    public bool AnimationFinished { get; private set; } = true;

    public void Play(Animation animation)
    {
        if (AnimationFinished == false)
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
        if (!CurrentAnimation.IsLooped) AnimationFinished = false;
    }

    public void Stop()
    {
        AnimationFinished = true;
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

    public void SetScale(Vector2 scale)
    {
        _scale = scale;
    }
    
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        if (CurrentAnimation == null) return;
        var sourceRec = new Rectangle((int)(_currentFrame + CurrentAnimation.StartFrame.X) * CurrentAnimation.Width, (int)CurrentAnimation.StartFrame.Y * CurrentAnimation.Height, CurrentAnimation.Width, CurrentAnimation.Height);

        try
        {
            if (CurrentAnimation.FlipX)
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, 0, Vector2.Zero, _scale,
                    SpriteEffects.FlipHorizontally, 0f);
            }
            else if (CurrentAnimation.FlipY)
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, 0, Vector2.Zero, _scale,
                    SpriteEffects.FlipVertically, 0f);
            }
            else
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, Color.White, 0, Vector2.Zero, _scale,
                    SpriteEffects.None, 0f);
            }
        }
        catch (ArgumentNullException e)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine("Texture not found.");
        }
    }
    
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        if (CurrentAnimation == null) return;
        var sourceRec = new Rectangle((int)(_currentFrame + CurrentAnimation.StartFrame.X) * CurrentAnimation.Width, (int)CurrentAnimation.StartFrame.Y * CurrentAnimation.Height, CurrentAnimation.Width, CurrentAnimation.Height);
        var scale = new Vector2(1, 1);

        try
        {
            if (CurrentAnimation.FlipX)
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            } else if (CurrentAnimation.FlipY)
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale, SpriteEffects.FlipVertically, 0f);
            }
            else
            {
                spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
        catch (ArgumentNullException e)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine("Texture not found.");
        }
    }
}