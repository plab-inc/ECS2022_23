using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ECS2022_23.Core.Sound;

public static class SoundManager
{
    public static void Play(SoundEffect sound)
    {
        sound.Play();
    }

    public static void PlayMusic(Song song)
    {
        MediaPlayer.Play(song);
    }

    public static void StopMusic()
    {
        MediaPlayer.Stop();
    }

    public static void Initialize()
    {
        MediaPlayer.Volume = 0.005f;
        SoundEffect.MasterVolume = 0.025f;
        //PlayMusic(ContentLoader.BackgroundMusic);
    }
}