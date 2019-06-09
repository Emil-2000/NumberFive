using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberFive
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            Console.WriteLine("Сгенерированная матрица:");
            int[,] testMatrix = MatrixGenerator(10, 2, 2, ran);
            Print(testMatrix);
            int[] answer = getVectorByMatrix(testMatrix);
            Console.WriteLine("Результат анализа:");
            Print(answer);
            Console.ReadLine();
        }
        /// <summary>
        /// Печать одномерного массива
        /// </summary>
        /// <param name="N"></param>
        static void Print(int[] N)
        {
            for (int i = 0; i < N.Length; i++)
                Console.Write(N[i] + " ");
            Console.WriteLine();
        }
        /// <summary>
        /// Печать двумерного массива
        /// </summary>
        /// <param name="N"></param>
        static void Print(int[,] N)
        {
            for (int i = 0; i < N.GetLength(0); i++)
            {
                for (int j = 0; j < N.GetLength(1); j++)
                {
                    Console.Write(N[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Возвращает вектор по заданной матрице в котором:
        /// - 0, если строка матрицы не упорядочена, а
        /// - 1, если строка матрицы упорядочена по возрастанию или убыванию 
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        static int[] getVectorByMatrix(int[,] Matrix)
        {
            int[] retValue = new int[Matrix.GetLength(0)];
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                // создадим массив строки
                int[] stroka = new int[Matrix.GetLength(1)];
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    stroka[j] = Matrix[i, j];
                }
                if (isSorted(stroka))
                    retValue[i] = 1;
                else
                    retValue[i] = 0;
            }
            return retValue;
        }
        static bool isSorted(int[] a)
        {
            // создаем клон массива
            int[] testAscending = (int[])a.Clone();
            int[] testDescending = (int[])a.Clone();
            testAscending = BubbleSort(testAscending, true);
            testDescending = BubbleSort(testDescending, false);
            if (CheckArray(a, testAscending))
                return true;
            if (CheckArray(a, testDescending))
                return true;
            return false;
        }
        /// <summary>
        /// Проверка на одинаковость элементов массива
        /// </summary>
        /// <param name="Source">Исходный массив</param>
        /// <param name="Target">Конечный массив</param>
        /// <returns></returns>
        static bool CheckArray(int[] Source, int[] Target)
        {
            // проверим на одинаоковость размеров массива
            if (Source.Length != Target.Length)
                return false;
            // теперь проверяем
            for (int i = 0; i < Source.Length; i++)
            {
                if (Source[i] != Target[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Сортировка пузырьком массива int
        /// </summary>
        /// <param name="mas">Сам массив</param>
        /// <param name="Ascending">По возрастанию/убыванию</param>
        /// <returns></returns>
        static int[] BubbleSort(int[] mas, bool Ascending = true)
        {
            int temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (Ascending)
                    {
                        // если по возрастанию
                        if (mas[i] > mas[j])
                        {
                            temp = mas[i];
                            mas[i] = mas[j];
                            mas[j] = temp;
                        }
                    }
                    else
                    {
                        // если по убыванию
                        if (mas[i] < mas[j])
                        {
                            temp = mas[i];
                            mas[i] = mas[j];
                            mas[j] = temp;
                        }
                    }
                }
            }
            return mas;
        }

        /// <summary>
        /// Генерирует квадратную матрицу заданного размера случайным образом
        /// </summary>
        /// <param name="Size">Размер матрицы</param>
        /// <param name="AscendingCount">количество строк, котрые должны быть упорядочены по возрастанию</param>
        /// <param name="DescendingCount">количество строк, которые должны быть упорядочены по убыванию</param>
        /// <param name="ran">Рандомный генератор</param>
        /// <returns></returns>
        static int[,] MatrixGenerator(int Size, int AscendingCount, int DescendingCount, Random ran)
        {
            // возвращаемый массив
            int[,] retValue = new int[Size, Size];
            // проверяем количество строк, которые должны быть отсортированы
            if ((AscendingCount + DescendingCount) >= Size)
                throw new System.ArgumentException("Количество упорядоченных строк матрицы не должно быть больше количества строк матрицы", "Ошибка");
            // номера строк, где должна быть сортировка по возрастанию
            List<int> NumbersAscending = new List<int>();
            //Random ran = new Random();
            for (int i = 0; i < AscendingCount; i++)
            {
                // генерируем неповторяющийся список случайных номеров строк которые должны быть упорядочены по возрастанию
                do
                {
                    int s = ran.Next(0, Size);
                    if (!NumbersAscending.Contains(s))
                    {
                        NumbersAscending.Add(s);
                        break;
                    }
                } while (true);
            }
            // номера строк, где должна быть сортировка по убыванию
            List<int> NumberDescending = new List<int>();
            for (int i = 0; i < DescendingCount; i++)
            {
                // генерируем неповторяющийся список случайных номеров строк которые должны быть упорядочены по убыванию и не повторяться с первым списком
                do
                {
                    int s = ran.Next(0, Size);
                    if (!NumbersAscending.Contains(s) && !NumbersAscending.Contains(s))
                    {
                        NumberDescending.Add(s);
                        break;
                    }
                } while (true);
            }
            // заполняем строки матрицы
            for (int i = 0; i < Size; i++)
            {
                int[] a = null;
                if (NumbersAscending.Contains(i))
                {
                    // заполняем строку возрастающим массивом
                    a = getSortedVector(Size);
                }
                if (NumberDescending.Contains(i))
                {
                    // заполняем строку убывающим массивом
                    a = getSortedVector(Size, false);
                }
                if (a == null)
                {
                    //заполняем строку рандомными значениями
                    a = new int[Size];
                    for (int j = 0; j < Size; j++)
                    {
                        a[j] = ran.Next(0, Size);
                    }
                }
                // заполняем строку возвращаемого массива
                for (int j = 0; j < Size; j++)
                {
                    retValue[i, j] = a[j];
                }
            }
            return retValue;
        }
        /// <summary>
        /// Возвращает одномерный отсортированный массив
        /// </summary>
        /// <param name="Size">размер</param>
        /// <param name="Ascending">по возрастанию либо убыванию</param>
        /// <returns></returns>
        static int[] getSortedVector(int Size, bool Ascending = true)
        {
            int[] retVal = new int[Size];
            for (int i = 0; i < Size; i++)
            {
                if (Ascending)
                    retVal[i] = i;
                else
                    retVal[i] = Size - 1 - i;
            }
            return retVal;
        }
    }
}
