﻿using Microsoft.Xna.Framework;

namespace ECS2022_23.Helper;

public class Timer
{
    private readonly float _limit;
    private float _timer;

    public Timer(float timeLimit)
    {
        _limit = timeLimit;
    }

    public void Update(GameTime gameTime)
    {
        _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
    }

    public bool LimitReached()
    {
        if (_timer >= _limit)
        {
            Reset();
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _timer = 0f;
    }
}