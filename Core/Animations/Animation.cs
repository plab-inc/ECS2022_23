using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Animations;

public class Animation
{
    public readonly bool IsLooped;

    public Animation(Texture2D texture, int width, int height, int frameCount, Point startFrame, bool isLooped)
    {
        Texture = texture;
        Width = width;
        Height = height;
        FrameCount = frameCount;
        StartFrame = startFrame;
        FrameSpeed = 0.2f;
        IsLooped = isLooped;
    }

    public Animation(Texture2D texture, int width, int height, int frameCount, Point startFrame, bool isLooped,
        bool flipX, bool flipY)
        : this(texture, width, height, frameCount, startFrame, isLooped)
    {
        FlipX = flipX;
        FlipY = flipY;
    }

    public bool FlipX { get; set; }
    public bool FlipY { get; set; }

    public Point StartFrame { get; }
    public float FrameSpeed { get; set; }

    public Texture2D Texture { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int FrameCount { get; set; }
}