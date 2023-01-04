using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;

public class GiantBlob : Enemy
{
    public GiantBlob(Level level, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet,
        AnimationLoader.CreateBlobEnemyAnimations(), new Chase(target), level)
    {
        Velocity = 0.5f;
        HP = 150;
        XpReward = 5;
        MoneyReward = 10;
        SpriteHeight = 32;
        SpriteWidth = 32;
        Behavior.SetEnemy(this);
        DeathSound = SoundLoader.BlobDeathSound;
        Strength = 2;
    }

    public override void Attack()
    {
        int delay = 100;
        if (delay >= 100)
        {
            SpawnBulletWave();
        }

        delay++;
    }

    private void SpawnBulletWave()
    {
        CombatManager.Shoot(Position, new Vector2(0,-1), Level);
        CombatManager.Shoot(Position, new Vector2(1,-1), Level);
        CombatManager.Shoot(Position, new Vector2(0,1), Level);
        CombatManager.Shoot(Position, new Vector2(1,1), Level);
        CombatManager.Shoot(Position, new Vector2(0,1), Level);
        CombatManager.Shoot(Position, new Vector2(-1,0), Level);
        CombatManager.Shoot(Position, new Vector2(-1,0), Level);
        CombatManager.Shoot(Position, new Vector2(-1,-1), Level);
    }

}