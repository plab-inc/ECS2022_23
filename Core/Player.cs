using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLevelGenerator.Core;

public class Player : Entity
{
    
    public Player(Texture2D texture) : base(texture)
    {
    }



    public override void Update(GameTime gameTime)
    {
        var velocity = new Vector2();

        var speed = 8f;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            velocity.Y = -speed;
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
            velocity.Y = speed;

        if (Keyboard.GetState().IsKeyDown(Keys.A))
            velocity.X = -speed;
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
            velocity.X = speed;

        bool canMove = true;
        
        
        if (canMove)
        {
            Position += velocity;
        }
        
    }
}