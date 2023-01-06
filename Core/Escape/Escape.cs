using Comora;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Screens;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Sound;
using ECS2022_23.Core.World;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Game;

public class Escape
{
    private Stage _currentStage;
    public Player _player;
    private Camera _camera;
    

    private int _difficulty;
    private int _levelsCompleted;
    private int _levelsToComplete;
    public bool WasSuccessful { get; private set; }
    public bool Failed { get; private set; }
    
    public Escape(Player player, int startDifficulty, int levelsToComplete)
    {
        
        _player = player;
        _difficulty = startDifficulty;
        _levelsToComplete = levelsToComplete;
        
        InitializeLevel();
        
    }
    private void InitializeLevel()
    { 
        CombatManager.Init();
        ItemManager.Init();
        _currentStage = LevelGenerator.GenerateLevel(_difficulty * 2, _difficulty * 4);
        _currentStage.Player = _player;
        _player.Stage = _currentStage;
        _player.Room = _currentStage.StartRoom;
        _player.Position = Vector2.Zero;
        _player.Position = _currentStage.StartRoom.GetRandomSpawnPos(_player);
        
        EnemyManager.Stage = _currentStage;
        EnemyManager.Player = _player;
        
        EnemyManager.KillEnemies();
        EnemyManager.SpawnMultipleEnemies(_difficulty);
    }
    public void AttachCamera(Camera camera)
    {
        _camera = camera;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _currentStage.Draw(spriteBatch);
        _player.Draw(spriteBatch);
        EnemyManager.Draw(spriteBatch);
    }
    public void Update(GameTime gameTime)
    {
        _camera.Position = _player.Position;
        _camera.Update(gameTime);
        _player.Update(gameTime);
        _currentStage.Update(gameTime);
        EnemyManager.Update(gameTime);

        if (!_player.IsAlive())
        {
            Failed = true;
        }

        if (!_currentStage.IsCompleted) return;
        
        _levelsCompleted++;
            
        if (_levelsCompleted < _levelsToComplete)
        {
            _difficulty++;
            InitializeLevel();
        }
        else
        {
            WasSuccessful = true;
        }
    }
    
}