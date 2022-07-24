namespace ConsoleMusicPlayer.Business
{
    public static class FileFunctions
    {
        public static string GetFullFilePath(string fileName)
        {
            var musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            var songName = $"\\{fileName}.mp3";
            var fullPath = musicFolder + songName;
            return fullPath;
        }

        public static bool FileExists(string fullPath) => File.Exists(fullPath);

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

        public static string[] GetMusicFromFolder()
        {
            string presentMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            var getSongsFromMusicFolder = Directory.GetFiles(presentMusicFolder, "*.mp3", SearchOption.TopDirectoryOnly);
            return getSongsFromMusicFolder;
        }
    }
}