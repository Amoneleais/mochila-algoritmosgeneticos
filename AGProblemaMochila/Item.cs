using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGProblemaMochila
{
    public class Item
    {
        public int Peso { get; set; }
        public int Valor { get; set; }

        public Item(int peso, int valor)
        {
            Peso = peso;
            Valor = valor;
        }
    }
}
