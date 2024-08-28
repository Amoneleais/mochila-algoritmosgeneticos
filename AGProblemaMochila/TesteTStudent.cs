using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace AGProblemaMochila
{
    public static class ComparadorEstatistico
    {
        public static void ExecutarComparacoes()
        {
            // Diretório dos arquivos CSV
            string diretorio = @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\";

            var torneio = CarregarDados(Path.Combine(diretorio, "resultado_torneio.csv"));
            var roleta = CarregarDados(Path.Combine(diretorio, "resultado_roleta.csv"));
            var uniforme = CarregarDados(Path.Combine(diretorio, "resultado_uniforme.csv"));
            var doisPontos = CarregarDados(Path.Combine(diretorio, "resultado_doispontos.csv"));
            var mutacao01 = CarregarDados(Path.Combine(diretorio, "resultado_mutacao01.csv"));
            var mutacao05 = CarregarDados(Path.Combine(diretorio, "resultado_mutacao05.csv"));
            var somenteFilhos = CarregarDados(Path.Combine(diretorio, "resultado_somente_filhos.csv"));
            var elitismo = CarregarDados(Path.Combine(diretorio, "resultado_elitismo.csv"));

            CompararConfiguracoes(torneio, roleta, "Torneio", "Roleta");
            CompararConfiguracoes(uniforme, doisPontos, "Uniforme", "Dois Pontos");
            CompararConfiguracoes(mutacao01, mutacao05, "Mutação 1%", "Mutação 5%");
            CompararConfiguracoes(somenteFilhos, elitismo, "Somente Filhos", "Elitismo");
        }

        private static List<int> CarregarDados(string caminhoCSV)
        {
            var fitness = new List<int>();

            if (File.Exists(caminhoCSV))
            {
                using (var reader = new StreamReader(caminhoCSV))
                {
                    string linha;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (linha.StartsWith("Cromossomo")) continue;

                        var partes = linha.Split(',');
                        var valorFitness = int.Parse(partes[1]);

                        fitness.Add(valorFitness);
                    }
                }
            }

            return fitness;
        }

        private static void CompararConfiguracoes(List<int> grupo1, List<int> grupo2, string nomeGrupo1, string nomeGrupo2)
        {
            var resultado = RealizarTesteT(grupo1.Select(x => (double)x).ToList(), grupo2.Select(x => (double)x).ToList());

            Console.WriteLine($"Comparação: {nomeGrupo1} vs. {nomeGrupo2}:");
            Console.WriteLine($"T-Statistic = {resultado.TStatistic}\nP-Value = {resultado.PValue}\n");

            double nivelSignificancia = 0.05;
            if (resultado.PValue < nivelSignificancia)
            {
                Console.WriteLine("A diferença é significativa.");

                string melhorResultado = resultado.TStatistic > 0 ? nomeGrupo1 : nomeGrupo2;
                Console.WriteLine($"Melhor Resultado com base na Estatística T: {melhorResultado}");
            }
            else
            {
                Console.WriteLine("A diferença não é significativa.\nNão foi possível determinar a melhor configuração.");
            }

            Console.WriteLine("\n");
        }

        private static (double TStatistic, double PValue) RealizarTesteT(List<double> grupo1, List<double> grupo2)
        {
            var estatisticas1 = new DescriptiveStatistics(grupo1);
            var estatisticas2 = new DescriptiveStatistics(grupo2);

            double media1 = estatisticas1.Mean;
            double media2 = estatisticas2.Mean;

            double variancia1 = estatisticas1.Variance;
            double variancia2 = estatisticas2.Variance;

            int n1 = grupo1.Count;
            int n2 = grupo2.Count;

            double denominador = Math.Sqrt((variancia1 / n1) + (variancia2 / n2));
            double tStatistic = (media1 - media2) / denominador;

            int grausLiberdade = n1 + n2 - 2;
            double pValue = 2 * StudentT.CDF(0, 1, grausLiberdade, -Math.Abs(tStatistic));

            return (tStatistic, pValue);
        }
    }
}
