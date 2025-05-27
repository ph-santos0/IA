using System;
using System.Collections.Generic;

class ProgramacaoEvolutiva
{
    static void Main(string[] args)
    {
        Random random = new Random();

        Console.Write("Digite o tamanho do vetor (deve ser par): ");
        if (!int.TryParse(Console.ReadLine(), out int tamanho) || tamanho % 2 != 0)
        {
            Console.WriteLine("Erro: O tamanho deve ser um número inteiro par.");
            return;
        }

        double taxaMutacao = 0.05;
        int maxGeracoes = 1000;
        int limiteSemMelhora = 100;

        List<int> melhorCromossomo = new List<int>();
        for (int i = 0; i < tamanho; i++)
        {
            melhorCromossomo.Add(random.Next(0, 2));
        }

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

        var resultadoInicial = CalcularFuncaoObjetivo(melhorCromossomo);
        double melhorFx = resultadoInicial.fx;
        double melhorX1 = resultadoInicial.x1;
        double melhorX2 = resultadoInicial.x2;

        Console.WriteLine($"Solução Inicial - f(x1, x2): {melhorFx:F5}");

        int geracoesSemMelhora = 0;

        for (int geracao = 1; geracao <= maxGeracoes; geracao++)
        {
            var cromossomoMutado = new List<int>(melhorCromossomo);
            for (int i = 0; i < cromossomoMutado.Count; i++)
            {
                if (random.NextDouble() < taxaMutacao)
                {
                    cromossomoMutado[i] = 1 - cromossomoMutado[i];
                }
            }

            var resultadoMutado = CalcularFuncaoObjetivo(cromossomoMutado);
            double fxMutado = resultadoMutado.fx;

            if (fxMutado < melhorFx)
            {
                melhorCromossomo = cromossomoMutado;
                melhorFx = fxMutado;
                melhorX1 = resultadoMutado.x1;
                melhorX2 = resultadoMutado.x2;
                geracoesSemMelhora = 0;
                Console.WriteLine($"Geração {geracao}: Nova melhor solução - f(x1, x2): {melhorFx:F5}");
            }
            else
            {
                geracoesSemMelhora++;
            }

            if (geracoesSemMelhora >= limiteSemMelhora)
            {
                Console.WriteLine($"\nParando: Nenhuma melhora nas últimas {limiteSemMelhora} gerações.");
                break;
            }
        }

        Console.WriteLine("\n--- Melhor Solução Encontrada ---");
        Console.WriteLine($"Cromossomo: {string.Join("", melhorCromossomo)}");
        Console.WriteLine($"x1 = {melhorX1:F5}");
        Console.WriteLine($"x2 = {melhorX2:F5}");
        Console.WriteLine($"f(x1, x2) = {melhorFx:F5}");
    }
}