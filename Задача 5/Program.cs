using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Задача_5
{
    class Program
    {
        const int N = 7;

        public static void Place(ref int x, ref int y, ref int dir)
        {
            switch (dir % 4)
            {
                case 0: // движение вправо
                    y++;
                    if (y == N - 1 - x) dir++;
                    break;
                case 1: // движение вниз
                    x++;
                    if (x == y) dir++;
                    break;
                case 2: // движение влево
                    y--;
                    if (y == N - 1 - x) dir++;
                    break;
                case 3: // движение вверх
                    x--;
                    if (x - 1 == y) dir++;
                    break;
            }
        }
        static void Main(string[] args)
        {
            string input_f = "input.txt";

            double[,] arr = new double[N, N];
            double[] ans = new double[(int)Math.Pow(N, 2)];

            bool running = true;
            while (running)
            {
                Console.WriteLine("Выберите способ ввода: ");
                Console.WriteLine("1 - ввод матрицы из файла");
                Console.WriteLine("2 - ручной ввод");

                switch (Console.ReadLine())
                {
                    case "1": // ввод из файла
                        using (FileStream sf = new FileStream(input_f, FileMode.OpenOrCreate)) { }
                        using (StreamReader reader = new StreamReader(input_f))
                        {
                            try
                            {
                                for (int i = 0; i < arr.GetLength(0); i++)
                                {
                                    string str = reader.ReadLine();
                                    str = Regex.Replace(str.Trim(), @"\s+", " ");
                                    if (str != "")
                                    {
                                        string[] chisla = str.Split(new char[] { ' ' });
                                        for (int j = 0; j < arr.GetLength(1); j++) arr[i, j] = Convert.ToDouble(chisla[j]);
                                    }
                                    else i--;
                                }
                                running = false;
                            }
                            catch { Console.WriteLine($"Ошибка чтения из файла. Проверьте файл для ввода: в нем должна быть матрица {N}х{N} из действительных чисел"); }
                        }
                        break;

                    case "2": // ручной ввод
                        for (int i = 0; i < arr.GetLength(0); i++)
                        {
                            for (int j = 0; j < arr.GetLength(1); j++)
                                while (true)
                                {
                                    Console.WriteLine("Введите число в {0} строке и {1} столбце", i + 1, j + 1);
                                    try
                                    {
                                        arr[i, j] = Convert.ToDouble(Console.ReadLine());
                                        break;
                                    }
                                    catch { Console.WriteLine("Ошибка ввода"); }
                                }
                        }
                        running = false;
                        break;
                }
            }

            Console.WriteLine("Полученная матрица: ");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + "\t");
                }
                Console.WriteLine();
            }

            int x = 0, y = 0, direction = 0;

            for (int i = 0; i < ans.Length; i++) // создание массива раскрученной спирали
            {
                ans[i] = arr[x, y];
                Place(ref x, ref y, ref direction);
                Console.WriteLine($"{i + 1} элемент спирали: {ans[i]}");
            }
            Console.ReadKey();
        }
    }
}
