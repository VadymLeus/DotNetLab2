using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; set; }
    public U Value1 { get; set; }
    public V Value2 { get; set; }
}

public class ExtendedDictionary<T, U, V>
{
    private List<ExtendedDictionaryElement<T, U, V>> elements = new List<ExtendedDictionaryElement<T, U, V>>();

    public void AddElement(T key, U value1, V value2)
    {
        elements.Add(new ExtendedDictionaryElement<T, U, V> { Key = key, Value1 = value1, Value2 = value2 });
    }

    public void RemoveElement(T key)
    {
        elements.RemoveAll(e => e.Key.Equals(key));
    }

    public bool ContainsKey(T key)
    {
        return elements.Any(e => e.Key.Equals(key));
    }

    public bool ContainsValue(U value1, V value2)
    {
        return elements.Any(e => e.Value1.Equals(value1) && e.Value2.Equals(value2));
    }

    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get { return elements.FirstOrDefault(e => e.Key.Equals(key)); }
    }

    public int Count
    {
        get { return elements.Count; }
    }

    public IEnumerable<ExtendedDictionaryElement<T, U, V>> GetElements()
    {
        return elements;
    }
}

public static class ConsoleHelper
{
    public static void WriteColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ExtendedDictionary<string, int, double> dictionary = new ExtendedDictionary<string, int, double>();

        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Додати елемент у словник");
            Console.WriteLine("2. Видалити елемент зі словника за ключем");
            Console.WriteLine("3. Перевірити наявність елемента за ключем");
            Console.WriteLine("4. Перевірити наявність елемента за значеннями Value1 та Value2");
            Console.WriteLine("5. Повернути елемент за ключем");
            Console.WriteLine("6. Показати кількість елементів у словнику");
            Console.WriteLine("7. Вийти");

            Console.Write("Виберіть опцію (1-7): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть ключ: ");
                    string key = Console.ReadLine();
                    int value1;
                    double value2;

                    Console.Write("Введіть перше значення: ");
                    if (int.TryParse(Console.ReadLine(), out value1))
                    {
                        Console.Write("Введіть друге значення: ");
                        if (double.TryParse(Console.ReadLine(), out value2))
                        {
                            dictionary.AddElement(key, value1, value2);
                            ConsoleHelper.WriteColor("Елемент доданий до словника.", ConsoleColor.Green);
                        }
                        else
                        {
                            ConsoleHelper.WriteColor("Невірний формат другого значення.", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteColor("Невірний формат першого значення.", ConsoleColor.Red);
                    }
                    break;

                case "2":
                    Console.Write("Введіть ключ для видалення: ");
                    string keyToRemove = Console.ReadLine();
                    if (dictionary.ContainsKey(keyToRemove))
                    {
                        dictionary.RemoveElement(keyToRemove);
                        ConsoleHelper.WriteColor("Елемент видалений зі словника.", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleHelper.WriteColor("Елемент з таким ключем не знайдений.", ConsoleColor.Red);
                    }
                    break;

                case "3":
                    Console.Write("Введіть ключ для перевірки наявності: ");
                    string keyToCheck = Console.ReadLine();
                    if (dictionary.ContainsKey(keyToCheck))
                    {
                        ConsoleHelper.WriteColor("Елемент із вказаним ключем існує в словнику.", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleHelper.WriteColor("Елемент із вказаним ключем не знайдений.", ConsoleColor.Red);
                    }
                    break;

                case "4":
                    Console.Write("Введіть перше значення для перевірки: ");
                    if (int.TryParse(Console.ReadLine(), out int value1ToCheck))
                    {
                        Console.Write("Введіть друге значення для перевірки: ");
                        if (double.TryParse(Console.ReadLine(), out double value2ToCheck))
                        {
                            if (dictionary.ContainsValue(value1ToCheck, value2ToCheck))
                            {
                                ConsoleHelper.WriteColor("Елемент із вказаними значеннями Value1 та Value2 існує в словнику.", ConsoleColor.Green);
                            }
                            else
                            {
                                ConsoleHelper.WriteColor("Елемент із вказаними значеннями Value1 та Value2 не знайдений.", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            ConsoleHelper.WriteColor("Невірний формат другого значення.", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteColor("Невірний формат першого значення.", ConsoleColor.Red);
                    }
                    break;

                case "5":
                    Console.Write("Введіть ключ для пошуку елемента: ");
                    string keyToFind = Console.ReadLine();
                    var foundElement = dictionary[keyToFind];
                    if (foundElement != null)
                    {
                        ConsoleHelper.WriteColor($"Знайдений елемент: Ключ: {foundElement.Key}, Значення1: {foundElement.Value1}, Значення2: {foundElement.Value2}", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleHelper.WriteColor("Елемент з таким ключем не знайдений.", ConsoleColor.Red);
                    }
                    break;

                case "6":
                    ConsoleHelper.WriteColor("Кількість елементів у словнику: " + dictionary.Count, ConsoleColor.Green);
                    break;

                case "7":
                    ConsoleHelper.WriteColor("Дякуємо за використання програми.", ConsoleColor.Green);
                    return;

                default:
                    ConsoleHelper.WriteColor("Невірний вибір. Будь ласка, виберіть опцію з меню (1-7).", ConsoleColor.Red);
                    break;
            }
        }
    }
}
