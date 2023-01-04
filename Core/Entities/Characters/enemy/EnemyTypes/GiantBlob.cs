using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class GiantBlob : Enemy
{
    private int _bulletWaveDelay;
    private int _shotDelay;
    
    public GiantBlob(Level level, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet,
        AnimationLoader.CreateBlobEnemyAnimations(), new Boss(target), level)
    {
        IsBoss = true;
        Velocity = 0.5f;
        HP = 150;
        XpReward = 5;
        MoneyReward = 10;
        SpriteHeight = 32;
        SpriteWidth = 32;
        Behavior.SetEnemy(this);
        DeathSound = SoundLoader.BlobDeathSound;
        Strength = 2;
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
        CombatManager.Shoot(Position, new Vector2(0,-1), Level);
        CombatManager.Shoot(Position, new Vector2(1,-1), Level);
        CombatManager.Shoot(Position, new Vector2(1,0), Level);
        CombatManager.Shoot(Position, new Vector2(1,1), Level);
        CombatManager.Shoot(Position, new Vector2(0,1), Level);
        CombatManager.Shoot(Position, new Vector2(-1,1), Level);
        CombatManager.Shoot(Position, new Vector2(-1,0), Level);
        CombatManager.Shoot(Position, new Vector2(-1,-1), Level);
    }

}