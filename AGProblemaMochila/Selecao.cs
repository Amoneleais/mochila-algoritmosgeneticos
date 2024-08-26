using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGProblemaMochila
{
    public class Selecao
    {
        public static Individuo Roleta(List<Individuo> populacao)
        {
            int somaFitness = populacao.Sum(ind => ind.Fitness);
            int valorAleatorio = new Random().Next(somaFitness);
            int somaParcial = 0;

            foreach (var individuo in populacao)
            {
                somaParcial += individuo.Fitness;
                if (somaParcial >= valorAleatorio)
                {
                    return individuo;
                }
            }
            return populacao.Last(); // Caso nenhum seja selecionado, retorna o último
        }

        public static Individuo Torneio(List<Individuo> populacao)
        {
            Random random = new Random();
            List<Individuo> torneio = new List<Individuo>();

            for (int i = 0; i < 3; i++) // Seleciona 3 indivíduos aleatórios para o torneio
            {
                torneio.Add(populacao[random.Next(populacao.Count)]);
            }

            return torneio.OrderByDescending(ind => ind.Fitness).First(); // Retorna o melhor entre os 3
        }
    }

}
