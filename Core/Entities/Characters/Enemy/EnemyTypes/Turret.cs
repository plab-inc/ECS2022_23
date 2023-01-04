using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Turret : Enemy
{
    private int attackDelay;
    
    public Turret(Level level, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(), new StationaryShooter(target), level)
    {
        Velocity = 0f;
        HP = 20;
        XpReward = 1;
        MoneyReward = 1;
        Behavior.SetEnemy(this);
        ActivationHeight = 175;
        ActivationWith = 175;
        Strength = 1;
        DeathSound = SoundLoader.BlobDeathSound;
    }
    
    public override void Attack()
    {
        if (++attackDelay >= 75)
        {
            attackDelay = 0;
            CombatManager.Shoot(this);
        }
    }
    
}