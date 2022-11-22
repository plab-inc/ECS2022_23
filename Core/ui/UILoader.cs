using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.ui;

internal static class UiLoader
{
    private const int PixelSize = 16;

    private static ContentManager _content;
    private static Texture2D _texture2D;
    private static SpriteFont _font;
    public static void Load(UiManager uiManager, ContentManager content)
    
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();

        _content = content;
        try
        {
            _texture2D = _content.Load<Texture2D>("sprites/spritesheet");
            _font = _content.Load<SpriteFont>("fonts/rainyhearts");
        
            UiPanel topContainer = CreateTopContainer();
            topContainer.Add(CreateHpIcon());
            topContainer.Add(CreateCoinIcon());
            topContainer.Add(CreateTextElement(UiLabels.CoinText));
            topContainer.Add(CreateXpIcon());
            topContainer.Add(CreateTextElement(UiLabels.XpText));
            uiManager.AddPanel(topContainer);
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error while loading assets.\n" + e.Message);
        }
    }

    private static UiElement CreateHpIcon()
    {
        return new UiElement(new Rectangle(11*16, 4*16, PixelSize, PixelSize), _texture2D, UiLabels.HpIcon);
    }

    private static UiElement CreateCoinIcon()
    {
        return new UiElement(new Rectangle(13*16, 4*16, PixelSize, PixelSize), _texture2D, UiLabels.CoinIcon);
    }

    private static UiElement CreateXpIcon()
    {
        return new UiElement(new Rectangle(15*16, 4*16, PixelSize, PixelSize), _texture2D, UiLabels.XpIcon);
    }
    
    public static UiElement CreateHeart()
    {
        return new UiElement(new Rectangle(16*16, 4*16, PixelSize, PixelSize), _texture2D, UiLabels.Heart);
    }

    private static UiPanel CreateTopContainer()
    {
        return new UiPanel(new Rectangle(0,0, PixelSize, PixelSize), new Rectangle(0,0, Game1.ScreenWidth, 100), UiLabels.TopContainer);
    }
    
    private static UiText CreateTextElement(UiLabels uiLabel)
    {
        return new UiText(new Rectangle(0,0, PixelSize, PixelSize), _font, "0", uiLabel);
    }
}