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

            while (troco > 0)
            {

                if (troco > 0)
                {
                    Math.Round(troco = troco - 0.20);
                    cent20++;
                    Console.WriteLine("entrou");
                }
                if (troco >= 0.11)
                {
                    Math.Round(troco = troco - 0.11);
                    cent11++;
                    Console.WriteLine("entrou");
                }
                if (troco >= 0.05)
                {
                    Math.Round(troco = troco - 0.05);
                    cent5++;
                    Console.WriteLine("entrou");
                }
                if (troco >= 0.01)
                {
                    Math.Round(troco = troco - 0.01);
                    cent1++;
                    Console.WriteLine("entrou");
                }
                else
                {
                    break;
                }
            }


            Console.WriteLine(cent20 + " " + cent11 + " " + cent5 + " " + cent1);




        }
    }
}
