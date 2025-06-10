using System;

namespace Atividade_4___Algoritmo_Guloso_com_Algoritmos_Genéticos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double troco;

            Console.Write("Digite o valor do troco: ");
            troco = double.Parse(Console.ReadLine());

            Random rand = new Random();
            int tamanhoPopulacao = 100;
            int numeroGeracoes = 500;
            double taxaMutacao = 0.1;

            int[,] populacao = new int[tamanhoPopulacao, 4];

            double menorCusto = double.MaxValue;

            int melhor20 = 0, melhor11 = 0, melhor5 = 0, melhor1 = 0;

            for (int i = 0; i < tamanhoPopulacao; i++)
            {
                populacao[i, 0] = rand.Next(0, (int)(troco / 0.20) + 2);
                populacao[i, 1] = rand.Next(0, (int)(troco / 0.11) + 2);
                populacao[i, 2] = rand.Next(0, (int)(troco / 0.05) + 2);
                populacao[i, 3] = rand.Next(0, (int)(troco / 0.01) + 2);
            }

            for (int geracao = 0; geracao < numeroGeracoes; geracao++)
            {
                for (int i = 0; i < tamanhoPopulacao; i++)
                {
                    double totalValorMoedas = populacao[i, 0] * 0.20 + populacao[i, 1] * 0.11 + populacao[i, 2] * 0.05 + populacao[i, 3] * 0.01;
                    totalValorMoedas = Math.Round(totalValorMoedas, 2);
                    int totalMoedasUsadas = populacao[i, 0] + populacao[i, 1] + populacao[i, 2] + populacao[i, 3];

                    double diferenca = Math.Abs(troco - totalValorMoedas);
                    double custoAtual = diferenca + (totalMoedasUsadas * 0.0001);

                    if (custoAtual < menorCusto)
                    {
                        menorCusto = custoAtual;
                        melhor20 = populacao[i, 0];
                        melhor11 = populacao[i, 1];
                        melhor5 = populacao[i, 2];
                        melhor1 = populacao[i, 3];
                    }
                }

                int[,] novaPopulacao = new int[tamanhoPopulacao, 4];

                novaPopulacao[0, 0] = melhor20;
                novaPopulacao[0, 1] = melhor11;
                novaPopulacao[0, 2] = melhor5;
                novaPopulacao[0, 3] = melhor1;

                for (int i = 1; i < tamanhoPopulacao; i++)
                {
                    int pai1 = rand.Next(tamanhoPopulacao);
                    int pai2 = rand.Next(tamanhoPopulacao);

                    for (int g = 0; g < 4; g++)
                    {
                        novaPopulacao[i, g] = rand.NextDouble() < 0.5 ? populacao[pai1, g] : populacao[pai2, g];

                        if (rand.NextDouble() < taxaMutacao)
                        {
                            int ajuste = rand.Next(-1, 2);
                            novaPopulacao[i, g] = Math.Max(0, novaPopulacao[i, g] + ajuste);
                        }
                    }
                }

                populacao = novaPopulacao;
            }

            Console.WriteLine("\n--- Melhor solução encontrada ---");
            Console.WriteLine($"Moedas de 20 centavos: {melhor20}");
            Console.WriteLine($"Moedas de 11 centavos: {melhor11}");
            Console.WriteLine($"Moedas de 5 centavos: {melhor5}");
            Console.WriteLine($"Moedas de 1 centavo: {melhor1}");

            double totalFinal = melhor20 * 0.20 + melhor11 * 0.11 + melhor5 * 0.05 + melhor1 * 0.01;
            int melhorTotalMoedas = melhor20 + melhor11 + melhor5 + melhor1;

            Console.WriteLine($"Total em moedas: R$ {Math.Round(totalFinal, 2)}");
            Console.WriteLine($"Total de moedas usadas: {melhorTotalMoedas}");
        }
    }
}
