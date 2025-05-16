using GrafosT2M1;
using System;
internal class Program
{
    private static void Main(string[] args)
    {
        GrafoMatriz grafoM = null;
        GrafoLista grafoL = null;

        //LeitorGrafo leitor = new LeitorGrafo(".\\..\\..\\..\\grafo.txt");
        LeitorGrafo leitor = new LeitorGrafo(".\\..\\..\\..\\grafo.txt");
        int origem = 0;

        leitor.GeraGrafo(ref grafoM);
        
        grafoM.ImprimeGrafo();

        grafoM.ImprimeBusca(grafoM.RetornarBuscaProfundidade(origem), false);

        grafoM.ImprimeBusca(grafoM.RetornarBuscaLargura(origem), true);

        grafoM.ImprimeBusca(grafoM.RetornarDijkstra(origem));


        Console.WriteLine("\n\n\n\n");

        leitor.GeraGrafo(ref grafoL);

        grafoL.ImprimeGrafo();

        grafoL.ImprimeBusca(grafoL.RetornarBuscaProfundidade(origem), false);

        grafoL.ImprimeBusca(grafoL.RetornarBuscaLargura(origem), true);

        grafoL.ImprimeBusca(grafoL.RetornarDijkstra(origem));

        Console.WriteLine("\n\n\n\n");
    }


}