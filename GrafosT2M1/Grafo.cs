using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosT1M1
{
    internal abstract class Grafo
    {
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

        public bool InserirVertice(string label)
        {
            // Não insere caso label já existir
            if (Vertices.Any(x => x.Equals(label)))
            {
                return false;
            }
            Vertices.Add(label);
            return true;
        }

        public bool RemoverVertice(int indice)
        {
            if (indice < 0 || indice >= Vertices.Count)
                return false;

            Vertices.RemoveAt(indice);

            return true;
        }

        public string LabelVertice(int index)
        {
            return Vertices[index];
        }

        public abstract void ImprimeGrafo();

        public abstract bool InserirAresta(int origem, int destino, float peso = 1);

        public abstract bool RemoverAresta(int origem, int destino);

        public abstract bool ExisteAresta(int origem, int destino);

        public abstract float PesoAresta(int origem, int destino);

        public abstract List<int> RetornarVizinhos(int vertice);
    }
}
