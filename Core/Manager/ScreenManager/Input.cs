using System.Diagnostics;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement;

public static class Input
{
    private static MouseState mouseState, lastMouseState;

    private static Vector2 MousePosition => new Vector2(mouseState.X, mouseState.Y);

    private static KeyboardState keyboardState, lastKeyboardState;

    public static void Update(InputState input, int playerIndex)
    {
        lastKeyboardState = keyboardState;
        lastMouseState = mouseState;
        
        keyboardState = input.CurrentKeyboardStates[playerIndex];;
        mouseState = Mouse.GetState();

    }

    public static Vector2 GetMovementDirection()
    {
        Vector2 direction = Vector2.Zero;
        
        if (keyboardState.IsKeyDown(Keys.A))
        {
            direction.X -= 1;
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            direction.X += 1;
        }
        if (keyboardState.IsKeyDown(Keys.W))
        {
            direction.Y -= 1;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            direction.Y += 1;
        }
        // Clamp the length of the vector to a maximum of 1.
        if (direction.LengthSquared() > 1)
            direction.Normalize();

        return direction;
    }
    public static Direction GetAimDirection()
    {
        
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            return Direction.Left;
        }
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            return Direction.Right;
        }
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            return Direction.Up;
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            return Direction.Down;
        }
        // If there's no aim input
        return Direction.None;
    }

    public static Action GetPlayerAction()
    {
        if (keyboardState.IsKeyDown(Keys.E) && keyboardState != lastKeyboardState)
        {
            return Action.OpensLocker;
        }
        if (keyboardState.IsKeyDown(Keys.X) && keyboardState != lastKeyboardState)
        {
            return Action.PicksUpItem;
        }
        if (keyboardState.IsKeyDown(Keys.Space) && keyboardState != lastKeyboardState)
        {
            return Action.Attacks;
        }
        if (ToolbarKeyDownIndex() != -1 && keyboardState != lastKeyboardState)
        {
            return Action.UseItem;
        }
        
        return Action.None;
    }
    public static int ToolbarKeyDownIndex()
    {
        for (var i = 49; i <= 57; i++)
        {
            if (keyboardState.IsKeyDown((Keys) i ))
            {
                return i - 49;
            }
        }

        return -1;
    }
    
}