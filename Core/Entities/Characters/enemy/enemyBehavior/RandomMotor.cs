using System;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class RandomMotor : Motor
{
    int delay=0;
    Random rand = new Random();
    private int LastDirection;
    
    public override Vector2 Move(Vector2 position, int velocity)
    {
        // Enemy chooses a Direction and stays on it for X seconds
        
        delay++;
        int newDirection = LastDirection;
        
        // This binds the speed of directional change to the FPS. Could result in some unexpected behavior should the FPS change.
        if (delay >= 20)
        {
            delay = 0;
            newDirection = rand.Next(0, 4);
        }

        LastDirection = newDirection;
        
        // Directions in Order: UP, DOWN, LEFT, Right
        switch (newDirection)
        {
            case 0 :
                return new Vector2(position.X, position.Y+velocity);
            case 1:
                return new Vector2(position.X, position.Y-velocity);
            case 2:
                return new Vector2(position.X - velocity, position.Y);
            case 3:
                return new Vector2(position.X + velocity, position.Y);
        }
        return position;
    }

    public override bool Collides(float velocity)
    {
        return false;
    }
}