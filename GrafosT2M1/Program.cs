using GrafosT2M1;
using System;
internal class Program
{
    private static void Main(string[] args)
    {
        GrafoMatriz grafoM = null;
        GrafoLista grafoL = null;

        LeitorGrafo leitor = new LeitorGrafo("C:\\Estudos_Univali\\Grafos\\GrafosT2M1\\GrafosT2M1\\grafo.txt");

        leitor.GeraGrafo(ref grafoM);
        leitor.GeraGrafo(ref grafoL);

        grafoM.ImprimeGrafo();

        grafoM.RetornarBuscaProfundidade(5);

        Console.WriteLine();

        grafoM.RetornarBuscaLargura(5);

        Console.WriteLine();

        List<float> menorCaminhoM = grafoM.RetornaDijkstra(5);

        Console.Write($"Menores caminhos Dijkstra a partir de {grafoM.LabelVertice(5)}: ");
        for(int i = 0; i < menorCaminhoM.Count; i++)
        {
            Console.Write($"{grafoM.LabelVertice(i)}: {menorCaminhoM[i]}   ");
        }

        Console.WriteLine("\n\n\n\n");

        grafoL.ImprimeGrafo();

        grafoL.RetornarBuscaProfundidade(5);

        Console.WriteLine();

        grafoL.RetornarBuscaLargura(5);

        Console.WriteLine("\n\n\n\n");
    }


}