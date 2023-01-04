using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;


namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class Gunner : Enemy
{
    private Character _target;
    private int delay;
    public Gunner(Level level, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet , AnimationLoader.CreateEyeEnemyAnimations(), new Dodger(target), level)
    {
        Velocity = 1f;
        HP = 10;
        MoneyReward = 1;
        XpReward = 1;
        _target = target;
        ActivationRectangle.Inflate(35, 35);
        Behavior.SetEnemy(this);
        Strength = 1;
        
        Color = Color.Cyan;
        DeathSound = SoundLoader.BlobDeathSound;
    }

    public override void Attack()
    {
        if (Vector2.Distance(Position, _target.Position)>50)
        {
            if (++delay > 50)
            {
                delay = 0;
                CombatManager.Shoot(this);
            }
        }
        else
        {
            Behavior.State = (int)EnemyStates.Move;
        }
    }
}