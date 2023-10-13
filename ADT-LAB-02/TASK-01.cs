using System;
using System.Linq;
using System.Text;
namespace TASK_01
{
    public static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static int CountOccurrences(this string input, char character)
        {
            return input.Count(c => c == character);
        }
    }
    public static class ArrayExtensions
    {
        public static int CountOccurrences<T>(this T[] array, T value)
        {
            return array.Count(item => item.Equals(value));
        }
        public static T[] GetDistinctValues<T>(this T[] array)
        {
            return array.Distinct().ToArray();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Console.Write("Введіть рядок: ");
            string text = Console.ReadLine();
            Console.WriteLine("Оригінальний рядок: " + text);
            Console.WriteLine("Обернутий рядок: " + text.Reverse());

            Console.Write("Введіть символ для підрахунку кількості входжень: ");
            char searchChar;
            if (char.TryParse(Console.ReadLine(), out searchChar))
            {
                int charCount = text.CountOccurrences(searchChar);
                Console.Write($"\nКількість входжень '{searchChar}': {charCount}");
            }
            else
            {
                Console.WriteLine("Некоректний ввід символу.");
            }
            Console.Write("\nВведіть елементи для масиву (через кому та пробіл): ");
            int[] numbers;
            try
            {
                numbers = Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }
            catch (FormatException)
            {
                Console.WriteLine("Некоректний ввід чисел для масиву.");
                return;
            }
            Console.WriteLine("Оригінальний масив: " + string.Join(", ", numbers));

            Console.Write("Введіть число для підрахунку кількості входжень у масив: ");
            int searchValue;
            if (int.TryParse(Console.ReadLine(), out searchValue))
            {
                int valueCount = numbers.CountOccurrences(searchValue);
                Console.WriteLine($"Кількість входжень {searchValue}: {valueCount}");
            }
            else
            {
                Console.WriteLine("Некоректний ввід числа.");
            }

            int[] distinctNumbers = numbers.GetDistinctValues();
            Console.WriteLine("Унікальні значення: " + string.Join(", ", distinctNumbers));
            Console.ReadLine();
        }
    }
}
