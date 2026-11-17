using System.Collections;

namespace KT39
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Кольцевой буфер";

            Console.WriteLine("Кольцевой буфер");

            int capacity;

            while (true)
            {
                Console.Write("Введите ёмкость буфера: ");

                if (int.TryParse(Console.ReadLine(), out capacity) && capacity > 0)
                    break;

                Console.WriteLine("Ошибка! Введите положительное целое число.");
            }

            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);

            Console.WriteLine("Буфер успешно создан.");

            while (true)
            {
                Console.WriteLine("\nМеню");
                Console.WriteLine($"Ёмкость: {buffer.Capacity}");
                Console.WriteLine($"Элементов: {buffer.Count}/{buffer.Capacity}");
                Console.WriteLine("1. Добавить число");
                Console.WriteLine("2. Показать содержимое буфера");
                Console.WriteLine("3. Показать буфер в обратном порядке");
                Console.WriteLine("4. Информация о буфере");
                Console.WriteLine("5. Добавить несколько чисел");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        {
                            Console.Write("Введите число: ");

                            if (int.TryParse(Console.ReadLine(), out int number))
                            {
                                AddNumber(buffer, number);
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Нужно ввести целое число.");
                            }

                            break;
                        }

                    case "2":
                        {
                            Console.Write("Содержимое буфера: ");

                            if (buffer.Count == 0)
                            {
                                Console.WriteLine("Буфер пуст.");
                            }
                            else
                            {
                                Console.Write("[ ");

                                foreach (int item in buffer)
                                {
                                    Console.Write(item + " ");
                                }

                                Console.WriteLine("]");
                            }

                            break;
                        }

                    case "3":
                        {
                            Console.Write("Обратный порядок: ");

                            if (buffer.Count == 0)
                            {
                                Console.WriteLine("Буфер пуст.");
                            }
                            else
                            {
                                Console.Write("[ ");

                                foreach (int item in buffer.Reverse())
                                {
                                    Console.Write(item + " ");
                                }

                                Console.WriteLine("]");
                            }

                            break;
                        }

                    case "4":
                        {
                            Console.WriteLine("Информация о буфере:");
                            Console.WriteLine($"Максимальная ёмкость: {buffer.Capacity}");
                            Console.WriteLine($"Количество элементов: {buffer.Count}");
                            Console.WriteLine($"Свободных мест: {buffer.Capacity - buffer.Count}");

                            Console.WriteLine(buffer.Count == buffer.Capacity
                                ? "Состояние: буфер заполнен."
                                : "Состояние: есть свободное место.");

                            break;
                        }

                    case "5":
                        {
                            Console.Write("Сколько чисел добавить? ");

                            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                            {
                                for (int i = 0; i < amount; i++)
                                {
                                    Console.Write($"Введите число №{i + 1}: ");

                                    if (int.TryParse(Console.ReadLine(), out int value))
                                    {
                                        AddNumber(buffer, value);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Некорректное число. Пропускаем.");
                                    }
                                }

                                Console.WriteLine("Добавление завершено.");
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Введите положительное количество.");
                            }

                            break;
                        }

                    case "0":
                        {
                            Console.WriteLine("Программа завершена.");
                            return;
                        }

                    default:
                        {
                            Console.WriteLine("Такого пункта меню нет.");
                            break;
                        }
                }

                Console.WriteLine("Действие завершено.");
            }
        }

        static void AddNumber(CircularBuffer<int> buffer, int number)
        {
            if (buffer.Count == buffer.Capacity)
            {
                int oldest = 0;

                foreach (int item in buffer)
                {
                    oldest = item;
                    break;
                }

                buffer.Add(number);

                Console.WriteLine($"Буфер заполнен. Элемент {oldest} заменён на {number}.");
            }
            else
            {
                buffer.Add(number);
                Console.WriteLine($"Число {number} добавлено.");
            }
        }
    }
}