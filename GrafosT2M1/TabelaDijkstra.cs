using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GrafosT2M1
{
    internal class TabelaDijkstra
    {
        private float _distancia;

        private int _verticeAnterior;

        private bool _fechado;

        public float Distancia { get => _distancia; set => _distancia = value; }

        public int VerticeAnterior { get => _verticeAnterior; set => _verticeAnterior = value; }

        public bool Fechado { get => _fechado; set => _fechado = value; }

        public TabelaDijkstra(float distancia, int verticeAnterior, bool fechado)
        {
            _distancia = distancia;
            _verticeAnterior = verticeAnterior;
            _fechado = fechado;
        }
    }
}
