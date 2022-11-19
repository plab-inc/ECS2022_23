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
    public static void Load(UiManager uiManager, ContentManager content)
    
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();

        _content = content;
        _texture2D = _content.Load<Texture2D>("sprites/spritesheet");

        try
        {
            UiPanel topContainer = CreateTopContainer();
            topContainer.Add(CreateHpIcon());
            topContainer.Add(CreateCoinIcon());
            topContainer.Add(CreateXpIcon());
            uiManager.AddPanel(topContainer);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }

    private static UiElement CreateHpIcon()
    {
        return new UiElement(new Rectangle(11*16, 4*16, PixelSize, PixelSize), _texture2D, Labels.HpIcon);
    }

    private static UiElement CreateCoinIcon()
    {
        return new UiElement(new Rectangle(13*16, 4*16, PixelSize, PixelSize), _texture2D, Labels.CoinIcon);
    }

    private static UiElement CreateXpIcon()
    {
        return new UiElement(new Rectangle(15*16, 4*16, PixelSize, PixelSize), _texture2D, Labels.XpIcon);
    }
    
    public static UiElement CreateHeart()
    {
        return new UiElement(new Rectangle(16*16, 4*16, PixelSize, PixelSize), _texture2D, Labels.Heart);
    }

    private static UiPanel CreateTopContainer()
    {
        return new UiPanel(new Rectangle(0,0, PixelSize, PixelSize), new Rectangle(0,0, Game1.ScreenWidth, 100), Labels.TopContainer);
    }
}