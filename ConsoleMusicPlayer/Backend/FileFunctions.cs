using ConsoleMusicPlayer.Frontend;

namespace ConsoleMusicPlayer.Backend
{
    internal class FileFunctions
    {
        private Messages _messages;

        public FileFunctions(Messages messages)
        {
            _messages = messages;
        }

        public string LoadSong(string loadedSong = "")
        {
            if (loadedSong == "")
            {
                _messages.DisplayLoadManually();
                string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                string songName = $"\\{Console.ReadLine()}.mp3";
                string fullPath = musicFolder + songName;
                FileInfo fileInfo = new FileInfo(fullPath);
                bool checkPath = fileInfo.Exists;
                if (checkPath)
                {
                    return fullPath;
                }
                else
                {
                    LoadSong();
                }
            }
            return loadedSong;
        }

        public string[] GetMusicFromFolder()
        {
            string presentMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string[] getSongsFromMusicFolder = Directory.GetFiles(presentMusicFolder, "*.mp3", SearchOption.TopDirectoryOnly);
            return getSongsFromMusicFolder;
        }
    }
}