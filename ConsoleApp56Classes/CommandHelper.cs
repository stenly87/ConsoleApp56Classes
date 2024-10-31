namespace ConsoleApp56Classes
{
    public class CommandHelper
    {
        DB db;
        Dictionary<Commands, Action> commands = new();

        public CommandHelper(DB db)
        {
            this.db = db;
            // создаем команды
            commands.Add(Commands.add_genre, () =>
            {
                Console.WriteLine("Укажите название нового жанра");
                string title = Console.ReadLine();
                this.db.Genres.Add(new Genre { Title = title });
                Console.WriteLine("Ваш жанр был добавлен");
            });
            commands.Add(Commands.list_genre, ListGenre);
            commands.Add(Commands.edit_genre, () =>
            {
                int index = FindGenre();
                Console.WriteLine($"Укажите новое название для жанра '{db.Genres[index].Title}'");
                db.Genres[index].Title = Console.ReadLine();
                Console.WriteLine($"Название жанра изменено на '{db.Genres[index].Title}'");
            });
            commands.Add(Commands.remove_genre, () =>
            {
                int index = FindGenre();
                if (index == -1)
                    return;
                Genre genre = db.Genres[index];
                db.Genres.RemoveAt(index);
                var games = db.Games.Where(s => s.Genres.Contains(genre));
                foreach (var game in games)
                    game.Genres.Remove(genre);
                Console.WriteLine($"Жанр удален. Кол-во затронутых игр: {games.Count()}");
            });


            commands.Add(Commands.add_platform, () =>
            {
                Console.WriteLine("Укажите название новой платформы");
                string title = Console.ReadLine();
                this.db.Platforms.Add(new Platform { Title = title });
                Console.WriteLine("Ваша платформа была добавлена");
            });
            commands.Add(Commands.list_platform, () =>
            {
                int i = 0;
                Console.WriteLine("Список платформ:");
                foreach (var p in db.Platforms)
                    Console.WriteLine($"{++i}) {p.Title}");
            });
            commands.Add(Commands.edit_platform, () =>
            {
                int index = FindPlatform();
                Console.WriteLine($"Укажите новое название для платформы '{db.Platforms[index].Title}'");
                db.Platforms[index].Title = Console.ReadLine();
                Console.WriteLine($"Название платформы изменено на '{db.Platforms[index].Title}'");
            });
            commands.Add(Commands.remove_platform, () =>
            {
                int index = FindPlatform();
                if (index == -1)
                    return;
                Platform p = db.Platforms[index];
                db.Platforms.RemoveAt(index);
                var games = db.Games.Where(s => s.Platforms.Contains(p));
                foreach (var game in games)
                    game.Platforms.Remove(p);
                Console.WriteLine($"Платформа удалена. Кол-во затронутых игр: {games.Count()}");
            });



            commands.Add(Commands.add_developer, () =>
            {
                Console.WriteLine("Укажите название новой конторы разработчиков");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите страну новой конторы разработчиков");
                string country = Console.ReadLine();
                Console.WriteLine("Укажите количество разработчиков");
                int.TryParse(Console.ReadLine(), out int number);

                this.db.Developers.Add(new Developer
                {
                    Title = title,
                    Coutry = country,
                    TeamCount = number
                });
                Console.WriteLine("Разработчики были добавлены");
            });

            commands.Add(Commands.list_developer, () =>
            {
                int i = 0;
                Console.WriteLine("Список разработчиков:");
                foreach (var p in db.Developers)
                    Console.WriteLine($"{++i}) {p.Title}");
            });

            commands.Add(Commands.edit_developer, () =>
            {
                int index = FindDeveloper();
                if (index == -1)
                    return;

                Console.WriteLine("Укажите название новой конторы разработчиков или пустую строку, чтобы оставить прежнее");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите страну новой конторы разработчиков или пустую строку, чтобы оставить прежнее");
                string country = Console.ReadLine();
                Console.WriteLine("Укажите количество разработчиков или пустую строку, чтобы оставить прежнее");
                int.TryParse(Console.ReadLine(), out int number);

                if (!string.IsNullOrEmpty(title))
                    db.Developers[index].Title = title;
                if (!string.IsNullOrEmpty(country))
                    db.Developers[index].Coutry = country;
                if (number != 0)
                    db.Developers[index].TeamCount = number;

                Console.WriteLine("Разработчики были изменены");
            });

            commands.Add(Commands.remove_developer, () => {
                int index = FindDeveloper();
                if (index == -1)
                    return;
                Developer p = db.Developers[index];
                db.Developers.RemoveAt(index);
                var games = db.Games.Where(s => s.Developer == p);
                foreach (var game in games)
                    game.Developer = db.NullDeveloper;
                Console.WriteLine($"Разработчик удален. Кол-во затронутых игр: {games.Count()}");

            });

            commands.Add(Commands.help, ListCommands);
            commands.Add(Commands.test, Test);
        }

        
        int FindPublisher()
        {
            commands[Commands.list_publisher]();
            Console.WriteLine("Укажите номер удаляемого издателя");
            bool check = int.TryParse(Console.ReadLine(), out int index);
            if (!check)
            {
                Console.WriteLine("Номер введен некорректно");
                return -1;
            }
            if (index <= 0 || index > db.Publishers.Count)
            {
                Console.WriteLine("Издателя с таким номером не существует");
                return -1;
            }
            return index - 1;
        }

        int FindDeveloper()
        {
            commands[Commands.list_developer]();
            Console.WriteLine("Укажите номер удаляемой конторы");
            bool check = int.TryParse(Console.ReadLine(), out int index);
            if (!check)
            {
                Console.WriteLine("Номер введен некорректно");
                return -1;
            }
            if (index <= 0 || index > db.Developers.Count)
            {
                Console.WriteLine("Конторы с таким номером не существует");
                return -1;
            }
            return index - 1;
        }

        int FindPlatform()
        {
            commands[Commands.list_platform]();
            Console.WriteLine("Укажите номер удаляемой платформы");
            bool check = int.TryParse(Console.ReadLine(), out int index);
            if (!check)
            {
                Console.WriteLine("Номер введен некорректно");
                return -1;
            }
            if (index <= 0 || index > db.Platforms.Count)
            {
                Console.WriteLine("Платформы с таким номером не существует");
                return -1;
            }
            return index - 1;
        }

        int FindGenre()
        {
            commands[Commands.list_genre]();
            Console.WriteLine("Укажите номер удаляемого жанра");
            bool check = int.TryParse(Console.ReadLine(), out int index);
            if (!check)
            {
                Console.WriteLine("Номер введен некорректно");
                return -1;
            }
            if (index <= 0 || index > db.Genres.Count)
            {
                Console.WriteLine("Жанра с таким номером не существует");
                return -1;
            }
            return index - 1;
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

        void ListGenre()
        {
            int i = 0;
            Console.WriteLine("Список жанров:");
            foreach (var genre in db.Genres)
                Console.WriteLine($"{++i}) {genre.Title}");
        }

        void ListCommands()
        {
            Console.WriteLine("Доступные команды:");
            Array test = Enum.GetValues(typeof(Commands));
            foreach (var t in test)
                Console.WriteLine(t.ToString().Replace('_', ' '));
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
