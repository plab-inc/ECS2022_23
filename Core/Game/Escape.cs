using System;
using System.Linq;
using Comora;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Game;

public class Escape
{
    private Level _currentLevel;
    private Player _player;
    private Camera _camera;
    
    private bool _debugOn;
    private Rectangle? debugRect;
    
    public Escape(Player player, int difficulty, bool debugOn)
    {
        
        _player = player;
        _currentLevel = LevelGenerator.GenerateLevel(difficulty * 2, difficulty * 4);
        _currentLevel.Player = player;
        player.Level = _currentLevel;
        player.Room = _currentLevel.StartRoom;
        player.Position =  _currentLevel.StartRoom.GetRandomSpawnPos(player);
        EnemyManager.Level = _currentLevel;
        EnemyManager.Player = _player;
        EnemyManager.SpawnEnemies();
        _debugOn = debugOn;

    }
    public void AttachCamera(Camera camera)
    {
        _camera = camera;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _currentLevel.Draw(spriteBatch);
        _player.Draw(spriteBatch);
        EnemyManager.Draw(spriteBatch);
        
        
        if (debugRect != null)
        {
            //TODO Draw debug rect
        }
    }

    public void Update(GameTime gameTime)
    { 
        _camera.Position = _player.Position;
        _camera.Update(gameTime);
        _player.Update(gameTime);
        _currentLevel.Update(gameTime);
        EnemyManager.Update(gameTime);
        
        if (_debugOn)
        {
            foreach (var obj in _currentLevel.GroundLayer)
            {
                if (obj.Intersects(_player.Rectangle))
                {
                    debugRect = obj;
                }
            }
            
        }
        
    }
    
}