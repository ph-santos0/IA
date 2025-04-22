using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_IA___Fibonacci_2
{
    class Program
    {
        static double[] v = new double[1000];

        static double fib(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            if (v[n] == -1)
            {
                v[n] = fib(n - 1) + fib(n - 2);
            }
            return v[n];
        }

        static void Main()
        {
            for (int i = 0; i < 1000; i++)
            {
                v[i] = -1;
            }

            Console.Write("Quantos termos da sequência de Fibonacci você quer ver (máx 1000)? ");
            int termos = Convert.ToInt32(Console.ReadLine());

            if (termos < 1 || termos > 1000)
            {
                Console.WriteLine("Entrada inválida. Por favor, digite um número entre 1 e 1000.");
                return;
            }

            Console.WriteLine("Sequência de Fibonacci:");
            for (int i = 0; i < termos; i++)
            {
                Console.WriteLine(fib(i) + " ");
            }

            Console.WriteLine();
        }
    }
}
