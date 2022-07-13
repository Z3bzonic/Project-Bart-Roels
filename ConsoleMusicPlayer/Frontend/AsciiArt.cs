namespace ConsoleMusicPlayer.Frontend
{
    internal class AsciiArt
    {
        public string[] TitleBlock()
        {
            string[] menu =
{"██████████████████████████████████████████████████████████████████████████████████████",
 "███████████████▄─▀█▀─▄█▄─▄▄─█▄▄▄░███▄─▄▄─█▄─▄████▀▄─██▄─█─▄█▄─▄▄─█▄─▄▄▀███By██████████",
 "████████████████─█▄█─███─▄▄▄██▄▄░████─▄▄▄██─██▀██─▀─███▄─▄███─▄█▀██─▄─▄███DiscoBart███",
 "██████████████▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▄▄▄▄▀▀▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀▄▄▀▄▄▀██████████████"};
            return menu;
        }

        public string ErrorNoSong()
        {
            string errorNoSong = @"
 █▄░█ █▀█   █▀ █▀█ █▄░█ █▀▀   █▀▀ █▀█ █░█ █▄░█ █▀▄
 █░▀█ █▄█   ▄█ █▄█ █░▀█ █▄█   █▀░ █▄█ █▄█ █░▀█ █▄▀";

            return errorNoSong;
        }

        public List<string> MenuBlock()
        {
            List<string> menuBlock = new List<string>();
            menuBlock.Add("(1)Play/|| ");
            menuBlock.Add("▀██████▀█▀█");
            menuBlock.Add("    ███ █ █");
            menuBlock.Add("      █ █ █");
            menuBlock.Add("    ███ █ █");
            menuBlock.Add("▄██████▄█▄█");

            menuBlock.Add("(2)  Stop  ");
            menuBlock.Add("███████████");
            menuBlock.Add("██      ███");
            menuBlock.Add("██      ███");
            menuBlock.Add("██      ███");
            menuBlock.Add("██▄▄▄▄▄▄███");

            menuBlock.Add("(3)  Load  ");
            menuBlock.Add("███████████");
            menuBlock.Add("███▀  ▀████");
            menuBlock.Add("███    ████");
            menuBlock.Add("██▄▄▄▄▄▄███");
            menuBlock.Add("██▄▄▄▄▄▄███");

            menuBlock.Add("(4) List   ");
            menuBlock.Add("███████████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██▄▄▄▄▄▄▄██");

            menuBlock.Add("(5)  Mute  ");
            menuBlock.Add("██▀▀███████");
            menuBlock.Add("███  ██████");
            menuBlock.Add("████  █████");
            menuBlock.Add("█████  ████");
            menuBlock.Add("██████▄▄███");

            //menuBlock.Add("    (N)ext ");
            //menuBlock.Add("██▀██▀█████");
            //menuBlock.Add("██  █  ████");
            //menuBlock.Add("██  |   ███");
            //menuBlock.Add("██  █  ████");
            //menuBlock.Add("██▄██▄█████");

            menuBlock.Add("(6) Volume ");
            menuBlock.Add("███████████");
            menuBlock.Add("█████▄▄▄▄██");
            menuBlock.Add("████▄▄▄▄███");
            menuBlock.Add("███▄▄▄▄████");
            menuBlock.Add("██▄▄▄▄█████");

            menuBlock.Add("(9)  Exit  ");
            menuBlock.Add("█▀▀████▀▀██");
            menuBlock.Add("███ ██ ████");
            menuBlock.Add("███ ▀▀ ████");
            menuBlock.Add("███ ██ ████");
            menuBlock.Add("█▄▄████▄▄██");

            menuBlock.Add("(8) Secret ");
            menuBlock.Add("███████████");
            menuBlock.Add("██  ███  ██");
            menuBlock.Add("███████████");
            menuBlock.Add("██       ██");
            menuBlock.Add("██▄▄▄▄▄▄▄██");
            return menuBlock;
        }

        public string VolumeBar()
        {
            string volumeBar = "▄";
            return volumeBar;
        }

        public string VolumePlaceHolder()
        {
            string placeHolder = "▀";
            return placeHolder;
        }
    }
}