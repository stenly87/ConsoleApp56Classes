using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public partial class CommandHelper
    {
        public void AddCommandsPublisher()
        {
            commands.Add(Commands.add_publisher, () =>
            {
                Console.WriteLine("Укажите название нового издателя");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите страну нового издателя");
                string country = Console.ReadLine();
                Console.WriteLine("Есть ли издатель в России? да/нет");
                bool result = Console.ReadLine() == "да";

                this.db.Publishers.Add(new Publisher
                {
                    Title = title,
                    Coutry = country,
                    ExistInRussia = result
                });
                Console.WriteLine("Издатель был добавлен");
            });

            commands.Add(Commands.list_publisher, () =>
            {
                int i = 0;
                Console.WriteLine("Список издателей:");
                foreach (var p in db.Publishers)
                    Console.WriteLine($"{++i}) {p.Title}");
            });

            commands.Add(Commands.edit_publisher, () =>
            {
                int index = FindPublisher();
                if (index == -1)
                    return;

                Console.WriteLine("Укажите новое название издателя или пустую строку, чтобы оставить прежнее");
                string title = Console.ReadLine();
                Console.WriteLine("Укажите новую страну издателя или пустую строку, чтобы оставить прежнее");
                string country = Console.ReadLine();
                Console.WriteLine("Укажите появился ли издатель в России (да/нет) или пустую строку, чтобы оставить прежнее");
                string exist = Console.ReadLine();
                bool? result = null;
                if (!string.IsNullOrEmpty(exist))
                    result = exist == "да";

                if (!string.IsNullOrEmpty(title))
                    db.Publishers[index].Title = title;
                if (!string.IsNullOrEmpty(country))
                    db.Publishers[index].Coutry = country;
                if (result != null)
                    db.Publishers[index].ExistInRussia = result.Value;

                Console.WriteLine("Издатель был изменен");
            });

            commands.Add(Commands.remove_publisher, () =>
            {
                int index = FindPublisher();
                if (index == -1)
                    return;
                Publisher p = db.Publishers[index];
                db.Publishers.RemoveAt(index);
                var games = db.Games.Where(s => s.Publisher == p).ToArray();
                foreach (var game in games)
                    game.Publisher = db.NullPublisher;
                Console.WriteLine($"Издатель удален. Кол-во затронутых игр: {games.Length}");

            });
        }

        int FindPublisher()
        {
            commands[Commands.list_publisher]();
            Console.WriteLine("Укажите номер издателя");
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

    }
}
