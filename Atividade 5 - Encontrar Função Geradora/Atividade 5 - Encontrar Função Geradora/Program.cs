using System;
using System.Collections.Generic;
using System.Data;

class Program
{
    static Random rand = new Random();
    static string[] operadores = { "+", "-", "*", "/" };
    static string[] terminais = { "x", "1", "2", "3", "4", "5" };

    static void Main()
    {
        List<int> entradas = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> saidas = new List<int> { 6, 2, 0, 0, 2, 6, 12, 20, 30, 42, 56 };

        int tamanhoPop = 100;
        int geracoes = 1000;

        List<string> populacao = new List<string>();
        for (int i = 0; i < tamanhoPop; i++)
            populacao.Add(GerarExpressao(3));

        string melhor = "";
        double melhorErro = double.MaxValue;

        for (int g = 0; g < geracoes; g++)
        {
            List<(string, double)> avaliados = new List<(string, double)>();
            foreach (string ind in populacao)
            {
                double erro = CalcularErro(ind, entradas, saidas);
                avaliados.Add((ind, erro));
                if (erro < melhorErro)
                {
                    melhorErro = erro;
                    melhor = ind;
                    Console.WriteLine($"G{g} Melhor: {melhor} | Erro: {erro:F2}");
                }
            }

            avaliados.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            List<string> novaPop = new List<string> { avaliados[0].Item1, avaliados[1].Item1 };

            while (novaPop.Count < tamanhoPop)
            {
                string pai1 = avaliados[rand.Next(5)].Item1;
                string pai2 = avaliados[rand.Next(5)].Item1;
                string filho = rand.NextDouble() < 0.5 ? Recombinar(pai1, pai2) : Mutar(pai1);
                novaPop.Add(filho);
            }

            populacao = novaPop;
        }

        Console.WriteLine($"\nMelhor expressão final: {melhor}");
        foreach (var x in entradas)
        {
            double y = Avaliar(melhor, x);
            Console.WriteLine($"x={x} | esperado={saidas[x]} | obtido={Math.Round(y, 2)}");
        }
    }

    static string GerarExpressao(int profundidade)
    {
        if (profundidade == 0)
            return terminais[rand.Next(terminais.Length)];
        string op = operadores[rand.Next(operadores.Length)];
        string esq = GerarExpressao(profundidade - 1);
        string dir = GerarExpressao(profundidade - 1);
        return $"({esq} {op} {dir})";
    }

    static double Avaliar(string expr, int x)
    {
        try
        {
            string e = expr.Replace("x", x.ToString());
            var table = new DataTable();
            var result = table.Compute(e, "");
            return Convert.ToDouble(result);
        }
        catch { return double.MaxValue; }
    }

    static double CalcularErro(string expr, List<int> entradas, List<int> saidas)
    {
        double erro = 0;
        for (int i = 0; i < entradas.Count; i++)
        {
            double y = Avaliar(expr, entradas[i]);
            double diff = y - saidas[i];
            erro += diff * diff;
        }
        return erro;
    }

    static string Recombinar(string a, string b)
    {
        int i = EncontrarSubexpressaoInicio(a);
        int j = EncontrarSubexpressaoInicio(b);
        if (i == -1 || j == -1) return a;

        string subA = ExtrairSubexpressao(a, i);
        string subB = ExtrairSubexpressao(b, j);

        if (subA == "" || subB == "") return a;

        return a.Substring(0, i) + subB + a.Substring(i + subA.Length);
    }

    static string Mutar(string expr)
    {
        int i = EncontrarSubexpressaoInicio(expr);
        if (i == -1) return expr;
        string antiga = ExtrairSubexpressao(expr, i);
        string nova = GerarExpressao(2);
        return expr.Substring(0, i) + nova + expr.Substring(i + antiga.Length);
    }

    static int EncontrarSubexpressaoInicio(string expr)
    {
        List<int> abertos = new List<int>();
        for (int i = 0; i < expr.Length; i++)
        {
            if (expr[i] == '(') abertos.Add(i);
            else if (expr[i] == ')' && abertos.Count > 0)
            {
                int ini = abertos[rand.Next(abertos.Count)];
                return ini;
            }
        }
        return -1;
    }

    static string ExtrairSubexpressao(string expr, int ini)
    {
        int cont = 0;
        for (int i = ini; i < expr.Length; i++)
        {
            if (expr[i] == '(') cont++;
            else if (expr[i] == ')') cont--;
            if (cont == 0) return expr.Substring(ini, i - ini + 1);
        }
        return "";
    }
}
