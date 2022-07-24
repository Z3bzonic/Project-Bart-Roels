using ConsoleMusicPlayer;
using ConsoleMusicPlayer.Business;
using ConsoleMusicPlayer.Frontend;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine(args[0]);
            Console.ReadLine();
        }

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        SetColorCombo colorCombo = new SetColorCombo();
        AsciiArt asciiArt = new AsciiArt();
        Positions position = new Positions();
        EasterEgg visualizer = new EasterEgg();
        Messages messages = new Messages(colorCombo, asciiArt, position, visualizer);
        FileFunctions fileFunctions = new FileFunctions();
        StatesDTO statesDTO = new StatesDTO();
        ConsoleMediaPlayer consoleMediaPlayer = new ConsoleMediaPlayer(messages, fileFunctions, statesDTO);

        int choice = 0;
        bool choiceTest = false;
        messages.RenderTitle();
        messages.RenderAsciiMenu();
        consoleMediaPlayer.PrepareSongList(fileFunctions.GetMusicFromFolder());

        while (choice != 9)
        {
            messages.DisplayMenuChoice();
            choiceTest = int.TryParse(Console.ReadLine(), out choice);
            if (choiceTest)
            {
                messages.ClearNoFileLoadedMessage();
                consoleMediaPlayer.ChoicePatchTrough(choice);
            }
        }
    }
}