using ECS2022_23.Core.Screens;
using ECS2022_23.Core.Sound;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace ECS2022_23;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private ScreenManager _screenManager;
    
    private static readonly string[] PreloadAssets = System.Array.Empty<string>();
    
    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _screenManager = new ScreenManager(this);
        Components.Add(_screenManager);
        _screenManager.AddScreen(new BackgroundScreen(), null);
        _screenManager.AddScreen(new MainMenuScreen(), null);
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();
        
        SoundManager.Initialize();
        
        base.Initialize();
    }
    protected override void LoadContent()
    {
        foreach (string asset in PreloadAssets)
        {
            Content.Load<object>(asset);
        }
        
    }
    protected override void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.Black);
        
        base.Draw(gameTime);
    }
}