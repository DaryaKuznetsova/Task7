using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task7
{
    class Point: IComparable<Point>
    {
        int name;
        string data;
        int info;
        
        public Point(int n, string d)
        {
            name = n;
            data = d;
            info = Convert.ToInt32(d);
        }

        public override string ToString()
        {
            return $"{name,4} {data}";
        }

        public int CompareTo(Point other)
        {
            if (info > other.info ) return 1;
            else
            if (info == other.info) return 0;
            else 
            return -1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("output.txt");
            Console.Write("Длина кодовых слов: ");
            int n = ReadAnswer(); 
            int[,] arr = Gray(n);
            Print(arr);
            RewriteGray(arr);
            foreach (Point s in list) sw.WriteLine(s);
            Console.WriteLine("Кодовые слова в лексикографическом порядке находятся в файле output.txt");
            sw.Close();
        }

        static List<Point> list = new List<Point>();

        static void RewriteGray(int[,] arr)
        {
            for(int i=0; i<arr.GetLength(0); i++)
            {
                string s = "";
                for(int j=0; j<arr.GetLength(1); j++)
                {
                    s += arr[i, j];
                }
                Point p = new Point(i + 1, s);
                list.Add(p);
            }
            list.Sort();
        }

        static int[,] Gray(int n)
        {
            int[,] arr = new int[(int)Math.Pow(2,n), n]; // создаем массив
            arr[0, n - 1] = 0; // инициализируем последние элементы
            arr[1, n - 1] = 1;
            int tek = 4;
                for(int j=n-1;j>0;j--) // начинаем обрабатывать каждый столбец
                {                      // не доходим до 0, потому что потом счетчик уменьшится
                    arr = Part(arr, n, j, ref tek);
                }
            return arr;
        }

        static int[,] Reverse(int[,] arr, int size, int column) // отражаем столбцы
        {
            int curr = size - 1;
            for (int i = 0; i < size / 2; i++)
            {
                arr[curr, column] = arr[i, column];
                curr--;
            }
            return arr;
        }

        static int[,] Add(int [,]arr, int size, int column) // заполняем столбцы
        {
            for (int i = 0; i < size / 2; i++)
                arr[i, column] = 0;
            for (int i = size / 2; i < size; i++)
                arr[i, column] = 1;
            return arr;
        }

        static int [,] Part(int [,]arr, int n, int j, ref int tek) 
        {
            int temp = tek;
            int tj = n-1;
            for (int i = 0; i < n-j; i++)           // переворачиваем n-j столбцов за один вызов метода
            {                                       
                arr = Reverse(arr, tek, tj);   
                tj--;                               // отсчитываем в какой столбец будут добавлены элементы за эту итерацию
            }
            tek *= 2;
            arr = Add(arr, temp, tj);               // заполняем следующий столбец
            return arr;
        }

        static void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(arr[i, j]);
                Console.WriteLine();
            }
        }

        public static int ReadAnswer()
        {
            int a = 0;
            bool ok = false;
            do
            {
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a > 0&&a<=24)
                        ok = true;
                    else Console.WriteLine("Длина кода - положительное число меньше 25");
                }
                catch (Exception)
                {
                    Console.WriteLine("Пожалуйста, введите целое число.");
                    ok = false;
                }
            } while (!ok);
            return a;
        }
    }
}
