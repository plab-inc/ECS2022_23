using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ECS2022_23.Core.Loader;

public static class SoundLoader
{
    public static SoundEffect LaserSound;
    public static Song BackgroundMusic;
    public static SoundEffect BlobDeathSound;
    
    
    public static void LoadSounds(ContentManager content)
    {
        LaserSound = content.Load<SoundEffect>("sound/laserSound");
        BackgroundMusic = content.Load<Song>("sound/backgroundMusic");
        BlobDeathSound = content.Load<SoundEffect>("sound/slimeDeath");
    }
    
}