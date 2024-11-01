using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        public void AddDeveloperCommands()
        {
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

                Console.WriteLine("Укажите новое название конторы разработчиков или пустую строку, чтобы оставить прежнее");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите новую страну конторы разработчиков или пустую строку, чтобы оставить прежнее");
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

            commands.Add(Commands.remove_developer, () =>
            {
                int index = FindDeveloper();
                if (index == -1)
                    return;
                Developer p = db.Developers[index];
                db.Developers.RemoveAt(index);
                var games = db.Games.Where(s => s.Developer == p).ToArray();
                foreach (var game in games)
                    game.Developer = db.NullDeveloper;
                Console.WriteLine($"Разработчик удален. Кол-во затронутых игр: {games.Length}");

            });
        }

        int FindDeveloper()
        {
            commands[Commands.list_developer]();
            Console.WriteLine("Укажите номер конторы");
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

    }
}
