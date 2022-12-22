using System;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.Ui.InventoryManagement;
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

        if (Keyboard.GetState().IsKeyDown(Keys.I) && _prevState != Keyboard.GetState())
        {
            InventoryManager.Show = !InventoryManager.Show;
        }

        if (InventoryManager.Show)
        {
            InventoryInput();
        }
        else
        {
            ToolbarInput();
        }
        
        // Attack & Actions
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _prevState != Keyboard.GetState())
        {
            _player.Attack();
        } else if (Keyboard.GetState().IsKeyDown(Keys.X) && _prevState != Keyboard.GetState())
        {
            ItemManager.PickItemUp(_player);
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
            if (!_player.ImmuneToWater)
            {
                _player.Kill();
            }
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

    private void InventoryInput()
    {
        var state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Right) && state != _prevState)
        {
            InventoryManager.IncreaseIndex();
        } else if (state.IsKeyDown(Keys.Left) && state != _prevState)
        {
            InventoryManager.DecreaseIndex();
        } else if (state.IsKeyDown(Keys.Enter) && state != _prevState)
        {
            InventoryManager.UseSelectedItem(_player);
        }
    }
    
    private void ToolbarInput()
    {
        var state = Keyboard.GetState();
        
        if (state.IsKeyDown(Keys.D1) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 0);
        } else if (state.IsKeyDown(Keys.D2) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 1);
        } else if (state.IsKeyDown(Keys.D3) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 2);
        } else if (state.IsKeyDown(Keys.D4) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 3);
        } else if (state.IsKeyDown(Keys.D5) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 4);
        } else if (state.IsKeyDown(Keys.D6) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 5);
        } else if (state.IsKeyDown(Keys.D7) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 6);
        } else if (state.IsKeyDown(Keys.D8) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 7);
        } else if (state.IsKeyDown(Keys.D9) && state != _prevState)
        {
            InventoryManager.UseItemAtIndex(_player, 8);
        }
    }
}