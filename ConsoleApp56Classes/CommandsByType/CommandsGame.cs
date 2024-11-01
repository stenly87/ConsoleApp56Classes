using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        public void AddGameCommands()
        {
            commands.Add(Commands.add_game, () =>
            {
                Console.WriteLine("Укажите название новой игры");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите дату выхода новой игры");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Неверная дата, выполнение прервано");
                    return;
                }
                ListGameRating();
                Console.WriteLine("Укажите номер рейтинга новой игры");
                int.TryParse(Console.ReadLine(), out int rating);
                Rating rate = (Rating)(--rating);

                int tryCount = 0;
                int dev = FindDeveloper();
                while (dev == -1 && tryCount < 5)
                {
                    dev = FindDeveloper();
                    tryCount++;
                }

                tryCount = 0;
                int pub = FindPublisher();
                while (pub == -1 && tryCount < 5)
                {
                    pub = FindPublisher();
                    tryCount++;
                }

                Game game = new Game
                {
                    Title = title,
                    Rating = rate,
                    Release = date,
                    Developer = dev != -1 ? db.Developers[dev] : db.NullDeveloper,
                    Publisher = pub != -1 ? db.Publishers[pub] : db.NullPublisher,
                    Genres = new List<Genre>(),
                    Platforms = new List<Platform>()
                };

                commands[Commands.list_genre]();
                Console.WriteLine("Введите номера жанров, которые нужно добавить в игру, чтобы прекратить, укажите 0");
                int index;
                do
                {
                    int.TryParse(Console.ReadLine(), out index);
                    if (index == 0)
                        break;
                    if (index > 0 && index <= db.Genres.Count)
                    {
                        if (!game.Genres.Contains(db.Genres[index - 1]))
                        {
                            game.Genres.Add(db.Genres[index - 1]);
                            Console.WriteLine($"В игру добавлен жанр {db.Genres[index - 1].Title}");
                        }
                    }
                }
                while (true);

                commands[Commands.list_platform]();
                Console.WriteLine("Введите номера платформ, которые нужно добавить в игру, чтобы прекратить, укажите 0");
                index = 0;
                do
                {
                    int.TryParse(Console.ReadLine(), out index);
                    if (index == 0)
                        break;
                    if (index > 0 && index <= db.Platforms.Count)
                    {
                        if (!game.Platforms.Contains(db.Platforms[index - 1]))
                        {
                            game.Platforms.Add(db.Platforms[index - 1]);
                            Console.WriteLine($"В игру добавлена платформа {db.Platforms[index - 1].Title}");
                        }
                    }
                }
                while (true);

                db.Games.Add(game);
                Console.WriteLine("Игра добавлена!");
            });

            commands.Add(Commands.list_game, () =>
            {
                int i = 0;
                Console.WriteLine("Список игр:");
                foreach (var game in db.Games)
                {
                    Console.WriteLine($"{++i}) {game.Title} дата выхода: {game.Release} рейтинг: {game.Rating.ToString().Replace('_', ' ')}");
                    Console.WriteLine($"Разработчик {game.Developer.Title}");
                    Console.WriteLine($"Издатель {game.Publisher.Title}");
                    Console.WriteLine($"Жанры: {string.Join(", ", game.Genres.Select(s => s.Title))}");
                    Console.WriteLine($"Платформы: {string.Join(", ", game.Platforms.Select(s => s.Title))}");
                }
            });
            commands.Add(Commands.edit_game, () =>
            {
                int index = FindGame();
                if (index == -1)
                    return;

                Game game = db.Games[index];

                Console.WriteLine("Укажите новое название для игры или пустую строку");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите дату выхода игры или пустую строку");
                string dateString = Console.ReadLine();
                DateTime date;
                if (!string.IsNullOrEmpty(dateString))
                {
                    if (!DateTime.TryParse(dateString, out date))
                    {
                        Console.WriteLine("Неверная дата, выполнение прервано");
                        return;
                    }
                }
                else
                    date = game.Release;

                ListGameRating();
                Console.WriteLine("Укажите номер рейтинга игры или пустую строку");
                string rateString = Console.ReadLine();
                Rating rate;
                if (!string.IsNullOrEmpty(rateString))
                {
                    int.TryParse(rateString, out int rating);
                    rate = (Rating)(--rating);
                }
                else
                    rate = game.Rating;

                int dev = FindDeveloper();
                int pub = FindPublisher();
                bool pleaseSayNo = true;
                while (pleaseSayNo)
                {
                    Console.WriteLine("Удалить жанры из игры? да/нет");
                    pleaseSayNo = Console.ReadLine() == "да";
                    if (pleaseSayNo)
                    {
                        int i = 0;
                        Console.WriteLine("Список жанров игры:");
                        foreach (var genre in game.Genres)
                            Console.WriteLine($"{++i}) {genre.Title}");
                        Console.WriteLine("Какой номер удаляем?");
                        int.TryParse(Console.ReadLine(), out int removeGenre);
                        if (removeGenre < game.Genres.Count)
                            game.Genres.RemoveAt(removeGenre);
                    }
                }

                commands[Commands.list_genre]();
                Console.WriteLine("Введите номера жанров, которые нужно добавить в игру, чтобы прекратить, укажите 0");
                index = 0;
                do
                {
                    int.TryParse(Console.ReadLine(), out index);
                    if (index == 0)
                        break;
                    if (index > 0 && index <= db.Genres.Count)
                    {
                        if (!game.Genres.Contains(db.Genres[index - 1]))
                        {
                            game.Genres.Add(db.Genres[index - 1]);
                            Console.WriteLine($"В игру добавлен жанр {db.Genres[index - 1].Title}");
                        }
                    }
                }
                while (true);

                pleaseSayNo = true;
                while (pleaseSayNo)
                {
                    Console.WriteLine("Удалить платформы из игры? да/нет");
                    pleaseSayNo = Console.ReadLine() == "да";
                    if (pleaseSayNo)
                    {
                        int i = 0;
                        Console.WriteLine("Список платформ игры:");
                        foreach (var p in game.Platforms)
                            Console.WriteLine($"{++i}) {p.Title}");
                        Console.WriteLine("Какой номер удаляем?");
                        int.TryParse(Console.ReadLine(), out int removePlatform);
                        if (removePlatform < game.Platforms.Count)
                            game.Platforms.RemoveAt(removePlatform);
                    }
                }

                commands[Commands.list_platform]();
                Console.WriteLine("Введите номера платформ, которые нужно добавить в игру, чтобы прекратить, укажите 0");
                index = 0;
                do
                {
                    int.TryParse(Console.ReadLine(), out index);
                    if (index == 0)
                        break;
                    if (index > 0 && index <= db.Platforms.Count)
                    {
                        if (!game.Platforms.Contains(db.Platforms[index - 1]))
                        {
                            game.Platforms.Add(db.Platforms[index - 1]);
                            Console.WriteLine($"В игру добавлена платформа {db.Platforms[index - 1].Title}");
                        }
                    }
                }
                while (true);

                game.Title = title;
                game.Release = date;
                game.Rating = rate;

                game.Developer = dev != -1 ? db.Developers[dev] : db.NullDeveloper; ;
                game.Publisher = pub != -1 ? db.Publishers[pub] : db.NullPublisher;

                Console.WriteLine("Игра отредактирована");
            });

            commands.Add(Commands.remove_game, () =>
            {
                int index = FindGame();
                if (index == -1)
                    return;

                db.Games.RemoveAt(index);
                Console.WriteLine("Игра удалена");
            });
        }

        int FindGame()
        {
            commands[Commands.list_game]();
            Console.WriteLine("Укажите номер игры");
            bool check = int.TryParse(Console.ReadLine(), out int index);
            if (!check)
            {
                Console.WriteLine("Номер введен некорректно");
                return -1;
            }
            if (index <= 0 || index > db.Games.Count)
            {
                Console.WriteLine("Игры с таким номером не существует");
                return -1;
            }
            return index - 1;
        }

        void ListGameRating()
        {
            int i = 0;
            var test = Enum.GetValues(typeof(Rating));
            foreach (var t in test)
            {
                Console.WriteLine($"{++i}) {t.ToString().Replace('_', ' ')}");
            }
        }

    }
}
