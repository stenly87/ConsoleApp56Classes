namespace ConsoleApp56Classes
{
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
}
