using ConsoleMusicPlayer;
using ConsoleMusicPlayer.Business;
using ConsoleMusicPlayer.Common.Enums;
using ConsoleMusicPlayer.Frontend;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine(args[0]);
            Console.ReadLine();
        }

        Console.OutputEncoding = Encoding.UTF8;
        var colorCombo = new SetColorCombo();
        var asciiArt = new AsciiArt();
        var position = new Positions();
        var visualizer = new EasterEgg();
        var messages = new Messages(colorCombo, asciiArt, position, visualizer);
        var statesDTO = new StatesDTO();
        var consoleMediaPlayer = new ConsoleMediaPlayer(messages, statesDTO);

        messages.RenderTitle();
        messages.RenderAsciiMenu();
        consoleMediaPlayer.PrepareSongList(FileFunctions.GetMusicFromFolder());

        UserOptions userOption = UserOptions.None;
        while (userOption != UserOptions.Exit)
        {
            messages.DisplayMenuChoice();
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                userOption = (UserOptions)choice;

                messages.ClearNoFileLoadedMessage();
                consoleMediaPlayer.ChoicePatchTrough(userOption);
            }
        }
    }
}