using GrafosT2M1;
using System;
internal class Program
{
    private static void Main(string[] args)
    {
        GrafoMatriz grafoM = null;
        GrafoLista grafoL = null;

        //LeitorGrafo leitor = new LeitorGrafo(".\\..\\..\\..\\grafo.txt");
        LeitorGrafo leitor = new LeitorGrafo(".\\..\\..\\..\\grafoAva.txt");

        leitor.GeraGrafo(ref grafoM);
        
        grafoM.ImprimeGrafo();

        grafoM.ImprimeBusca( grafoM.RetornarBuscaProfundidade(0), false);

        grafoM.ImprimeBusca(grafoM.RetornarBuscaLargura(0), true);

        grafoM.ImprimeBusca(grafoM.RetornarDijkstra(0));


        Console.WriteLine("\n\n\n\n");

        leitor.GeraGrafo(ref grafoL);

        grafoL.ImprimeGrafo();

        grafoL.ImprimeBusca(grafoL.RetornarBuscaProfundidade(0), false);

        grafoL.ImprimeBusca(grafoL.RetornarBuscaLargura(0), true);

        grafoL.ImprimeBusca(grafoL.RetornarDijkstra(0));

        Console.WriteLine("\n\n\n\n");
    }


}