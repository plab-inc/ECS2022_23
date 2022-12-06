using System;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Ui;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECS2022_23.Core;

public class Input
{
    private Player _player;
    private KeyboardState _prevState;
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
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _prevState != Keyboard.GetState())
        {
            _player.Attack();
        } else if (Keyboard.GetState().IsKeyDown(Keys.X) && _prevState != Keyboard.GetState())
        {
            ItemManager.PickItemUp(_player);
        } else if (Keyboard.GetState().IsKeyDown(Keys.D1) && _prevState != Keyboard.GetState())
        {
            if (_player.Items?.Count > 0)
            {
                var item = UiManager.UseItemAtIndex(1, _player);
                if(item != null) _player.UseItem(item);
            }
        } else if (Keyboard.GetState().IsKeyDown(Keys.D2) && _prevState != Keyboard.GetState())
        {
            if (_player.Items?.Count > 0)
            {
                var item = UiManager.UseItemAtIndex(3, _player);
                if(item != null) _player.UseItem(item);
            }
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

        if (_player.IsInWater(_player.Rectangle))
        {
            _player.Kill();
            Console.WriteLine("I'm ded");
        }

        if (!_player.Collides(velocity)) 
            return;
        
        
        _player.Position += velocity;
        _player.SetAnimation(animation);
        _prevState = Keyboard.GetState();
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