using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class Chaser : Enemy
{
    public Chaser(Level level) : base(Vector2.Zero, ContentLoader.EnemyTexture, new ChaseMotor(level, EnemyManager.Player.Position), level)
    {
        Velocity = 1;
        HP = 10;
        XpReward = 1;
        MoneyReward = 1;
        ActivationRectangle.Inflate(35, 35);
        Motor.SetEnemy(this);
    }

    public Chaser(Entity target, Dictionary<string, Animation> animations, Motor motor, Level level) : base(Vector2.Zero, ContentLoader.EnemyTexture, animations, motor, level)
    {
        Velocity = 1;
        HP = 10;
        XpReward = 1;
        MoneyReward = 1;
        ActivationRectangle.Inflate(35, 35);
        Motor.SetEnemy(this);
    }
}