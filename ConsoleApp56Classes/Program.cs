using Microsoft.VisualBasic;

namespace ConsoleApp56Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // классы в c#
            // класс это комлексный ссылочный тип данных,
            // который может иметь состояние и поведение
            // комплексность означает возможность хранения разнородных
            // значений, хранится комплекс связанных значений
            // например мы хотим хранить информацию о сотруднике
            // фио, телефон, дата рождения, пол, паспортные данные
            // проблема - как удобно хранить информацию о объекте?
            //string[] phones = new string[] { }; 
            //string[] firstName = new string[] { };

            // существует понятие класс - это шаблон для создания экземляров класса
            // существует понятие объект - синоним экземляра класса
            // экземпляр хранит индивидуальное состояние, т.е. значения всех полей и 
            // свойств в разных экземплярах различается

            // конструкторы
            // в классе может быть от 1 до множества конструкторов, это
            // элемент класса, который отвечает за инициализацию объекта
            // если мы не создаем свой конструктор для класса, он создается 
            // самостоятельно, это будет конструктор без аргументов и он пустой
            // конструктор создается с использованием имени класса и указанием от 0 до нескольких аргументов
            // типа возвращаемого значения у конструктора нет
            // можно создать разные конструкторы в одном классе, у них должны быть разные наборы аргументов
            // Первый созданный конструктор становится конструктором по умолчанию
            // т.е. при создании объекта мы должны указать все его аргументы
            // есть возможность вызывать из одного конструктора другой конструктор
            // с помощью ключевого слова base или this - после аргументов конструктора ставится : за ним base и набор аргументов
            // base позволяет вызвать конструктор из класса-родителя,
            // this позволяет вызвать конструктор из текущего класса
            // при условии, что в классе есть конструктор без аргументов (или нет конструкторов вообще)
            // мы можем назначить значения свойствам в классе через фигурные скобки
            // в инициализации объекта
            // конструктор вызывается только при инициализации и только через слово new

            // поля
            // полем называется переменная, объявленная внутри класса

            // свойства
            // свойство в c# это метод, который прикидывается полем, для управления другим полем
            // свойство позволяет контролировать чтение или запись определенного поля
            // может запрещать чтение или запись
            // может изменять назначаемое значение полю или изменять считываемое значение с поля
            // свойства применяются повсеместно в фреймворке
            // свойства нужны для контроля записи (да и чтения) в поля класса
            // свойства могут быть только для записи или только для чтения
            // или для записи и чтения одновременно
            // свойства имеют тип, имя (обычно с заглавной буквы) и модификаторы чтения/записи
            // свойства обычно публичные (тогда как поля обычно приватные - скрыты внутри класса)
            // работу блока модификатора в свойстве можно также прервать с помощью слова return
            //  { get; set; } - модификаторы доступа, краткая запись, подразумевающая создание поля, которое будет хранить значение и это поле доступно для чтения и для записи
            // get - аксессор - модификатор, указывающий возможность чтения
            // set - мутатор - модификатор, указывающий возможность записи

            // пример
            // создадим консоль, которая содержит команды для 
            // создания/редактирования/поиска/удаления инфы о 
            // библиотека игр - название, разработчик, издатель, жанр, рейтинг
            // платформа, дата выпуска
            // с точки зрения бд: есть объект игра (название, дата выпуска, рейтинг,
            // *разработчик, *издатель, **жанры, **платформы)
            DB dB = new DB();   
            CommandHelper commandHelper = new CommandHelper(dB);
            string message;
            while (true)
            {
                Console.WriteLine("Дайте команду");
                message = Console.ReadLine();

                if (message == "exit")
                    break;

                commandHelper.RunCommand(message);
            }
            dB.Save();
        }

        public static void Test()
        {
            int i = 10;
            Employer employer = new Employer("Алексей",
                "Анатольевич", "Пушкин", "890977782", DateTime.Parse("10.10.2010"));
            employer.LastName = "Пушкин"; // переназначение
            employer.Birthday = DateTime.Now;
            employer.Snils = "";
            employer.Snils = "1";
            Console.WriteLine(employer.Snils);

            employer.PrintAge();

            Employer employer1 = new Employer();
            employer1.LastName = "Пирогов";

            // назначение свойств, если есть конструктор без аргументов
            Employer employer2 = new Employer
            {
                Gender = true,
                FirstName = "Петр",
                LastName = "Кузнецов"
            };

            // вызов конструктора с 2 аргументами
            Employer employer3 = new Employer("", "");


            List<Employer> employers = new List<Employer>();
            employers.Add(employer);
            employers.Add(employer1);


            employers[0].FirstName = "";
        }
    }


    public class Employer
    {
        // поле
        int age;

        // свойства        
        public string FirstName { 
            get => firstName; 
            set => firstName = value; }
        public string LastName { get; set; }
        public string PatronimycName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public Passport Passport { get; set; }

        // полная запись свойства подразумевает создание поля
        // и подробное описание модификаторов с работой этого поля
        string snils; // поле
        private string firstName;

        public string Snils
        {
            get 
            {
                //Log.Attention("Кто-то узнал ваш снилс!");
                return snils;
            }
            //get => snils; // тоже самое, только в виде лямбды, выглядит проще
            set 
            {// назначение значения происходит с помощью слова value
                if (string.IsNullOrEmpty(value))
                    value = "Значение не назначено";
                if (value == "1")
                    return;
                snils = value;
            }
        }

        public Employer(string fName, string pName, string lName, string phone, DateTime birthday)
            : this(fName, pName, lName, phone)
        {
            Birthday = birthday;
            // неправильный пример вычисления возраста
            // не делайте так. это плохо
            age = DateTime.Now.Year - birthday.Year;
        }
        // конструктор это метод без возвращаемого типа, имя совпадает 
        // с классом
        // класс обычно отвечает за задание начальных значений в поля
        // и свойства внутри класса
        // может выполнять еще какую-то функцию, которая обязательно нужна
        // для работы объекта
        public Employer(string fName, string pName, string lName, string phone)
            : this(fName, pName, lName)
        {
            Phone = phone;
        }
        public Employer(string fName, string pName, string lName)
           : this(fName, pName)
        {
            LastName = lName;            
        }       
        public Employer(string fName, string pName)
            : this(fName)
        {
            PatronimycName = pName;
        }
        public Employer(string fName)
            : this()
        {
            FirstName = fName;
        }
        public Employer() 
        {
            Passport = new();
        }

        public void PrintAge()
        {
            Console.WriteLine($"Сотрудник {FirstName} прожил уже {age} лет. Поздравим его с этим достижением!");
        }
    }

    public class Passport
    {
        public int Serial { get; set; }
        public int Number { get; set; }
        public DateAndTime Date { get; set; }
    }
}
