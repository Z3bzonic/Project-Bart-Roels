namespace ConsoleMusicPlayer.Business
{
    public class FileFunctions
    {
        public FileFunctions()
        {
        }

        public string GetFullFilePath(string fileName)
        {
            string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string songName = $"\\{fileName}.mp3";
            string fullPath = musicFolder + songName;
            return fullPath;
        }

        public bool FileExists(string fullPath)
        {
            return File.Exists(fullPath);
        }

        //public string LoadSong(string loadedSong = "")
        //{
        //    if (loadedSong == "")
        //    {
        //        _messages.DisplayLoadManually();
        //        string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        //        string songName = $"\\{Console.ReadLine()}.mp3";
        //        string fullPath = musicFolder + songName;
        //        fullPath = File.Exists(fullPath) ? fullPath : LoadSong();
        //    }
        //    return loadedSong;
        //}

        public string[] GetMusicFromFolder()
        {
            string presentMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string[] getSongsFromMusicFolder = Directory.GetFiles(presentMusicFolder, "*.mp3", SearchOption.TopDirectoryOnly);
            return getSongsFromMusicFolder;
        }
    }
}