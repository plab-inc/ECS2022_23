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
        _player = new Player(Content.Load<Texture2D>("sprites/astro"));
        
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