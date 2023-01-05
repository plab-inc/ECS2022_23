using System;
using ECS2022_23.Core.Loader;
using ECS2022_23.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ECS2022_23.Core.Sound;

public static class SoundManager
{
    private static GameTime currentGameTime;
    public static void Play(SoundEffect sound)
    {
        sound.Play();
    }

    public static void Update(GameTime gameTime)
    {
        currentGameTime = gameTime;
        Console.WriteLine(gameTime);
    }

    public static void PlayMusic(SoundEffect Sound)
    {
        SoundEffectInstance SongInstance = Sound.CreateInstance();
        SongInstance.IsLooped = true;
        SongInstance.Volume = 0.5f;
        SongInstance.Play();
    }
    
    public static void Initialize()
    {
        SoundEffect.MasterVolume = 0.8f;
    }
}