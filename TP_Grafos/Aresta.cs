using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Aresta
    {
        int _v;
        int _w;
        int _peso; // é sempre inteiro?
        int _capacidade; // é sempre inteiro?

        public Aresta(int origem, int destino)
        {
            _v = origem; _w = destino;
        }
        public int GetV()
        {
            return _v;
        }
        public int GetW()
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
