using GrafosT1M1;
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

        Console.WriteLine("\n\n\n\n");

        grafoL.ImprimeGrafo();
    }
}