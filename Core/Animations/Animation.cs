using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Animations;

public class Animation
{
    private Texture2D _texture;
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public Vector2 StartFrame { get; }
    public float FrameSpeed { get; set; }
    public readonly bool IsLooped;

    public Texture2D Texture
    {
        get => _texture;
        set => _texture = value;
    }

    public int Width { get; set; }

    public int Height { get; set; }

    public int FrameCount { get; set; }

    public Animation(Texture2D texture, int width, int height, int frameCount, Vector2 startFrame, bool isLooped)
    {
        _texture = texture;
        Width = width;
        Height = height;
        FrameCount = frameCount;
        StartFrame = startFrame;
        FrameSpeed = 0.2f;
        IsLooped = isLooped;
    }
    
    public Animation(Texture2D texture, int width, int height, int frameCount, Vector2 startFrame, bool isLooped, bool flipX, bool flipY) 
        : this(texture, width, height, frameCount, startFrame, isLooped)
    {
        FlipX = flipX;
        FlipY = flipY;
    }
}