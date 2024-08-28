using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AGProblemaMochila
{
    public static class AlgoritmoGenetico
    {
        private static void Mutacao(Individuo individuo, double taxaMutacao)
        {
            Random random = new Random();
            for (int i = 0; i < individuo.Cromossomo.Length; i++)
            {
                if (random.NextDouble() < taxaMutacao)
                {
                    individuo.Cromossomo[i] = 1 - individuo.Cromossomo[i]; // Inverte o bit
                }
            }
            individuo.RecalcularFitness();
        }

        public static void Executar(Mochila mochila, Func<List<Individuo>, Individuo> selecao, Func<Individuo, Individuo, Individuo[]> cruzamento, double taxaMutacao, bool elitismo, string caminhoCSV, int numeroExecucoes)
        {
            List<Individuo> melhoresIndividuos = new List<Individuo>();

            for (int i = 0; i < numeroExecucoes; i++)
            {
                List<Individuo> populacao = GerarPopulacaoInicial(mochila);

                for (int geracao = 0; geracao < 1000; geracao++)
                {
                    List<Individuo> novaPopulacao = new List<Individuo>();

                    if (elitismo)
                    {
                        int quantidadeElitismo = (int)(populacao.Count * 0.10); // 10% da população
                        var melhores = populacao.OrderByDescending(ind => ind.Fitness).Take(quantidadeElitismo);
                        novaPopulacao.AddRange(melhores);
                    }

                    while (novaPopulacao.Count < populacao.Count)
                    {
                        Individuo pai1 = selecao(populacao);
                        Individuo pai2 = selecao(populacao);

                        Individuo[] filhos = cruzamento(pai1, pai2);

                        Mutacao(filhos[0], taxaMutacao);
                        Mutacao(filhos[1], taxaMutacao);

                        novaPopulacao.Add(filhos[0]);
                        if (novaPopulacao.Count < populacao.Count)
                        {
                            novaPopulacao.Add(filhos[1]);
                        }
                    }

                    populacao = novaPopulacao;
                }

                // Adiciona o melhor indivíduo da execução atual à lista de melhoresIndividuos
                var melhorIndividuo = populacao.OrderByDescending(ind => ind.Fitness).FirstOrDefault();
                if (melhorIndividuo != null)
                {
                    melhoresIndividuos.Add(melhorIndividuo);
                }
            }

            SalvarCSV(melhoresIndividuos, caminhoCSV);
        }

        private static List<Individuo> GerarPopulacaoInicial(Mochila mochila)
        {
            List<Individuo> populacao = new List<Individuo>();
            Random random = new Random();

            for (int i = 0; i < 30; i++)
            {
                int[] cromossomo = new int[mochila.Itens.Count];
                for (int j = 0; j < cromossomo.Length; j++)
                {
                    cromossomo[j] = random.Next(2); // Gera 0 ou 1 aleatoriamente
                }
                populacao.Add(new Individuo(cromossomo, mochila));
            }

            return populacao;
        }

        private static void SalvarCSV(List<Individuo> populacao, string caminhoCSV)
        {
            if (File.Exists(caminhoCSV))
            {
                File.Delete(caminhoCSV);
            }

            using (StreamWriter sw = new StreamWriter(caminhoCSV))
            {
                // Cabeçalho do CSV
                sw.WriteLine("Cromossomo,Fitness");

                // Adiciona os dados de cada indivíduo
                foreach (var individuo in populacao)
                {
                    // Converte o cromossomo para uma string separada por vírgulas
                    string cromossomoString = string.Join(",", individuo.Cromossomo);
                    // Escreve a linha no CSV com o cromossomo e o fitness
                    sw.WriteLine($"{cromossomoString},{individuo.Fitness}");
                }
            }
        }
    }
}
