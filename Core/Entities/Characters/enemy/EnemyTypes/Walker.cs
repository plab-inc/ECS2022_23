using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class Walker : Enemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public Walker(Level level) : base(Vector2.Zero, UiLoader.GetSpritesheet(), AnimationLoader.CreateBlobEnemyAnimations(), new RandomMotor(level), level)
    {
        Velocity = 1f;
        HP = 10;
        ActivationRectangle.Inflate(35, 35);
        Motor.SetEnemy(this);
        MoneyReward = 1;
        Color = Color.Cyan;
        DeathSound = SoundLoader.BlobDeathSound;
    }
}