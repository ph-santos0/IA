using System;
using System.Collections.Generic;

class ProgramacaoEvolutivaComPopulacao
{
    static void Main(string[] args)
    {
        Random random = new Random();

        Console.Write("Digite o tamanho do vetor: ");
        if (!int.TryParse(Console.ReadLine(), out int tamanho))
        {
            Console.WriteLine("Erro: Entrada inválida.");
            return;
        }

        double taxaMutacao = 0.05;
        int populacaoTamanho = 10;
        int maxGeracoes = 100;

        (double fx, double x1, double x2) CalcularFuncaoObjetivo(List<int> cromossomo)
        {
            int t = cromossomo.Count;
            int meio = t / 2;
            int decimalX1 = 0;
            int decimalX2 = 0;

            for (int i = 0; i < meio; i++)
            {
                decimalX1 = decimalX1 * 2 + cromossomo[i];
            }

            for (int i = meio; i < t; i++)
            {
                decimalX2 = decimalX2 * 2 + cromossomo[i];
            }

            double divisor = Math.Pow(2, meio) - 1;
            double x1_val = decimalX1 * (6.0 / divisor);
            double x2_val = decimalX2 * (6.0 / divisor);

            Func<double, double> f = (x) => 0.25 * Math.Pow(x, 4) - 3 * Math.Pow(x, 3) + 11 * Math.Pow(x, 2) - 13 * x;

            return (f(x1_val) + f(x2_val), x1_val, x2_val);
        }
       ;

        var populacao = new List<List<int>>();
        double melhorFx = double.MaxValue;
        List<int> melhorCromossomo = null;
        double melhorX1 = 0, melhorX2 = 0;

        Console.WriteLine("--- População Inicial ---");
        for (int i = 0; i < populacaoTamanho; i++)
        {
            var cromossomo = new List<int>();
            for (int j = 0; j < tamanho; j++)
            {
                cromossomo.Add(random.Next(0, 2));
            }
            populacao.Add(cromossomo);

            var res = CalcularFuncaoObjetivo(cromossomo);

            Console.WriteLine($"Indivíduo {i + 1} gerado:");
            Console.WriteLine($"Cromossomo: {string.Join("", cromossomo)}");
            Console.WriteLine($"x1 = {res.x1:F5}, x2 = {res.x2:F5}");
            Console.WriteLine($"f(x1, x2) = {res.fx:F5}");
            Console.WriteLine("------------------------");

            if (res.fx < melhorFx)
            {
                melhorFx = res.fx;
                melhorCromossomo = new List<int>(cromossomo);
                melhorX1 = res.x1;
                melhorX2 = res.x2;
            }
        }
        Console.WriteLine($"Melhor Inicial Encontrado - f(x1, x2): {melhorFx:F5}\n");

        for (int geracao = 1; geracao <= maxGeracoes; geracao++)
        {
            for (int i = 0; i < populacaoTamanho; i++)
            {
                var cromossomoMutado = new List<int>(populacao[i]);
                for (int j = 0; j < tamanho; j++)
                {
                    if (random.NextDouble() < taxaMutacao)
                    {
                        cromossomoMutado[j] = 1 - cromossomoMutado[j];
                    }
                }

                var resMutado = CalcularFuncaoObjetivo(cromossomoMutado);

                if (resMutado.fx < melhorFx)
                {
                    melhorFx = resMutado.fx;
                    melhorCromossomo = new List<int>(cromossomoMutado);
                    melhorX1 = resMutado.x1;
                    melhorX2 = resMutado.x2;
                    Console.WriteLine($"Geração {geracao}: Nova melhor solução encontrada - f(x1,x2): {melhorFx:F5}");
                }
                populacao[i] = cromossomoMutado;
            }
        }

        Console.WriteLine("\n--- Melhor Solução Encontrada ---");
        var resFinal = CalcularFuncaoObjetivo(melhorCromossomo); // Recalcula x1, x2 para exibir
        Console.WriteLine($"Cromossomo: {string.Join("", melhorCromossomo)}");
        Console.WriteLine($"x1 = {resFinal.x1:F5}, x2 = {resFinal.x2:F5}");
        Console.WriteLine($"f(x1, x2) = {resFinal.fx:F5}");
    }
}