using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random rand = new Random();

        List<int> entradas = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> saidas_esperadas = new List<int>() { 6, 2, 0, 0, 2, 6, 12, 20, 30, 42, 56 };

        int tamanhoPopulacao = 20;
        int totalGeracoes = 1000;

        double[] melhorIndividuo = new double[3];
        double menorErro = double.MaxValue;

        List<double[]> populacao = new List<double[]>();

        // Geração inicial aleatória
        for (int i = 0; i < tamanhoPopulacao; i++)
        {
            populacao.Add(new double[] {
                rand.NextDouble() * 20 - 10,
                rand.NextDouble() * 20 - 10,
                rand.NextDouble() * 20 - 10
            });
        }

        for (int geracao = 0; geracao < totalGeracoes; geracao++)
        {
            List<double[]> melhoresIndividuos = new List<double[]>();
            List<double> erros = new List<double>();

            // Avalia erro de cada indivíduo
            foreach (var individuo in populacao)
            {
                double erro = 0;
                for (int i = 0; i < entradas.Count; i++)
                {
                    double x = entradas[i];
                    double y = individuo[0] * x * x + individuo[1] * x + individuo[2];
                    double diff = y - saidas_esperadas[i];
                    erro += diff * diff;
                }
                erros.Add(erro);
                melhoresIndividuos.Add(individuo);
            }

            // Ordena por erro (bubble sort simples)
            for (int i = 0; i < tamanhoPopulacao - 1; i++)
            {
                for (int j = i + 1; j < tamanhoPopulacao; j++)
                {
                    if (erros[j] < erros[i])
                    {
                        double tempErro = erros[i];
                        erros[i] = erros[j];
                        erros[j] = tempErro;

                        double[] tempInd = melhoresIndividuos[i];
                        melhoresIndividuos[i] = melhoresIndividuos[j];
                        melhoresIndividuos[j] = tempInd;
                    }
                }
            }

            // Atualiza melhor indivíduo
            if (erros[0] < menorErro)
            {
                melhorIndividuo = melhoresIndividuos[0];
                menorErro = erros[0];
                Console.WriteLine($"Geração {geracao} - f(x) = {melhorIndividuo[0]:F4}x² + {melhorIndividuo[1]:F4}x + {melhorIndividuo[2]:F4} | Erro: {menorErro:F6}");
            }

            List<double[]> novaGeracao = new List<double[]>();

            // mantém 2 melhores
            novaGeracao.Add(melhoresIndividuos[0]);
            novaGeracao.Add(melhoresIndividuos[1]);

            // Recombinação (cruzamento) + mutação
            for (int i = 0; i < (tamanhoPopulacao / 2) - 1; i++)
            {
                var pai1 = melhoresIndividuos[i];
                var pai2 = melhoresIndividuos[i + 1];

                double a = (pai1[0] + pai2[0]) / 2;
                double b = (pai1[1] + pai2[1]) / 2;
                double c = (pai1[2] + pai2[2]) / 2;

                if (rand.NextDouble() < 0.2) a += rand.NextDouble() - 0.5;
                if (rand.NextDouble() < 0.2) b += rand.NextDouble() - 0.5;
                if (rand.NextDouble() < 0.2) c += rand.NextDouble() - 0.5;

                novaGeracao.Add(new double[] { a, b, c });
            }

            // Completa população com novos aleatórios
            while (novaGeracao.Count < tamanhoPopulacao)
            {
                novaGeracao.Add(new double[] {
                    rand.NextDouble() * 20 - 10,
                    rand.NextDouble() * 20 - 10,
                    rand.NextDouble() * 20 - 10
                });
            }

            populacao = novaGeracao;
        }

        Console.WriteLine("\nMelhor função encontrada:");
        Console.WriteLine($"f(x) = {melhorIndividuo[0]:F4}x² + {melhorIndividuo[1]:F4}x + {melhorIndividuo[2]:F4}");
        Console.WriteLine($"Erro total: {menorErro:F6}");

        Console.WriteLine("\nComparação entre saídas esperadas e calculadas:");
        for (int i = 0; i < entradas.Count; i++)
        {
            double x = entradas[i];
            double y = melhorIndividuo[0] * x * x + melhorIndividuo[1] * x + melhorIndividuo[2];
            Console.WriteLine($"X = {x}\t| Esperado = {saidas_esperadas[i]}\t| Obtido = {Math.Round(y, 4)}");
        }

        // Tenta arredondar coeficientes e testa se a função arredondada funciona perfeitamente
        int aInt = (int)Math.Round(melhorIndividuo[0]);
        int bInt = (int)Math.Round(melhorIndividuo[1]);
        int cInt = (int)Math.Round(melhorIndividuo[2]);

        bool funciona = true;
        for (int i = 0; i < entradas.Count; i++)
        {
            int x = entradas[i];
            int yArredondado = aInt * (x * x) + bInt * x + cInt;
            if (yArredondado != saidas_esperadas[i])
            {
                funciona = false;
                break;
            }
        }

        if (funciona)
        {
            Console.WriteLine("\nFunção arredondada funciona perfeitamente:");
            Console.WriteLine($"f(x) = {aInt}x² + {bInt}x + {cInt}");

            Console.WriteLine("\nComparação com função arredondada:");
            for (int i = 0; i < entradas.Count; i++)
            {
                int x = entradas[i];
                int y = aInt * (x * x) + bInt * x + cInt;
                Console.WriteLine($"X = {x}\t| Esperado = {saidas_esperadas[i]}\t| Obtido = {y}");
            }
        }
        else
        {
            Console.WriteLine("\nFunção arredondada não bate exatamente.");
        }
    }
}
