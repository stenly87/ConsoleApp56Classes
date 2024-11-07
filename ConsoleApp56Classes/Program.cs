﻿using ConsoleApp56Classes;
using System.Text.Json;
using Test;

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

            Employer employer = new Employer("Алексей",
                "Анатольевич", "Пушкин", "телефон", DateTime.Now);
            employer.Passport = new Passport { Date = DateTime.Now, Number = 123, Serial = 547 };

            string text = JsonSerializer.Serialize(employer);
            Console.WriteLine(text);

            using (var fs = File.Create("test.json"))
                // иерархия объектов сохраняется в файл
                JsonSerializer.Serialize(fs, employer);
            Employer second;
            using (var fs = File.OpenRead("test.json"))
                // иерархия объектов воссоздается из файла
                second = JsonSerializer.Deserialize<Employer>(fs);

            Console.WriteLine(second.FirstName);

                //RunLesson6();

                return;


            RunLesson5();
            Console.ReadLine();
            FreeLesson();
            Console.ReadLine();
            return;


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
        static Lesson5 anotherObj;
        public static void RunLesson5()
        {
            // содержимое классов которое уже задели:
            // поля (переменные в классе)
            // свойства (управление полями)
            // методы 
            // конструкторы

            // существует еще один вариант конструктора
            // статичный конструктор (см Lesson5)

            // сначал выполнится статичный конструктор
            // следом выполнится метод
            //Lesson5.Hello();

            // деструктор
            // может быть объявлен в классе (см Lesson5
            // выполняется в момент, когда экземпляр
            // класса уничтожается сборщиком мусора
            // т.к. нельзя точно предсказать, когда это
            // произойдет, пользоваться нужно только
            // в случаях, когда больше нечем пользоваться
            // в приложениях на шарпе есть сборщик мусора
            // Garbage Collector (GC), он автоматически
            // сканирует память занимаемую приложением
            // и отслеживает объекты-сироты, если таковые 
            // находятся - объекты-сироты уничтожаются

            //List<Lesson5> list = new List<Lesson5>();
            /*for (int i = 0; i < 100000000; i++)
                new Lesson5();

            Console.ReadLine();*/

            // события 
            // события в c# представлены в виде
            // элемента event
            // является реализацией паттерна - издатель-подписчик
            // событие можно вызвать только изнутри класса
            // это главное отличие события от делегата

            Lesson5 lesson5 = new Lesson5();
            lesson5.OnHello += (o, e) => {
                Console.WriteLine("объект " + o + " вызвал метод Hello");
            };
            anotherObj = new Lesson5(lesson5);
           

            /*(s, arg) => {
                Console.WriteLine("объект " + s + " вызвал метод Hello и шлет вам "+ arg);
            };*/
            lesson5.Hello();

        }

        public static void FreeLesson()
        {
            anotherObj.FreeField();
            GC.Collect() ;
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

        public static void RunLesson6()
        { 
            // индексаторы
            Lesson6 lesson6 = new Lesson6();
            lesson6[0] = "замена строки";
            for(int i = 0 ; i < 10; i++)
                Console.WriteLine(lesson6[i]);

            lesson6.strings = new string[] { };

            // проверка internal
            ClassLibrary1.Class1 class1 = new ClassLibrary1.Class1();
            //class1.Test = "недоступен";
            class1.Test2 = "str";
            Lesson5 lesson5 = new Lesson5();
            TestClass testClass = TestClass.CreateInstance();
            testClass.Test1(lesson5);
        }
    }

    public class Lesson6
    {
        internal string[] strings;
        public Lesson6()
        {
            strings = ["первая строка",
                "вторая строка"];
        }
        // синтаксис индексатора включает
        // тип возвращаемого значения
        // ключевое слово this после которого
        // в квадратных скобках идут аргументы
        public string this[int index]
        {
            // чтение значения по индексу
            get
            {// проверки на отрицательный индекс
                if (strings.Length <= index)
                    return "нет значения";
                return strings[index];
            }
            // запись значения по индексу
            set
            {// проверки на отрицательный индекс
             // проверки на существование индекса
                strings[index] = value;
            }
        }
        // другой индексатор
        // доступ только для чтения
        // должно быть отличие в кол-ве или типе аргументов
        public int this[string i]
        { // без смысла
            get { return i.Length; }
        }


        // модификаторы доступа
        // контролируют область, откуда есть доступ
        // к элементу, к которому применяется модификатор
        // public - доступ есть отовсюду, внутри класса, извне класса, из другого пространства имен и даже из другой сборки
        // private - самый закрытый модификатор, доступ есть только внутри класса
        // protected - доступ внутри класса и внутри классов-наследников
        // internal - доступ внутри класса, внутри наследников, снаружи класса, но в рамках одной сборки
        // private protected 
        // protected internal  - доступ внутри класса и его наследников, но в рамках сборки
        // в с# 11 появился еще модификатор file - доступ в рамках одного файла   
        // если объект класса передается в некоторый 
        // метод (конструктор, индексатор, событие и тп)
        // то модификатор видимости объекта должен быть
        // не ниже, чем модификатор видимости метода

        // static - ключевое слово, которое помечает
        // класс или элемента как статичный, это означает
        // что для использования элемента не нужно создавать
        // экземпляр класса
        // static элементы инициализируются в единственном экземпляре
        // static элементы не имеют доступа к нестатичным
        // элементам класса, в другую сторону доступ есть
        // если класс помечен как статичный, то он может содержать только статичные элементы
        // экземпляр статичного класса создать нельзя
        //  Math math = new Math(); не выйдет

        
    }
}

namespace Test
{
    public class TestClass
    {
        private TestClass()
        {
            // закрытый конструктор можно вызывать
            // только внутри класса
            // полезно, если мы не хотим, чтобы 
            // все подряд создавали экземпляры нашего класса
            // или мы хотим контролировать кол-во экземпляров 
            // класса
        }
        static TestClass instance;
        // статичный метод
        // мы можем вызвать его через имя класса TestClass
        public static TestClass CreateInstance()
        {
            // пример простого синглтона (паттерн - единственный экземпляр)
           if (instance == null)
                instance = new TestClass();
           return instance; 
            // такой вызов невозможен, поскольку в данном случае
            // Test1 не относится к конкретному экземпляру
            //Test1(new Lesson5());
            return new TestClass();
        }

        internal void Test1(Lesson5 lesson5)
        {
            var test = CreateInstance();
            Lesson6 lesson6 = new Lesson6();
            lesson6.strings = null;
        }
    }

}