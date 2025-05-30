using System;
using System.Collections.Generic;
using System.Linq;

public class AlgoritmoGenetico
{
    public static void Main(string[] args)
    {
        Random random = new Random();

        Console.Write("Digite o tamanho do vetor (cromossomo): ");
        if (!int.TryParse(Console.ReadLine(), out int tamanhoDoCromossomo) || tamanhoDoCromossomo <= 0)
        {
            Console.WriteLine("Erro: Tamanho inválido. Deve ser um inteiro positivo.");
            return;
        }

        double taxaDeMutacao = 0.05;
        int tamanhoDaPopulacao = 10;
        int numeroMaximoDeGeracoes = 45;

        bool exibirPopulacaoInicial = false;
        bool exibirFilhosGerados = false;

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

            double divisorBase = Math.Pow(2, meio) - 1;

            double valX1 = 0;
            double valX2 = 0;

            if (divisorBase > 0)
            {
                valX1 = decimalX1 * (6.0 / divisorBase);
                valX2 = decimalX2 * (6.0 / divisorBase);
            }
            else if (meio == 0)
            {
                valX1 = 0;
                valX2 = 0;
            }

            Func<double, double> calcularParteFuncao = (x) => 0.25 * Math.Pow(x, 4) - 3 * Math.Pow(x, 3) + 11 * Math.Pow(x, 2) - 13 * x;
            return (calcularParteFuncao(valX1) + calcularParteFuncao(valX2), valX1, valX2);
        }

        void ExibirIndividuo(List<int> cromossomo, string prefixo = "")
        {
            var (fx, x1, x2) = CalcularFuncaoObjetivo(cromossomo);
            Console.Write(prefixo);
            Console.WriteLine($"Cromossomo: {string.Join("", cromossomo)}");
            Console.WriteLine($"x1 = {x1:F5}, x2 = {x2:F5}");
            Console.WriteLine($"f(x1, x2) = {fx:F5}");
        }

        // cruzamento/recombinação
        (List<int> filho1, List<int> filho2) RealizarCruzamento(List<int> pai1, List<int> pai2)
        {
            int t = pai1.Count;
            int pontoDeCorte = (t > 0) ? random.Next(0, t) : 0;

            var filho1 = new List<int>(pai1);
            var filho2 = new List<int>(pai2);

            for (int i = pontoDeCorte; i < t; i++)
            {
                int temp = filho1[i];
                filho1[i] = filho2[i];
                filho2[i] = temp;
            }
            return (filho1, filho2);
        }

        // mutação
        List<int> AplicarMutacao(List<int> cromossomoOriginal)
        {
            var cromossomoMutado = new List<int>(cromossomoOriginal);
            for (int i = 0; i < cromossomoMutado.Count; i++)
            {
                if (random.NextDouble() < taxaDeMutacao)
                {
                    cromossomoMutado[i] = 1 - cromossomoMutado[i]; // inverte o bit
                }
            }
            return cromossomoMutado;
        }

        var populacaoAtual = new List<List<int>>();
        for (int i = 0; i < tamanhoDaPopulacao; i++)
        {
            var novoIndividuo = new List<int>();
            for (int j = 0; j < tamanhoDoCromossomo; j++)
            {
                novoIndividuo.Add(random.Next(0, 2)); // adiciona bit 0 ou 1
            }
            populacaoAtual.Add(novoIndividuo);
        }

        List<int> melhorCromossomoGlobal = null;
        double melhorFxGlobal = double.MaxValue;

        foreach (var individuo in populacaoAtual)
        {
            if (exibirPopulacaoInicial)
            {
                ExibirIndividuo(individuo, $"Indivíduo Inicial:\n");
                Console.WriteLine("------------------------");
            }
            var (fxIndividuo, _, _) = CalcularFuncaoObjetivo(individuo);
            if (fxIndividuo < melhorFxGlobal)
            {
                melhorFxGlobal = fxIndividuo;
                melhorCromossomoGlobal = new List<int>(individuo);
            }
        }
        Console.WriteLine($"Melhor Solução Inicial - f(x1, x2): {melhorFxGlobal:F5}\n");

        for (int geracao = 1; geracao <= numeroMaximoDeGeracoes; geracao++)
        {
            for (int i = 0; i < tamanhoDaPopulacao / 2; i++)
            {
                int indicePai1 = random.Next(0, tamanhoDaPopulacao);
                int indicePai2 = random.Next(0, tamanhoDaPopulacao);

                var (filho1Original, filho2Original) = RealizarCruzamento(populacaoAtual[indicePai1], populacaoAtual[indicePai2]);

                var filho1Mutado = AplicarMutacao(filho1Original);
                var filho2Mutado = AplicarMutacao(filho2Original);

                if (exibirFilhosGerados)
                {
                    Console.WriteLine($"\n--- Geração {geracao}, Par de Filhos {i + 1} ---");
                    ExibirIndividuo(filho1Mutado, "Filho 1 gerado: \n");
                    ExibirIndividuo(filho2Mutado, "Filho 2 gerado: \n");
                }

                var (fxFilho1, _, _) = CalcularFuncaoObjetivo(filho1Mutado);
                var (fxFilho2, _, _) = CalcularFuncaoObjetivo(filho2Mutado);

                // atualização do melhor global
                if (fxFilho1 < melhorFxGlobal)
                {
                    melhorFxGlobal = fxFilho1;
                    melhorCromossomoGlobal = filho1Mutado;
                    Console.WriteLine($"Geração {geracao}: Nova melhor solução (de Filho 1) - f(x1,x2): {melhorFxGlobal:F5}");
                }
                if (fxFilho2 < melhorFxGlobal)
                {
                    melhorFxGlobal = fxFilho2;
                    melhorCromossomoGlobal = filho2Mutado;
                    Console.WriteLine($"Geração {geracao}: Nova melhor solução (de Filho 2) - f(x1,x2): {melhorFxGlobal:F5}");
                }

                populacaoAtual[indicePai1] = filho1Mutado;
                populacaoAtual[indicePai2] = filho2Mutado;
            }
        }

        Console.WriteLine("\n--- Melhor Solução Encontrada Globalmente ---");
        if (melhorCromossomoGlobal != null)
        {
            ExibirIndividuo(melhorCromossomoGlobal);
        }
        else
        {
            Console.WriteLine("Nenhuma solução foi encontrada (população inicial vazia ou erro).");
        }
    }
}