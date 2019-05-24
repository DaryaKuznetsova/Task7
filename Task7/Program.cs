using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine()); 
            int[,] arr = Gray(n);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(arr[i, j] + " ");
                Console.WriteLine();
            }
        }

        static int [,] BuildCode(int n)
        {
            int[,] GrayCode = new int[(int)Math.Pow(2, n), n];
            GrayCode[0, n-1] = 0;
            GrayCode[1, n-1] = 1;
            int p = 2;
            int t = 0;
            for(int i=1; i<n;i++)
            {
                t = p;
                p = p * 2;
                for(int k=p/2;k<p;k++)
                {
                    GrayCode[k, i] = GrayCode[t, i];       // Отражение имеющихся кодов 
                    GrayCode[t, n + 1 - i] = 0;
                    GrayCode[k, n + 1 - i] = 1;    // Добавление 0 и 1 в начало 
                    t--;
                }
            }
            return GrayCode;
        }

        static int[,] Gray(int n)
        {
            int[,] arr = new int[(int)Math.Pow(2,n), n];
            arr[0, n - 1] = 0;
            arr[1, n - 1] = 1;
            int tec = 2;
            for(int i=1;i<n;i++)
            {
                tec *= 2;
                for(int j=n-1;j>0;j--)
                {
                    arr = Part(arr, n, tec, j);
                }

            }
            return arr;
        }

        static int[,] Reverse(int[,] arr, int p, int j)
        {
            int curr = p - 1;
            //int del = curr;
            for (int i = 0; i < p / 2; i++)
            {
                arr[curr, j] = arr[i, j];
                curr--;
                //del-=2;
            }
            return arr;
        }

        static int[,] Add(int [,]arr, int p, int j)
        {
            for (int i = 0; i < p / 2; i++)
                arr[i, j] = 0;
            for (int i = p / 2; i < p; i++)
                arr[i, j] = 1;
            return arr;
        }

        static int [,] Part(int [,]arr, int n, int p, int j)
        {
            int temp = p;
            int tj = n-1;
            for (int i = 0; i < n-j; i++)
            {
                arr = Reverse(arr, p, tj);
                p *= 2;
                tj--;
            }
            arr = Add(arr, temp, tj);
            return arr;
        }
    }
}
