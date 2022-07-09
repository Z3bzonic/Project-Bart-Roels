namespace ConsoleMusicPlayer
{
    internal class AsciiArt
    {
        public string[] TitleBlock()
        {
            string[] menu =
{"██████████████████████████████████████████████████████████",
 "█▄─▀█▀─▄█▄─▄▄─█▄▄▄░███▄─▄▄─█▄─▄████▀▄─██▄─█─▄█▄─▄▄─█▄─▄▄▀█",
 "██─█▄█─███─▄▄▄██▄▄░████─▄▄▄██─██▀██─▀─███▄─▄███─▄█▀██─▄─▄█",
 "▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▄▄▄▄▀▀▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀▄▄▀▄▄▀"};
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
            menuBlock.Add(" (P)lay/|| ");
            menuBlock.Add("▀██████▀█▀█");
            menuBlock.Add("    ███ █ █");
            menuBlock.Add("      █ █ █");
            menuBlock.Add("    ███ █ █");
            menuBlock.Add("▄██████▄█▄█");

            menuBlock.Add("    (S)top ");
            menuBlock.Add("███████████");
            menuBlock.Add("██      ███");
            menuBlock.Add("██      ███");
            menuBlock.Add("██      ███");
            menuBlock.Add("██▄▄▄▄▄▄███");

            menuBlock.Add("    (L)oad ");
            menuBlock.Add("███████████");
            menuBlock.Add("███▀  ▀████");
            menuBlock.Add("███    ████");
            menuBlock.Add("██▄▄▄▄▄▄███");
            menuBlock.Add("██▄▄▄▄▄▄███");

            menuBlock.Add("    (M)ute ");
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

            menuBlock.Add("    E(x)it ");
            menuBlock.Add("█▀▀████▀▀██");
            menuBlock.Add("███ ██ ████");
            menuBlock.Add("███ ▀▀ ████");
            menuBlock.Add("███ ██ ████");
            menuBlock.Add("█▄▄████▄▄██");

            menuBlock.Add("  (V)olume ");
            menuBlock.Add("███████████");
            menuBlock.Add("█████▄▄▄▄██");
            menuBlock.Add("████▄▄▄▄███");
            menuBlock.Add("███▄▄▄▄████");
            menuBlock.Add("██▄▄▄▄█████");

            menuBlock.Add("From Lis(t)");
            menuBlock.Add("███████████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██  ███████");
            menuBlock.Add("██▄▄▄▄▄▄▄██");

            menuBlock.Add("T(o)p secret");
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