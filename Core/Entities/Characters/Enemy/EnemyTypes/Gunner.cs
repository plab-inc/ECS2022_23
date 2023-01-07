using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Gunner : Enemy
{
    private Character _target;
    private int delay;
    public Gunner(Stage stage, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet , AnimationLoader.CreateEyeEnemyAnimations(), new Dodger(target), stage)
    {
        Behavior.SetEnemy(this);
        
        Velocity = 1f;
        HP = 10;
        Strength = 1;
        EpReward = 2;

        ActivationRadius = 150f;
        _target = target;

        Color = Color.Cyan;
        DeathSound = SoundLoader.BlobDeathSound;
    }

    public override void Attack()
    {
        if(!IsActive)
            return;
        
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