using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGProblemaMochila
{
    public class Mochila
    {
        public List<Item> Itens { get; set; }
        public int CapacidadeMochila { get; set; }

        public Mochila(List<Item> itens, int capacidadeMochila)
        {
            Itens = itens;
            CapacidadeMochila = capacidadeMochila;
        }
    }
}
