using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_IA___Fibonacci
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Digite a quantidade de números: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" ");

            for (int i = 0; i < quantidade; i++)
            {
                Console.WriteLine(fibonnaci(i));
            }
        }

        public static long fibonnaci(int N)
        {
            if (N == 0) { return 0; }
            else if (N == 1) { return 1; }
            else { return fibonnaci(N - 1) + fibonnaci(N - 2); }
        }
    }
}
