﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Comora;
using ECS2022_23.Core.animations;
using ECS2022_23.Core.entities.characters;
using ECS2022_23.Core.entities.items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLevelGenerator.Core;
using Transform;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ECS2022_23;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Player _player;
    private Camera _camera;
    
    private LevelGenerator generator;
    
    private Rectangle? debugRect;

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
        _camera.Debug.IsVisible = true;
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _player = new Player(Content.Load<Texture2D>("sprites/astro"), new Dictionary<string, Animation>()
        {
            { "WalkUp", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1,5), true) },
            { "WalkRight", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1,4), true) },
            { "WalkLeft", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1,4),true, true, false) },
            { "WalkDown", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1,3), true) },
            { "AttackRight", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 2, new Vector2(7,6), false) },
            { "Default", new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 7, new Vector2(1,2), true) }
        });
        
        _player.SetWeapon(new Weapon(Content.Load<Texture2D>("sprites/spritesheet"), Vector2.Zero, 
            new Animation(Content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(13, 6), false)));
        
        ContentLoader.Load(Content);
        generator = new LevelGenerator(50, 3);
        generator.generateLevel();

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
        
        foreach (var obj in generator.CollisionLayer)
        {
            if (obj.Contains(_player.Position))
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
        
        foreach (var room in generator.Rooms)
        {
            room.Draw(_spriteBatch);
        }
        
        if (debugRect != null)
        {
            Texture2D _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Green });

            _spriteBatch.Draw(_texture, (Rectangle)debugRect, Color.White);
        }
        
        _player.Draw(_spriteBatch);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}