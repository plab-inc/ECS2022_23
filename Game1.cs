using System.Collections.Generic;
using ECS2022_23.Core.animations;
using ECS2022_23.Core.entities.characters;
using ECS2022_23.Core.entities.characters.enemy;
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
    private Enemy _enemy;

    private LevelGenerator generator;
    
    private Rectangle? debugRect;
    const int scaleFactor = 1;
    private Matrix transformMatrix;
    
    public static int ScreenHeight;
    public static int ScreenWidth;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ScreenHeight = _graphics.PreferredBackBufferHeight;
        ScreenWidth = _graphics.PreferredBackBufferWidth;
        
        transformMatrix = Matrix.CreateScale(scaleFactor, scaleFactor, 1f);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _camera = new Camera();
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

        _enemy = new Enemy(Content.Load<Texture2D>("sprites/astro"), new ChaseMotor(_player));
        _enemy.Position = (new Vector2(_player.Position.X + 20, _player.Position.Y + 20));
        
        ContentLoader.Load(Content);
        generator = new LevelGenerator(50, 3);
        generator.generateLevel();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        var mousePos = Mouse.GetState().Position.ToVector2();

        // Check if mouse is in the bounds of a Tiled object
        debugRect = null;
        
        foreach (var obj in generator.CollisionLayer)
        {
            if (obj.Contains(mousePos))
            {
                debugRect = obj;
            }
        }
        
        _player.Update(gameTime);
        _camera.Follow(_player);
        _enemy.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin(transformMatrix: _camera.Transform);

        foreach (var room in generator.Rooms)
        {
            room.Draw(_spriteBatch);
        }
        _player.Draw(_spriteBatch);
        _enemy.Draw(_spriteBatch);
        
        
        if (debugRect != null)
        {
            Texture2D _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Green });

            _spriteBatch.Draw(_texture, (Rectangle)debugRect, Color.White);
        }
        
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}