namespace ConsoleApp56Classes
{
    public class Game
    { 
        public string Title             { get; set; }
        public DateTime Release         { get; set; }
        public Rating Rating            { get; set; }
        public Developer Developer      { get; set; }
        public Publisher Publisher      { get; set; }
                                        
        public List<Genre> Genres       { get; set; }
        public List<Platform> Platforms { get; set; }
    }
}
