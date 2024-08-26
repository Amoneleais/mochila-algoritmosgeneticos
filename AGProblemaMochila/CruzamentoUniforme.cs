using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGProblemaMochila
{
    public static class CruzamentoUniforme
    {
        public static Individuo[] Cruzar(Individuo pai1, Individuo pai2)
        {
            Random random = new Random();
            int[] filho1 = new int[pai1.Cromossomo.Length];
            int[] filho2 = new int[pai1.Cromossomo.Length];

            for (int i = 0; i < pai1.Cromossomo.Length; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    filho1[i] = pai1.Cromossomo[i];
                    filho2[i] = pai2.Cromossomo[i];
                }
                else
                {
                    filho1[i] = pai2.Cromossomo[i];
                    filho2[i] = pai1.Cromossomo[i];
                }
            }

            return new Individuo[]
            {
                new Individuo(filho1, pai1.Mochila),
                new Individuo(filho2, pai1.Mochila)
            };
        }
    }

}
