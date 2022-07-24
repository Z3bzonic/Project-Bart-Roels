namespace ConsoleMusicPlayer.Business
{
    public class StatesDTO
    {
        public bool ToggleStatePlayer { get; set; }
        public bool ToggleMutePlayer { get; set; }
        public bool PathPresent { get; set; }
        public int Volume { get; set; } = 50;
        public string SongName { get; set; } = "";
        public int SongListCycle { get; set; }

        public int GetSomethingHeavyFromDatabase()
        {
            return 1;
        }
    }
}