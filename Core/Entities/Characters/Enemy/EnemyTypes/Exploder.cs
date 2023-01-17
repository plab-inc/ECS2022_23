using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;


namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Exploder : Enemy
{
    public Exploder(Stage stage) : base(Vector2.Zero, UiLoader.SpriteSheet,
        AnimationLoader.CreateBlobEnemyAnimations(), new RandomBehavior(), stage)
    {
        Velocity = 1f;
        HP = 10;
        Strength = 1;
        EpReward = 1;

        Color = Color.GreenYellow;
        DeathSound = SoundLoader.BlobDeathSound;
        ItemSpawnRate = 10f;
    }

    public override void OnDeath()
    {
        CombatManager.Shoot(Position, new Vector2(0,-1), Stage);
        CombatManager.Shoot(Position, new Vector2(1,-1), Stage);
        CombatManager.Shoot(Position, new Vector2(1,0), Stage);
        CombatManager.Shoot(Position, new Vector2(1,1), Stage);
        CombatManager.Shoot(Position, new Vector2(0,1), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,1), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,0), Stage);
        CombatManager.Shoot(Position, new Vector2(-1,-1), Stage);
    }
}