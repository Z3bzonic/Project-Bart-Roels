using System.ComponentModel;

namespace ConsoleMusicPlayer.Common.Enums
{
    public enum UserOptions
    {
        [Description("None")]
        None = 0,

        [Description("Play / Pause")]
        Play_Pause = 1,
        
        [Description("Stop")]
        Stop = 2,

        [Description("Load")]
        Load = 3,

        [Description("Load from list")]
        LoadFromList = 4,

        [Description("Mute / Unmute")]
        Mute_Unmute = 5,

        [Description("Volume")]
        Volume = 6,

        [Description("Visualizer")]
        Visualizer = 8,

        [Description("Exit")]
        Exit = 9
    }
}