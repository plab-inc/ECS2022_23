using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class Walker : Enemy
{
   public Walker(Level level) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateBlobEnemyAnimations(), new RandomBehavior(), level)
    {
        Velocity = 1f;
        HP = 10;
        MoneyReward = 1;
        XpReward = 1;
        
        ActivationRectangle.Inflate(35, 35);
        Behavior.SetEnemy(this);
        
        Color = Color.Cyan;
        DeathSound = SoundLoader.BlobDeathSound;
    }
   
   public override void Attack()
   {
        
   }
   
}