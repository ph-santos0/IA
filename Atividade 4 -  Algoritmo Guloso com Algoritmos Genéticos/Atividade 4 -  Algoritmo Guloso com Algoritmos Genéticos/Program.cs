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

            double melhorDiferenca = double.MaxValue;
            int melhorTotalMoedas = int.MaxValue;
            int melhor20 = 0, melhor11 = 0, melhor5 = 0, melhor1 = 0;

            for (int geracao = 0; geracao < numeroGeracoes; geracao++)
            {
                for (int individuo = 0; individuo < tamanhoPopulacao; individuo++)
                {
                    int qtd20 = rand.Next(0, (int)(troco / 0.20) + 1);
                    int qtd11 = rand.Next(0, (int)(troco / 0.11) + 1);
                    int qtd5 = rand.Next(0, (int)(troco / 0.05) + 1);
                    int qtd1 = rand.Next(0, (int)(troco / 0.01) + 1);

                    double totalMoedas = qtd20 * 0.20 + qtd11 * 0.11 + qtd5 * 0.05 + qtd1 * 0.01;
                    totalMoedas = Math.Round(totalMoedas, 2);

                    if (totalMoedas <= troco)
                    {
                        double diferenca = troco - totalMoedas;
                        int totalMoedasUsadas = qtd20 + qtd11 + qtd5 + qtd1;

                        // Se a diferença for menor que a melhor até agora
                        if (diferenca < melhorDiferenca)
                        {
                            melhorDiferenca = diferenca;
                            melhorTotalMoedas = totalMoedasUsadas;
                            melhor20 = qtd20;
                            melhor11 = qtd11;
                            melhor5 = qtd5;
                            melhor1 = qtd1;
                        }
                        // Se a diferença for igual a melhor e tiver menos moedas
                        else if (diferenca == melhorDiferenca && totalMoedasUsadas < melhorTotalMoedas)
                        {
                            melhorDiferenca = diferenca;
                            melhorTotalMoedas = totalMoedasUsadas;
                            melhor20 = qtd20;
                            melhor11 = qtd11;
                            melhor5 = qtd5;
                            melhor1 = qtd1;
                        }
                    }
                }
            }

            Console.WriteLine("\n--- Melhor solução encontrada ---");
            Console.WriteLine($"Moedas de 20 centavos: {melhor20}");
            Console.WriteLine($"Moedas de 11 centavos: {melhor11}");
            Console.WriteLine($"Moedas de 5 centavos: {melhor5}");
            Console.WriteLine($"Moedas de 1 centavo: {melhor1}");

            double totalFinal = melhor20 * 0.20 + melhor11 * 0.11 + melhor5 * 0.05 + melhor1 * 0.01;
            Console.WriteLine($"Total em moedas: R$ {Math.Round(totalFinal, 2)}");
            Console.WriteLine($"Total de moedas usadas: {melhorTotalMoedas}");
        }
    }
}
