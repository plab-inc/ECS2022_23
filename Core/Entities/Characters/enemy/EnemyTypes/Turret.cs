using System.Diagnostics;
using System.Threading;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;


namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class Turret : Enemy
{
    private int attackDelay;
    
    public Turret(Level level, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateZombieEnemyAnimations(), new StationaryShooter(target), level)
    {
        Velocity = 0f;
        HP = 20;
        XpReward = 1;
        MoneyReward = 1;
        
        ActivationRectangle.Inflate(35, 35);
        Behavior.SetEnemy(this);
        DeathSound = SoundLoader.BlobDeathSound;
    }
    
    public override void Attack()
    {
        if (++attackDelay >= 50)
        {
            attackDelay = 0;
            CombatManager.Shoot(this);
        }
    }
    
}