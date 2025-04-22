using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Digite a quantidade de dígitos da senha: ");
        int quantidade = int.Parse(Console.ReadLine());

        long total = (long)Math.Pow(10, quantidade);
        Console.WriteLine($"\nTotal de combinações possíveis: {total}\n");

        for (long i = 0; i < total; i++)
        {
            string numero = i.ToString();
            int zerosFaltando = quantidade - numero.Length;

            for (int z = 0; z < zerosFaltando; z++)
            {
                Console.Write("0");
            }

            Console.WriteLine(numero);
        }
    }
}