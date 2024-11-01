namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        DB db;
        Dictionary<Commands, Action> commands = new();

        public CommandHelper(DB db)
        {
            this.db = db;

            AddGenreCommands();
            AddPlaformCommands();
            AddDeveloperCommands();
            AddCommandsPublisher();
            AddGameCommands();

            commands.Add(Commands.help, ListCommands);
        }
                
        public void RunCommand(string message)
        {
            Commands command = GetCommand(ref message);
            if (commands.ContainsKey(command))
            {
                // запуск команды на исполнение
                commands[command]();
            }
        }

        Commands GetCommand(ref string message)
        {
            message = message.Replace(' ', '_');
            var test = Enum.GetValues(typeof(Commands));
            foreach (var t in test)
            {
                if (t.ToString() == message)
                {
                    return (Commands)t;
                }
            }
            return Commands.exit;
        }

        void ListCommands()
        {
            Console.WriteLine("Доступные команды:");
            Array test = Enum.GetValues(typeof(Commands));
            foreach (var t in test)
                Console.WriteLine(t.ToString().Replace('_', ' '));
        }
    }
}
