using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GrafosT2M1
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
            int maxS = Vertices.MaxBy(x => x.Length).Length + 2;

            // Gera primeira linha com labels
            GeraEspaco(maxS);
            for (int i = 0; i < Vertices.Count; i++)
            {
                Console.Write(Vertices[i]);
                GeraEspaco(maxS - Vertices[i].Length);
            }

            Console.Write("\n\n");

            for (int i = 0; i < Vertices.Count; i++)
            {
                Console.Write(Vertices[i]);
                GeraEspaco(maxS - Vertices[i].Length);

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

            float val = !Ponderado? 1 : peso;

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

        protected override void BuscaProfundidade(int origem, ref List<int> verticesVisitadas)
        {
            verticesVisitadas.Add(origem);
            for(int i = 0; i < Arestas[origem].Count; i++)
            {
                if (Arestas[origem][i] != 0 && !verticesVisitadas.Any(x => x == i))
                {
                    BuscaProfundidade(i, ref verticesVisitadas);
                }
            }
        }

        public override List<int> RetornarBuscaLargura(int origem)
        {
            Console.Write("\n\nBusca por largura: ");

            List<int> verticesVisitadas = new List<int>();
            List<int> fila = new List<int>();
            fila.Add(origem);
            verticesVisitadas.Add(origem);

            while (fila.Count > 0)
            {
                int atual = fila[0];

                for (int i = 0; i < Arestas[atual].Count; i++)
                {
                    if (Arestas[atual][i] != 0 && !verticesVisitadas.Any(x => x == i))
                    {
                        fila.Add(i);
                        verticesVisitadas.Add(i);
                    }
                }
                fila.RemoveAt(0);
            }

            for (int i = 0; i < verticesVisitadas.Count; i++)
            {
                Console.Write($"{LabelVertice(verticesVisitadas[i])}");
                if (i < verticesVisitadas.Count - 1)
                    Console.Write(" -> ");
            }

            return verticesVisitadas;
        }

        public override List<float> RetornaDijkstra(int origem)
        {
            var tabela = new List<TabelaDijkstra>();
            Vertices.ForEach(vertice => tabela.Add(new TabelaDijkstra(-1, -1, false)));

            int verticeAtual = origem;
            tabela[verticeAtual].Distancia = 0;

            do
            {
                List<int> vizinhos = RetornarVizinhos(verticeAtual);
                foreach (int verticeDestino in vizinhos)
                {
                    float distancia = tabela[verticeAtual].Distancia + PesoAresta(verticeAtual, verticeDestino);
                    if (distancia < tabela[verticeDestino].Distancia)
                    {
                        tabela[verticeDestino].Distancia = distancia;
                        tabela[verticeDestino].VerticeAnterior = verticeAtual;
                    }
                }
                tabela[verticeAtual].Fechado = true;
                verticeAtual = tabela.IndexOf(tabela.FirstOrDefault(vertice => !vertice.Fechado && vertice.Distancia >= 0));

            } while ((tabela.Any(vertice => !vertice.Fechado && vertice.Distancia >= 0)));

            return tabela.Select(x => x.Distancia).ToList();
        }
    }
}
