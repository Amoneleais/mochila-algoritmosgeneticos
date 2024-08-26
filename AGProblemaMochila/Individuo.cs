using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGProblemaMochila
{
    public class Individuo
    {
        public int[] Cromossomo { get; private set; }
        public int Fitness { get; private set; }
        
        public readonly Mochila Mochila;

        public Individuo(int[] cromossomo, Mochila mochila)
        {
            Cromossomo = cromossomo;
            Mochila = mochila;
            Fitness = CalcularFitness();
        }

        public int CalcularFitness()
        {
            int pesoTotal = 0;
            int valorTotal = 0;

            for (int i = 0; i < Cromossomo.Length; i++)
            {
                if (Cromossomo[i] == 1)
                {
                    pesoTotal += Mochila.Itens[i].Peso;
                    valorTotal += Mochila.Itens[i].Valor;
                }
            }

            return pesoTotal <= Mochila.CapacidadeMochila ? valorTotal : 0;
        }

        public void RecalcularFitness()
        {
            Fitness = CalcularFitness();
        }
    }

}
