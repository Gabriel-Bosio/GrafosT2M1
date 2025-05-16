using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GrafosT2M1
{
    internal abstract class Grafo
    {

        #region Atributos, propriedades e construtor

        private bool _ponderado;
        private bool _direcionado;
        private List<string> _vertices;

        public bool Ponderado
        {
            get { return _ponderado; }
        }

        public bool Direcionado
        {
            get { return _direcionado; }
        }

        public List<string> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Grafo(bool ponderado, bool direcionado)
        {
            this._ponderado = ponderado;
            this._direcionado = direcionado;
            _vertices = new List<string>();
        }

        #endregion

        #region Controle vértices
        protected bool InserirVertice(string label)
        {
            // Não insere caso label já existir
            if (Vertices.Any(x => x.Equals(label)))
            {
                return false;
            }
            Vertices.Add(label);
            return true;
        }

        protected bool RemoverVertice(int indice)
        {
            //Não remove caso índice inválido
            if (indice < 0 || indice >= Vertices.Count) 
                return false;

            Vertices.RemoveAt(indice);

            return true;
        }

        public string LabelVertice(int index)
        {
            return Vertices[index];
        }

        #endregion

        #region Grafo e controle de arestas
        public abstract void ImprimeGrafo();

        public abstract bool InserirAresta(int origem, int destino, float peso = 1);

        public abstract bool RemoverAresta(int origem, int destino);

        public abstract bool ExisteAresta(int origem, int destino);

        public abstract float PesoAresta(int origem, int destino);

        public abstract List<int> RetornarVizinhos(int vertice);

        #endregion

        #region Buscas
        public List<int> RetornarBuscaProfundidade(int origem)
        {
            List<int> verticesVisitadas = new List<int>();

            BuscaProfundidade(origem, ref verticesVisitadas); //Inicia a busca de forma recursiva

            return verticesVisitadas;
        }

        private void BuscaProfundidade(int origem, ref List<int> verticesVisitadas)
        {
            verticesVisitadas.Add(origem);
            List<int> vizinhos = RetornarVizinhos(origem);
            foreach (int vizinho in vizinhos)
            {
                if (!verticesVisitadas.Any(v => v == vizinho))
                {
                    BuscaProfundidade(vizinho, ref verticesVisitadas);
                }
            }
        }

        public List<int> RetornarBuscaLargura(int origem)
        {

            List<int> verticesVisitadas = new List<int>();
            List<int> fila = new List<int>();

            //Visita o vertice de origem (Adiciona na fila e adiciona na lista de visitados)
            verticesVisitadas.Add(origem);
            fila.Add(origem);
            

            while (fila.Count > 0)
            {
                int atual = fila[0]; //Seleciona o próximo vértice da fila
                List<int> vizinhos = RetornarVizinhos(atual);

                foreach (int vizinho in vizinhos)
                {
                    if (!verticesVisitadas.Any(v => v == vizinho)) //Visita todos os vizinhos que ainda não foram vizitados 
                    {
                        verticesVisitadas.Add(vizinho);
                        fila.Add(vizinho);   
                    }
                }
                fila.RemoveAt(0); //Remove da fila o vértice que acabou de ser selecionado
            }

            return verticesVisitadas;
        }

        public List<(float Distancia, List<int> Caminho)> RetornarDijkstra(int origem)
        {
            var tabela = new List<TabelaDijkstra>(); 
            Vertices.ForEach(vertice => tabela.Add(new TabelaDijkstra(-1, -1, false))); // Composto por distância e verificador de fechamento

            //Inicia distância 0 para origem e seleciona como vertice atual
            int verticeAtual = origem;
            tabela[verticeAtual].Distancia = 0; 

            do
            {
                //Verifica a distância dos vizinhos do vértice atual e fecha o vértice em seguida
                List<int> vizinhos = RetornarVizinhos(verticeAtual);
                foreach (int verticeDestino in vizinhos)
                {
                    float distancia = tabela[verticeAtual].Distancia + PesoAresta(verticeAtual, verticeDestino);
                    if (distancia < tabela[verticeDestino].Distancia || tabela[verticeDestino].Distancia < 0) 
                    {
                        tabela[verticeDestino].Distancia = distancia; //Atualiza a distância caso seja a primeira ou a menor
                        tabela[verticeDestino].Anterior = verticeAtual;
                    }
                }
                tabela[verticeAtual].Fechado = true;

                verticeAtual = tabela.IndexOf(tabela.FirstOrDefault(vertice => !vertice.Fechado && vertice.Distancia >= 0)); //Seleciona próximo vértice aberto com distância conhecida

            } while (verticeAtual >= 0); //Encerra laço caso não tenha mais vértices acessíveis

            List<(float Distancia, List<int>)> caminhos = new();
            for(int i = 0; i < tabela.Count; i++)
            {
                float distancia = tabela[i].Distancia;
                List<int> caminho = TabelaDijkstra.ObterCaminho(tabela, i);

                caminhos.Add(new(distancia, caminho));
            }

            return caminhos;
        }

        //Sobrecarga que imprime ordem de acesso em buscas de profundidade e largura
        public void ImprimeBusca(List<int> vizitas, bool isLargura = false)
        {
            if (isLargura) 
                Console.Write("\nBusca por Largura: ");
            else
                Console.Write("\nBusca por Profundidade: ");

            for (int i = 0; i < vizitas.Count; i++)
            {
                Console.Write($"{vizitas[i]}");
                if(i < vizitas.Count - 1) Console.Write($" -> ");
            }
            Console.WriteLine();
        }

        //Sobrecarga que imprime distâncias de um ponto de origem para cada vértice com base em busca de Dijkstra
        public void ImprimeBusca(List<(float Distancia, List<int> Caminho)> caminhos)
        {
            Console.WriteLine($"\nMenores caminhos a partir de {LabelVertice(caminhos.IndexOf(caminhos.FirstOrDefault(x => x.Distancia == 0)))}: ");
            for (int i = 0; i < caminhos.Count; i++)
            {
                string dist = caminhos[i].Distancia != -1? Convert.ToString(caminhos[i].Distancia) : "Infinito";
                Console.Write($"\n{LabelVertice(i)} : Distancia = {dist}     Caminho -- ");
                if (caminhos[i].Caminho.Count > 0)
                {
                    for (int j = 0; j < caminhos[i].Caminho.Count; j++)
                    {
                        if (j < caminhos[i].Caminho.Count - 1)
                        {
                            Console.Write($"{LabelVertice(caminhos[i].Caminho[j])} -> ");
                        }
                        else
                        {
                            Console.Write($"{LabelVertice(caminhos[i].Caminho[j])}\n");
                        }
                    }
                }
                else Console.Write("Nenhum\n");
                
            }
        }
        #endregion
    }
}
