using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Aresta
    {
        Vertice _v;
        Vertice _w;
        int _peso; // é sempre inteiro?
        int _capacidade; // é sempre inteiro?

        public Aresta(Vertice origem, Vertice destino)
        {
            _v = origem; _w = destino;
        }
        public Vertice GetV()
        {
            return _v;
        }
        public Vertice GetW()
        {
            return _w;
        }
        public int GetCapacidade()
        {
            return _capacidade;
        }
        public int GetPeso()
        {
            return _peso;
        }

        public void DefinirPeso(int peso)
        {
            _peso = peso;
        }
        public void DefinirCapacidade(int capacidade)
        {
            _capacidade = capacidade;
        }
    }
}
