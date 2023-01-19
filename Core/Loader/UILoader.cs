using System.IO;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Loader;

internal static class UiLoader
{
    private const int PixelSize = 16;

    private static ContentManager _content;
    public static Texture2D SpriteSheet;
    private static SpriteFont _font;
    private static GraphicsDevice _graphicsDevice;

    public static void Load(ContentManager content, GraphicsDevice graphicsDevice)

    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();

        _content = content;
        _graphicsDevice = graphicsDevice;

        SpriteSheet = _content.Load<Texture2D>("sprites/spritesheet");
        _font = _content.Load<SpriteFont>("fonts/rainyhearts");
    }

    public static void InitializeUi(float maxHearts)
    {
        UiManager.Init();
        var statsContainer = UiManager.StatsContainer;

        statsContainer.Add(CreateUiElementNew(UiLabel.HpIcon));
        for (var i = 0; i < maxHearts; i++) statsContainer.Add(CreateUiElementNew(UiLabel.HeartIcon));
        statsContainer.Add(CreateUiElementNew(UiLabel.EpIcon));
        statsContainer.Add(CreateTextElement(UiLabel.EpText));
        statsContainer.Add(CreateUiElementNew(UiLabel.LevelIcon));
        statsContainer.Add(CreateTextElement(UiLabel.LevelText));
        statsContainer.Add(CreateUiElementNew(UiLabel.ArmorIcon));
        statsContainer.Add(CreateTextElement(UiLabel.ArmorText));
    }

    private static UiElement CreateUiElementNew(UiLabel label)
    {
        Rectangle sourceRectangle = default;

        switch (label)
        {
            case UiLabel.ArmorIcon:
                sourceRectangle = new Rectangle(14 * PixelSize, 4 * PixelSize, PixelSize, PixelSize);
                break;
            case UiLabel.HpIcon:
                sourceRectangle = new Rectangle(11 * PixelSize, 4 * PixelSize, PixelSize, PixelSize);
                break;
            case UiLabel.LevelIcon:
                sourceRectangle = new Rectangle(15 * PixelSize, 4 * PixelSize, PixelSize, PixelSize);
                break;
            case UiLabel.EpIcon:
                sourceRectangle = new Rectangle(13 * PixelSize, 4 * PixelSize, PixelSize, PixelSize);
                break;
            case UiLabel.HeartIcon:
                sourceRectangle = new Rectangle(16 * PixelSize, 4 * PixelSize, PixelSize, PixelSize);
                break;
        }

        return new UiElement(sourceRectangle, SpriteSheet, label);
    }

    private static UiText CreateTextElement(UiLabel uiLabel)
    {
        return new UiText(new Rectangle(0, 0, PixelSize, PixelSize), _font, "0", uiLabel);
    }

    public static UiText CreateTextElement(string text)
    {
        return new UiText(new Rectangle(0, 0, PixelSize, PixelSize), _font, text);
    }

    public static Texture2D CreateColorTexture(Color color)
    {
        var recTexture = new Texture2D(_graphicsDevice, 1, 1);
        recTexture.SetData(new[] {color});
        return recTexture;
    }
}