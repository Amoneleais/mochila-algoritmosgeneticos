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

            int execucoes = 30;

            //Roleta
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Roleta,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_roleta.csv",
                numeroExecucoes: execucoes
            );

            //Torneio
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_torneio.csv",
                numeroExecucoes: execucoes
            );

            //Uniforme
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: true,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_uniforme.csv",
                numeroExecucoes: execucoes
            );

            //Dois Pontos
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoDoisPontos.Cruzar,
                0.01,
                elitismo: true,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_doispontos.csv",
                numeroExecucoes: execucoes
            );

            //Mutação 1%
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_mutacao01.csv",
                numeroExecucoes: execucoes
            );

            //Mutação 5%
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.05,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_mutacao05.csv",
                numeroExecucoes: execucoes
            );

            //Elitismo
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: true,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_elitismo.csv",
                numeroExecucoes: execucoes
            );

            //Somente Filhos
            AlgoritmoGenetico.Executar(
                mochila,
                Selecao.Torneio,
                CruzamentoUniforme.Cruzar,
                0.01,
                elitismo: false,
                caminhoCSV: @"C:\Users\manoe\source\repos\AGProblemaMochila\AGProblemaMochila\resultado_somente_filhos.csv",
                numeroExecucoes: execucoes
            );

            Console.WriteLine("Dados de Execução Gerados.:");

            ComparadorEstatistico.ExecutarComparacoes();
        }
    }
}
