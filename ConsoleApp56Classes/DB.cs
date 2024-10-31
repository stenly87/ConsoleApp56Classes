using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp56Classes
{
    public class DB
    {
        public List<Genre> Genres = new List<Genre>();
        public List<Platform> Platforms = new List<Platform>();
        public List<Publisher> Publishers = new List<Publisher>();
        public List<Developer> Developers = new List<Developer>();
        public List<Game> Games = new List<Game>();

        string filename = "db.bin";
        public DB()
        {
            if (!File.Exists(filename))
                return;
            int count;
            using (var fs = File.OpenRead(filename))
            using (var br = new BinaryReader(fs))
            {
                if (fs.Length == 0)
                    return;
                // жанры
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                    Genres.Add(new Genre { Title = br.ReadString() });
                // платформы
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                    Platforms.Add(new Platform { Title = br.ReadString() });
                // паблишеры
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                    Publishers.Add(
                        new Publisher
                        {
                            Title = br.ReadString(),
                            Coutry = br.ReadString(),
                            ExistInRussia = br.ReadBoolean()
                        });
                // разрабы
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                    Developers.Add(
                        new Developer
                        {
                            Title = br.ReadString(),
                            Coutry = br.ReadString(),
                            TeamCount = br.ReadInt32()
                        });
                // игры
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    var game = new Game
                    {
                        Title = br.ReadString(),
                        Rating = (Rating)br.ReadInt32(),
                        Release = DateTime.FromBinary(br.ReadInt64())
                    };
                    string d = br.ReadString();
                    game.Developer = Developers.FirstOrDefault(s => s.Title == d);
                    string p = br.ReadString();
                    game.Publisher = Publishers.FirstOrDefault(s => s.Title == p);
                    int count2 = br.ReadInt32();
                    game.Genres = new();
                    for (int j = 0; j < count2; j++)
                    { 
                        string g = br.ReadString();
                        game.Genres.Add(Genres.FirstOrDefault(s => s.Title == g));  
                    }

                    count2 = br.ReadInt32();
                    game.Platforms = new();
                    for (int j = 0; j < count2; j++)
                    {
                        string pl = br.ReadString();
                        game.Platforms.Add(Platforms.FirstOrDefault(s => s.Title == pl));
                    }
                    Games.Add(game);
                }
            }
        }

        public void Save()
        {
            using (var fs = File.Create(filename))
            using (var bw = new BinaryWriter(fs))
            {
                // жанры
                bw.Write(Genres.Count);
                foreach (var genre in Genres)
                    bw.Write(genre.Title);
                // платформы
                bw.Write(Platforms.Count);
                foreach (var p in Platforms)
                    bw.Write(p.Title);
                // паблишеры
                bw.Write(Publishers.Count);
                foreach (var p in Publishers)
                {
                    bw.Write(p.Title);
                    bw.Write(p.Coutry);
                    bw.Write(p.ExistInRussia);
                }
                // разрабы
                bw.Write(Developers.Count);
                foreach (var p in Developers)
                {
                    bw.Write(p.Title);
                    bw.Write(p.Coutry);
                    bw.Write(p.TeamCount);
                }
                // игры
                bw.Write(Games.Count);
                foreach (var game in Games)
                {
                    bw.Write(game.Title);
                    bw.Write((int)game.Rating);
                    bw.Write(game.Release.ToBinary());
                    // разработчик
                    bw.Write(game.Developer.Title);
                    // издатель
                    bw.Write(game.Publisher.Title);
                    // жанры
                    bw.Write(game.Genres.Count);
                    foreach (var genre in game.Genres)
                        bw.Write(genre.Title);
                    // платформы
                    bw.Write(game.Platforms.Count);
                    foreach (var p in game.Platforms)
                        bw.Write(p.Title);
                }
            }
        }
    }
}
