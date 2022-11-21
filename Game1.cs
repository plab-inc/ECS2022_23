using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Comora;
using ECS2022_23.Core;
using ECS2022_23.Core.animations;
using ECS2022_23.Core.entities.characters;
using ECS2022_23.Core.entities.items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Player _player;
    private Camera _camera;
    
    private Rectangle? debugRect;
    private List<Level> levels = new();
    
    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;
    
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();
        
        _camera = new Camera(_graphics.GraphicsDevice);
        _camera.LoadContent();
        _camera.Zoom = 2f;
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _player = new Player(Content.Load<Texture2D>("sprites/astro"), AnimationLoader.LoadPlayerAnimations(Content));
        
        _player.SetWeapon(new Weapon(Content.Load<Texture2D>("sprites/spritesheet"),Vector2.Zero, AnimationLoader.LoadBasicWeaponAnimation(Content)));

        ContentLoader.Load(Content);
        var level = LevelGenerator.GenerateLevel(5, 50);
        _player.setLevel(level);
        levels.Add(level);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        debugRect = null;

        _player.Update(gameTime);
        
        _camera.Position = _player.Position;
        _camera.Update(gameTime);
        
        foreach (var obj in levels.First().CollisionLayer)
        {
            if (obj.Intersects(_player.Rectangle))
            {
                debugRect = obj;
            }
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin(_camera, samplerState: SamplerState.PointClamp);
        
        levels.First().Draw(_spriteBatch);
        
        if (debugRect != null)
        {
            Texture2D _textureGreen = new Texture2D(GraphicsDevice, 1, 1);
            
            _textureGreen.SetData(new Color[] { Color.Green * 0.5f });
            
            _spriteBatch.Draw(_textureGreen, (Rectangle)debugRect, Color.White);
            
            Texture2D _textureRed = new Texture2D(GraphicsDevice, 1, 1);
            _textureRed.SetData(new Color[] { Color.Red * 0.5f });
            
            _spriteBatch.Draw(_textureRed,_player.Rectangle, Color.White);
            
        }
        
        _player.Draw(_spriteBatch);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}