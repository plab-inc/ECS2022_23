using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class Chaser : Enemy
{
    public Chaser(Level level, Entity target) : base(Vector2.Zero, ContentLoader.EnemyTexture, new ChaseMotor(level, target), level)
    {
        Velocity = 1f;
        HP = 10;
        ActivationRectangle.Inflate(35, 35);
        _motor.SetEnemy(this);
    }

    public Chaser(Dictionary<string, Animation> animations, Motor motor, Level level) : base(Vector2.Zero, ContentLoader.EnemyTexture, animations, motor, level)
    {
        Velocity = 1f;
        HP = 10;
        ActivationRectangle.Inflate(35, 35);
        _motor.SetEnemy(this);
    }
}