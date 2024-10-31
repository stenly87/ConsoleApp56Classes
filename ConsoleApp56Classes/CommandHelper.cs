namespace ConsoleApp56Classes
{
    public class CommandHelper
    {
        DB db;
        Dictionary<string, Action> commands = new();
        
        public CommandHelper(DB db)
        {
            this.db = db;
            // создаем команды
            Action action = () => {
                Console.WriteLine("Укажите название нового жанра");
                string title = Console.ReadLine();
                this.db.Genres.Add(new Genre { Title = title });
                Console.WriteLine("Ваш жанр был добавлен");
            };
            commands.Add("add genre", action);
            commands.Add("list genre", ListGenre);
            commands.Add("test", Test);
        }

        public void RunCommand(string message)
        {
            if (commands.ContainsKey(message))
            {
                // запуск команды на исполнение
                commands[message]();
            }
        }

        void ListGenre()
        { 
            Console.WriteLine("Список жанров:");
            foreach (var genre in db.Genres)
                Console.WriteLine(genre.Title); 
        }

        void Test()
        {
            var p = new Publisher { Title = "test1", Coutry = "test2" };
            db.Publishers.Add(p);
            var d = new Developer { Title = "test3", Coutry = "test4" };
            db.Developers.Add(d);
            var g1 = new Genre { Title = "test5" };
            var g2 = new Genre { Title = "test6" };
            db.Genres.Add(g1);
            db.Genres.Add(g2);
            var p1 = new Platform { Title = "test7" };
            var p2 = new Platform { Title = "test8" };
            db.Platforms.Add(p1);
            db.Platforms.Add(p2);

            var game1 = new Game
            {
                Title = "Русь",
                Developer = d,
                Publisher = p,
                Genres = new List<Genre>(),
                Platforms = new List<Platform>(),
                Rating = Rating.На_один_раз,
                Release = DateTime.Now
            };

            game1.Genres.Add(g1);
            game1.Platforms.Add(p1);

            db.Games.Add(game1);

            Console.WriteLine("Тестовые данные подготовлены");
        }
    }
}
