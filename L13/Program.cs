using CarsLibrary;
using MyCollectionLibrary;
using System.Net.WebSockets;
using System.Runtime.InteropServices;

namespace L13
{
    internal class Program
    {
        static void GenerateCollectionsAndJournals(ref MyObservableCollection<Car> col1, ref MyObservableCollection<Car> col2, ref Journal jor1, ref Journal jor2)
        {
            Console.WriteLine("Введите желаемую длину коллекций:");
            int len = VHS.Input("Ошибка! Попробуйте ввести натуральное число!", 1);
            col1 = new MyObservableCollection<Car>("table1", len);
            col2 = new MyObservableCollection<Car>("table2", len);
            jor1 = new Journal();
            jor2 = new Journal();
            col1.CollectionCountChanged += jor1.WriteRecord;
            col1.CollectionReferenceChanged += jor1.WriteRecord;
            col1.CollectionReferenceChanged += jor2.WriteRecord;
            col2.CollectionReferenceChanged += jor2.WriteRecord;
            Console.WriteLine("\nКоллекции и журналы к ним успешно созданы!");
        }
        static void PrintBothCollections(MyObservableCollection<Car> col1, MyObservableCollection<Car> col2)
        {
            if (col1.Count > 0)
            {
                Console.WriteLine("Первая колллекция:");
                col1.Print();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Первая коллекция пуста!");
            }
            if (col2.Count > 0)
            {
                Console.WriteLine("Вторая колллекция:");
                col2.Print();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Вторая коллекция пуста!");
            }
        }
        static void PrintBothJournals(Journal jor1, Journal jor2)
        {
            if (jor1.Count > 0)
            {
                Console.WriteLine("Первый журнал:");
                jor1.Print();
                Console.WriteLine();
            }
            else
                Console.WriteLine("Первый журнал пуст!");
            if (jor2.Count > 0)
            {
                Console.WriteLine("Второй журнал:");
                jor2.Print();
            }
            else
                Console.WriteLine("Второй журнал пуст!");
            Console.WriteLine();
        }
        static void DeleteElementFromChosenCollection(ref MyObservableCollection<Car> col1, ref MyObservableCollection<Car> col2)
        {
            if (col1.Count == 0 || col2.Count == 0)
            {
                Console.WriteLine("Одна или две коллекции пустые! Сначала сгенерируйте их первой функцией!");
            }
            else
            {
                Console.WriteLine("Введите номер коллекции, из которой хотите удалить элемент:");
                int response = VHS.Input("Ошибка! Введите целое число от 1 до 2!", 1, 2);
                Console.WriteLine("Введите данные для элемента, который собираетесь удалить:");
                Car item = new Car();
                item.Init();
                if (response == 1)
                { 
                    if (col1.Remove(item)) 
                        Console.WriteLine("Элемент был успешно удалён из коллекции.");
                    else
                        Console.WriteLine("Элемент не найден в коллекции!");
                }
                else
                {
                    if (col2.Remove(item))
                        Console.WriteLine("\nЭлемент был успешно удалён из коллекции.");
                    else
                        Console.WriteLine("\nЭлемент не найден в коллекции!");
                }
            }
        }
        static void AddElementToChosenCollection(ref MyObservableCollection<Car> col1, ref MyObservableCollection<Car> col2)
        {
            Console.WriteLine("Введите номер коллекции, в которую хотите добавить элемент:");
            int response = VHS.Input("Ошибка! Введите целое число от 1 до 2!", 1, 2);
            Console.WriteLine("Введите данные для элемента, который собираетесь добавить:");
            Car item = new Car();
            item.Init();
            if (response == 1)
            {
                col1.Add(item);
            }
            else
            {
                col2.Add(item);
            }
            Console.WriteLine("\nЭлемент был успешно добавлен в коллекцию.");
        }
        static void ChangeElementInChosenCollection(ref MyObservableCollection<Car> col1, ref MyObservableCollection<Car> col2)
        {
            Console.WriteLine("Введите номер коллекции, в которой хотите заменить элемент:");
            int response = VHS.Input("Ошибка! Введите целое число от 1 до 2!", 1, 2);
            Console.WriteLine("Введите данные для элемента, который собираетесь изменить:");
            Car item = new Car();
            item.Init();
            Car newItem = new Car();
            newItem.RandomInit();
            if (response == 1)
            {
                col1[item] = newItem;
            }
            else
            {
                col2[item] = newItem;
            }
            Console.WriteLine("\nЭлемент был успешно заменён на рандомный.");
        }
        static void Main(string[] args)
        {
            string Menu = "\nВыберите действие с хеш-таблицей:\n" +
                         "1. Создать две коллекции и два журнала к ним.\n" +
                         "2. Распечатать коллекции.\n" +
                         "3. Распечатать журналы.\n" +
                         "4. Удалить элемент из выбранной коллекции.\n" +
                         "5. Добавить элемент к выбранной коллекции.\n" +
                         "6. Изменить элемент из выбранной коллекции.\n" +
                         "7. Выход.\n";
            MyObservableCollection<Car> table1 = new MyObservableCollection<Car>("table1", 0);
            MyObservableCollection<Car> table2 = new MyObservableCollection<Car>("table2", 0);
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();
            int response;
            do
            {
                Console.WriteLine(Menu);
                response = VHS.Input("Ошибка! Введите целое число от 1 до 7!", 1, 7);
                Console.WriteLine();
                try
                {
                    switch (response)
                    {
                        case 1:
                            GenerateCollectionsAndJournals(ref table1, ref table2, ref journal1, ref journal2);
                            break;
                        case 2:
                            PrintBothCollections(table1, table2);
                            break;
                        case 3:
                            PrintBothJournals(journal1, journal2);
                            break;
                        case 4:
                            DeleteElementFromChosenCollection(ref table1, ref table2);
                            break;
                        case 5:
                            AddElementToChosenCollection(ref table1, ref table2);
                            break;
                        case 6:
                            ChangeElementInChosenCollection(ref table1, ref table2);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (response != 7);
        }
    }
}
