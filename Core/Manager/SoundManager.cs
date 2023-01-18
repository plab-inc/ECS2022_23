using Microsoft.Xna.Framework.Audio;

namespace ECS2022_23.Core.Sound;

public static class SoundManager
{
    private static SoundEffectInstance SoundEffectInstance;

    public static void Play(SoundEffect sound)
    {
        sound.Play();
    }

    public static void StopMusic()
    {
        SoundEffectInstance?.Dispose();
    }

    public static void PlayMusic(SoundEffect Sound)
    {
        if (Sound == null) return;

        SoundEffectInstance = Sound.CreateInstance();
        SoundEffectInstance.IsLooped = true;
        SoundEffectInstance.Volume = 0.5f;
        SoundEffectInstance.Play();
    }

    public static void Initialize()
    {
        SoundEffect.MasterVolume = 0.8f;
    }
}