internal class Lesson5
{
    public Lesson5()
    {
        
    }
    // здесь мы через конструктор получаем 
    // ссылку на другой объект лессон
    // сохраняем ссылку в поле - тогда
    // объект lesson перестает быть сиротой
    private Lesson5 lesson;
    public Lesson5(Lesson5 lesson)
    {
        lesson.OnHello2 += Test;
        this.lesson = lesson;
    }

    public void Test(object? sender, string test)
    {
        Console.WriteLine("Другой объект Lesson5 узнал о том, что первый объект Lesson5 вызвал метод Hello при помощи события Hello2 ");
    }

    string text = "мне очень жаль, я никому не нужен";
    // статичный конструктор
    // объявляется через слово static
    // не имеет аргументов
    // может быть только один
    // вызывается при первом обращении к классу
    // выполняется только один раз
    // позволяет назначить или настроить какие-то
    // первичные данные для класса
    // т.к. конструктор статичный, из него можно
    // вызвать только статичные элементы класса
    static Lesson5()
    {
        Console.WriteLine("hello from static lego");
    }

    public void Hello()
    {
        Console.WriteLine("hello from static method");
        // вызов события вызывает все подписанные методы на 
        // это событие
        OnHello?.Invoke(this, EventArgs.Empty);
        OnHello2?.Invoke(this, "что-нибудь");
        /*if (OnHello != null) то же самое, что с оператором ?.
            OnHello(this, EventArgs.Empty);*/
        }

    internal void FreeField()
    {
        // обнуление ссылки на объект
        // может сделать объект сиротой
        lesson = null;
    }

    // деструктор объявляется с помощью знака ~
    // аргументов нет
    static int index = 0;
  

    ~Lesson5()
    {
        Console.WriteLine("Выполнен деструктор класса. Еще один сирота уничтожен. Всего: " + ++index ); 
    }

    public event EventHandler OnHello;
    public event EventHandler<string> OnHello2;
}