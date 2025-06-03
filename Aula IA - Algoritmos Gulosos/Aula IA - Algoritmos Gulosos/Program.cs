using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_IA___Algoritmos_Gulosos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double cent20 = 0, cent11 = 0, cent5 = 0, cent1 = 0;

            Console.Write("Digite o valor do troco: ");
            double troco = double.Parse(Console.ReadLine());

            while (troco >= 0.01)
            {
                if (troco >= 0.20)
                {
                    troco = Math.Round(troco - 0.20, 2);
                    cent20++;
                }
                else if (troco >= 0.11)
                {
                    troco = Math.Round(troco - 0.11, 2);
                    cent11++;
                }
                else if (troco >= 0.05)
                {
                    troco = Math.Round(troco - 0.05, 2);
                    cent5++;
                }
                else if (troco >= 0.01)
                {
                    troco = Math.Round(troco - 0.01, 2);
                    cent1++;
                }
            }

            Console.WriteLine("Solução Gulosa:");
            Console.WriteLine("Moedas de 20 centavos: " + cent20);
            Console.WriteLine("Moedas de 11 centavos: " + cent11);
            Console.WriteLine("Moedas de 5 centavos: " + cent5);
            Console.WriteLine("Moedas de 1 centavo: " + cent1);
        }
    }
}
