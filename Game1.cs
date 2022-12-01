using Comora;
using ECS2022_23.Core;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Game;
using ECS2022_23.Core.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ECS2022_23;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Camera _camera;

    private Escape _escape;
    private Player _player;
    private Enemy _enemy;
    private Enemy _enemy2;
    private Enemy _enemy3;
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
        _camera = new Camera(_graphics.GraphicsDevice)
        {
            Zoom = 3f
        };

        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ContentLoader.Load(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        AnimationLoader.Load(Content);
        _player = new Player(Content.Load<Texture2D>("sprites/astro"), AnimationLoader.CreatePlayerAnimations());
        _player.Weapon = AnimationLoader.CreatePhaserWeapon(Vector2.Zero);
        
        _escape = new Escape(_player, 3, false);
        _escape.AttachCamera(_camera);
        _enemy = new Enemy(_player.Position, Content.Load<Texture2D>("sprites/spritesheet"), AnimationLoader.CreatePlayerAnimations(), new ChaseMotor(_player));
        _enemy2 = new Enemy(_player.Position, Content.Load<Texture2D>("sprites/spritesheet"), AnimationLoader.CreatePlayerAnimations(), new ChaseMotor(_player));
        _enemy3 = new Enemy(_player.Position, Content.Load<Texture2D>("sprites/spritesheet"), AnimationLoader.CreatePlayerAnimations(), new ChaseMotor(_player));
        CombatManager.AddEnemy(_enemy);
        CombatManager.AddEnemy(_enemy2);
        CombatManager.AddEnemy(_enemy3);
       
        UiLoader.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _escape.Update(gameTime);
        UiManager.Update(_player);
        _enemy.Update(gameTime);
        CombatManager.Update(gameTime, _player);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin(_camera, samplerState: SamplerState.PointClamp);
        
            _escape.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _enemy2.Draw(_spriteBatch);
            _enemy3.Draw(_spriteBatch);
            CombatManager.Draw(_spriteBatch);
            ItemManager.Draw(_spriteBatch);
            
        _spriteBatch.End();
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            UiManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}