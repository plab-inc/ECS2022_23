using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ECS2022_23.Core.Loader;

public static class SoundLoader
{

    public static SoundEffect Background;
    public static SoundEffect Blueberry;
    public static SoundEffect Ominous;
    
    public static SoundEffect LaserSound;
    public static SoundEffect BlobDeathSound;
    public static SoundEffect PlayerDamageSound;
    public static SoundEffect ShieldBreakSound;
    public static SoundEffect LevelUpSound;
    
    public static SoundEffect PickUpItemSound;
    public static SoundEffect PlayerDrownASound;
    public static SoundEffect PlayerDrownBSound;
    public static SoundEffect UnlockDoorSound;
    
    
    public static void LoadSounds(ContentManager content)
    {
        LaserSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_laser_sound");
        BlobDeathSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_pop");
        PlayerDamageSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_player_damage");
        ShieldBreakSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_shield_break");
        LevelUpSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_level_up");
        PickUpItemSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_item_pickup");
        PlayerDrownASound = content.Load<SoundEffect>("Sounds/Sfx/sfx_drown_1");
        PlayerDrownBSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_drown_2");
        UnlockDoorSound = content.Load<SoundEffect>("Sounds/Sfx/sfx_door_unlock");
        
        Background = content.Load<SoundEffect>("GameStateManagement/Sounds/Music/music_background");
        Blueberry = content.Load<SoundEffect>("GameStateManagement/Sounds/Music/music_blueberry");
        Ominous = content.Load<SoundEffect>("GameStateManagement/Sounds/Music/music_ominous");
        
    }
    
}