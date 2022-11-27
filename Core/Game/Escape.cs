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
    public Level _level;
    private Player _player;

    private int _difficuty;
    private float _score;

    private Camera _camera;
    
    private bool _debugOn;
    private Rectangle? debugRect;

    private EnemyManager _enemyManager;
    
    public Escape(Player player, int difficulty, bool debugOn)
    {
        _player = player;
        _level = LevelGenerator.GenerateLevel((int) (difficulty * 2), (int) (difficulty * 4));
        _player.setLevel(_level);
        player.Position = _level.StartRoom.GetRandomSpawnPos(player);
        _enemyManager = new(this);
        _enemyManager.SpawnEnemies();
        _debugOn = debugOn;
    }
    public void AttachCamera(Camera camera)
    {
        _camera = camera;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _level.Draw(spriteBatch);
        _player.Draw(spriteBatch);

        foreach (var enemy in _enemyManager.Enemies)
        {
            enemy.Draw(spriteBatch);
        }
        
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
        _enemyManager.Update(gameTime);
        if (_debugOn)
        {
            foreach (var obj in _level.GroundLayer)
            {
                if (obj.Intersects(_player.Rectangle))
                {
                    debugRect = obj;
                }
            }
            
        }
    }
}