
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class GiantBlob : Enemy
{
    private int _bulletWaveDelay;
    private int _shotDelay;
    
    public GiantBlob(Stage stage, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet,
        AnimationLoader.CreateBlobEnemyAnimations(), new Boss(target), stage)
    {
        IsBoss = true;
        Behavior.SetEnemy(this);

        Velocity = 0.5f;
        HP = 150;
        Strength = 2;
        EpReward = 10;
        
        SpriteHeight = 32;
        SpriteWidth = 32;
        DeathSound = SoundLoader.BlobDeathSound;
        ItemSpawnRate = 1.0f;
    }

    public override void Attack()
    {
        if (++_bulletWaveDelay>280)
        {
            _bulletWaveDelay = 0;
            SpawnBulletWave();
        }
        _bulletWaveDelay++;

        if (++_shotDelay > 50)
        {
            _shotDelay = 0;
            CombatManager.Shoot(this);
        }
    }

    private void SpawnBulletWave()
    {
        CombatManager.Shoot(Position, new Vector2(0,-1), Stage);
        CombatManager.Shoot(Position, new Vector2(1,-1), Stage);
        CombatManager.Shoot(Position, new Vector2(1,0), Stage);
        CombatManager.Shoot(Position, new Vector2(1,1), Stage);
        CombatManager.Shoot(Position, new Vector2(0,1), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,1), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,0), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,-1), Stage);
    }

}