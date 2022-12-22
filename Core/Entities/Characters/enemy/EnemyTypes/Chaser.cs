using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class Chaser : Enemy
{
    public Chaser(Level level, Entity target) : base(Vector2.Zero, ContentLoader.EnemyTexture, new ChaseMotor(level, target), level)
    {
        Velocity = 1f;
        HP = 10;
        XpReward = 1;
        MoneyReward = 1;
        ActivationRectangle.Inflate(35, 35);
        Motor.SetEnemy(this);
        Motor.SetTarget(target);
    }

    public Chaser(Entity target, Dictionary<string, Animation> animations, Motor motor, Level level) : base(Vector2.Zero, ContentLoader.EnemyTexture, animations, motor, level)
    {
        Velocity = 1f;
        HP = 10;
        XpReward = 1;
        MoneyReward = 1;
        ActivationRectangle.Inflate(35, 35);
        Motor.SetEnemy(this);
        Motor.SetTarget(target);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}