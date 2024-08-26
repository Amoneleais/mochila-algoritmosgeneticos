namespace AGProblemaMochila
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> itens = new List<Item>
            {
                new Item(25, 350),
                new Item(35, 400),
                new Item(45, 450),
                new Item(5, 20),
                new Item(25, 70),
                new Item(3, 8),
                new Item(2, 5),
                new Item(2, 5)
            };

            int capacidadeMochila = 104;

            Mochila mochila = new Mochila(itens, capacidadeMochila);

            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Roleta,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_roleta_uniforme.csv"
            );

            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoDoisPontos.Cruzar,
                0.05,
                elitismo: true,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_torneio_doispontos.csv"
            );
        }    
    }
}
