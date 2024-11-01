using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        public void AddGenreCommands()
        {
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
                var games = db.Games.Where(s => s.Genres.Contains(genre)).ToArray();
                foreach (var game in games)
                    game.Genres.Remove(genre);
                Console.WriteLine($"Жанр удален. Кол-во затронутых игр: {games.Length}");
            });
        }

        void ListGenre()
        {
            int i = 0;
            Console.WriteLine("Список жанров:");
            foreach (var genre in db.Genres)
                Console.WriteLine($"{++i}) {genre.Title}");
        }

        int FindGenre()
        {
            commands[Commands.list_genre]();
            Console.WriteLine("Укажите номер жанра");
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

    }
}
