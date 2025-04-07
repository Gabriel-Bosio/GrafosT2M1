using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GrafosT1M1
{
    internal class GrafoMatriz : Grafo
    {

        private List<List<float>> _arestas;

        public List<List<float>> Arestas
        {
            get { return _arestas; }
            set { _arestas = value; }
        }

        public GrafoMatriz(bool ponderado, bool direcionado) : base(ponderado, direcionado)
        {
            _arestas = new List<List<float>>();
        }

        public bool InserirVertice(string label)
        {
            // Não insere caso label já existir
            if (base.InserirVertice(label))
            {
                int index = Vertices.IndexOf(label);

                Arestas.ForEach(x => x.Add(0));
                Arestas.Add(new List<float>());
                for (int i = 0; i <= index; i++) Arestas[index].Add(0);
                return true;
            }
            return false;
        }

        public bool RemoverVertice(int indice)
        {

            if (base.RemoverVertice(indice)) 
            {
                Arestas.RemoveAt(indice);
                Arestas.ForEach(x => x.RemoveAt(indice));

                return true;
            }
            return false;
        }

        public override void ImprimeGrafo() // Em processo
        {

            // Define o espaçamento entre colunas
            int maxS = Vertices.MaxBy(x => x.Length).Length + 6;

            // Gera primeira linha com labels
            GeraEspaco(maxS);
            for (int i = 0; i < Vertices.Count; i++)
            {
                Console.Write(i + " - " + Vertices[i]);
                GeraEspaco(maxS - Vertices[i].Length - 4);
            }

            Console.Write("\n\n");

            for (int i = 0; i < Vertices.Count; i++)
            {
                Console.Write(i + " - " + Vertices[i]);
                GeraEspaco(maxS - Vertices[i].Length - 4);

                // Imprime coluna
                for (int j = 0; j < Vertices.Count; j++)
                {
                    Console.Write(Arestas[i][j]);
                    GeraEspaco(maxS - Arestas[i][j].ToString().Length);
                }
                Console.Write("\n\n");
            }
        }

        private void GeraEspaco(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write(" ");
            }

        }

        public override bool InserirAresta(int origem, int destino, float peso = 1)
        {
            if (ExisteAresta(origem, destino) || peso < 1) return false;

            float val = !Ponderado && peso != 1 ? 1 : peso;

            Arestas[origem][destino] = val;

            if (!Direcionado) Arestas[destino][origem] = val;

            return true;
        }

        public override bool RemoverAresta(int origem, int destino)
        {
            if (!ExisteAresta(origem, destino)) return false;

            Arestas[origem][destino] = 0;

            if (!Direcionado) Arestas[destino][origem] = 0;

            return true;
        }

        public override bool ExisteAresta(int origem, int destino)
        {
            return Arestas[origem][destino] == 0 ? false : true;
        }

        public override float PesoAresta(int origem, int destino)
        {
            return Arestas[origem][destino];
        }

        public override List<int> RetornarVizinhos(int vertice)
        {
            List<int> vizinhos = new List<int>();
            for (int i = 0; i < Arestas[vertice].Count; i++)
            {
                if (Arestas[vertice][i] != 0) vizinhos.Add(i);
            }

            return vizinhos;
        }

    }
}
