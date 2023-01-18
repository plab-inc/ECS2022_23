using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Turret : Enemy
{
    private int _attackDelay;
    
    public Turret(Stage stage, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(), new StationaryShooter(target), stage)
    {
        Velocity = 0f;
        HP = 20;
        Strength = 1;
        EpReward = 1;
        
        DeathSound = SoundLoader.BlobDeathSound;
        ItemSpawnRate = 30f;
    }
    
    public override void Attack()
    {
        if(!IsActive)
            return;
        
        if (++_attackDelay >= 75)
        {
            _attackDelay = 0;
            CombatManager.Shoot(this);
        }
    }
    
}