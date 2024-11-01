using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        public void AddPlaformCommands()
        {
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
                var games = db.Games.Where(s => s.Platforms.Contains(p)).ToArray();
                foreach (var game in games)
                    game.Platforms.Remove(p);
                Console.WriteLine($"Платформа удалена. Кол-во затронутых игр: {games.Length}");
            });
        }

        int FindPlatform()
        {
            commands[Commands.list_platform]();
            Console.WriteLine("Укажите номер платформы");
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
    }
}
