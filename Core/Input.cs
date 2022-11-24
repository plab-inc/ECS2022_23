using System;
using ECS2022_23.Core.entities.characters;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLevelGenerator.Core;

public class Input
{
    private Character _character;

    public Input(Character character)
    {
        _character = character;
    }

    public void Move()
    {
        var velocity = new Vector2();
        var speed = 3f;
        var animation = "Default";

        // Attack & Actions
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            _character.Attack();
        } 
        
        // Diagonal Movement
        if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
        {
            // UP + RIGHT
            velocity.X = speed;
            velocity.Y = -speed;
            animation = "WalkRight";
        } else if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A))
        {
            // UP + LEFT
            velocity.X = -speed;
            velocity.Y = -speed;
            animation = "WalkLeft";
        }else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
        {
            //DOWN + RIGHT
            velocity.X = speed;
            velocity.Y = speed;
            animation = "WalkRight";
        }else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
        {
            //DOWN + LEFT
            velocity.X = -speed;
            velocity.Y = speed;
            animation = "WalkLeft";
        }
        else // Straight Movement & Attack
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y = -speed;
                animation = "WalkUp";
            } else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y = speed;
                animation = "WalkDown";
            } else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -speed;
                animation = "WalkLeft";
            } else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = speed;
                animation = "WalkRight";
            } 
        }
        
        _character.Position += velocity;
        _character.SetAnimation(animation);
    }
    public void Aim()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            _character.AimDirection = (int) Direction.Up;

        } else if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            _character.AimDirection = (int) Direction.Down;
        } else if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _character.AimDirection = (int) Direction.Left;
        } else if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _character.AimDirection = (int) Direction.Right;
        } 
    }
}