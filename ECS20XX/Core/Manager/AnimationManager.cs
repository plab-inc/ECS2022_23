using ECS2022_23.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public class AnimationManager
{
    private readonly Timer _colorTimer = new(0.3f);
    private Color _activeColor = Color.White;
    private int _currentFrame;
    private Color _prevColor = new(236, 86, 113, 255);
    private Vector2 _scale = new(1, 1);
    private bool _switchingColors;
    private float _timer;
    private Animation.Animation CurrentAnimation { get; set; }
    public bool AnimationFinished { get; private set; } = true;

    public void Play(Animation.Animation animation)
    {
        if (AnimationFinished == false) return;
        if (CurrentAnimation == animation) return;

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
        _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > CurrentAnimation.FrameSpeed)
        {
            _timer = 0f;

            _currentFrame++;

            if (_currentFrame >= CurrentAnimation.FrameCount)
                if (CurrentAnimation.IsLooped)
                    _currentFrame = 0;
                else
                    Stop();
        }

        if (_switchingColors)
        {
            _colorTimer.Update(gameTime);
            if (_colorTimer.LimitReached()) (_activeColor, _prevColor) = (_prevColor, _activeColor);
        }
    }

    public void SetScale(Vector2 scale)
    {
        _scale = scale;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale = default)
    {
        if (scale == Vector2.Zero)
            scale = _scale;

        if (CurrentAnimation == null) return;
        var sourceRec = new Rectangle((_currentFrame + CurrentAnimation.StartFrame.X) * CurrentAnimation.Width,
            CurrentAnimation.StartFrame.Y * CurrentAnimation.Height, CurrentAnimation.Width, CurrentAnimation.Height);
        var color = Color.White;

        if (_switchingColors) color = _activeColor;

        if (CurrentAnimation.FlipX)
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.FlipHorizontally, 0f);
        else if (CurrentAnimation.FlipY)
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.FlipVertically, 0f);
        else
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.None, 0f);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float sourceHeightModifier = 1.0f,
        Vector2 scale = default)
    {
        if (CurrentAnimation == null) return;
        var sourceRec = new Rectangle((_currentFrame + CurrentAnimation.StartFrame.X) * CurrentAnimation.Width,
            CurrentAnimation.StartFrame.Y * CurrentAnimation.Height,
            CurrentAnimation.Width, (int) (CurrentAnimation.Height * sourceHeightModifier));

        if (scale == Vector2.Zero)
            scale = _scale;

        if (_switchingColors) color = _activeColor;

        if (CurrentAnimation.FlipX)
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.FlipHorizontally, 0f);
        else if (CurrentAnimation.FlipY)
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.FlipVertically, 0f);
        else
            spriteBatch.Draw(CurrentAnimation.Texture, position, sourceRec, color, 0, Vector2.Zero, scale,
                SpriteEffects.None, 0f);
    }

    public void StartColorChange(Color color1, Color color2)
    {
        _activeColor = color1;
        _prevColor = color2;
        _switchingColors = true;
    }

    public void StopColorChange()
    {
        _switchingColors = false;
        _colorTimer.Reset();
    }
}