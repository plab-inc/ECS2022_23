using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ECS2022_23.Core.Loader;

public static class SoundLoader
{
    public static SoundEffect LaserSound;
    public static SoundEffect BlobDeathSound;
    public static SoundEffect PlayerDamageSound;
    public static SoundEffect ShieldBreak;
    
    public static void LoadSounds(ContentManager content)
    {
        LaserSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_laser_sound");
        BlobDeathSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_pop");
        PlayerDamageSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_player_damage");
        ShieldBreak = content.Load<SoundEffect>("Sounds/Sfx/sfx_shield_break");
    }
    
}