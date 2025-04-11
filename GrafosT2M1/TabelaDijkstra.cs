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

        private bool _fechado;

        public float Distancia { get => _distancia; set => _distancia = value; }


        public bool Fechado { get => _fechado; set => _fechado = value; }

        public TabelaDijkstra(float distancia, bool fechado)
        {
            _distancia = distancia;
            _fechado = fechado;
        }
    }
}
