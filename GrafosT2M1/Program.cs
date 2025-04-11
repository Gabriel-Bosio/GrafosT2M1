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
        
        grafoM.ImprimeGrafo();

        grafoM.ImprimeBusca( grafoM.RetornarBuscaProfundidade(5), false);

        grafoM.ImprimeBusca(grafoM.RetornarBuscaLargura(5), true);

        grafoM.ImprimeBusca(grafoM.RetornarDijkstra(5));


        leitor.GeraGrafo(ref grafoL);

        grafoL.ImprimeGrafo();

        grafoL.ImprimeBusca(grafoL.RetornarBuscaProfundidade(5), false);

        grafoL.ImprimeBusca(grafoL.RetornarBuscaLargura(5), true);

        grafoL.ImprimeBusca(grafoL.RetornarDijkstra(5));
    }


}