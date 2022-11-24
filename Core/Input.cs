using System;
using ECS2022_23.Core.entities.characters;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLevelGenerator.Core;

public class Input
{
    private Player _player;

    public Input(Player character)
    {
        _player = character;
    }

    public void Move()
    {
        var velocity = new Vector2();
        var speed = 3f;
        var animation = "Default";

        // Attack & Actions
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            _player.Attack();
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

        if (!_player.Collides(velocity)) 
            return;
        
        _player.Position += velocity;
        _player.SetAnimation(animation);
    }
    public void Aim()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            _player.AimDirection = (int) Direction.Up;

        } else if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            _player.AimDirection = (int) Direction.Down;
        } else if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _player.AimDirection = (int) Direction.Left;
        } else if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _player.AimDirection = (int) Direction.Right;
        } 
    }
}