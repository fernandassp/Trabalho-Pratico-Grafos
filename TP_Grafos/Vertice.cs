using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Vertice
    {
        private LinkedList<Aresta> _arestas;
        private int _numero;

        public Vertice(int numero) { 
            _arestas = new LinkedList<Aresta>();
            _numero = numero;
        }
        public void AddAresta(Aresta a) { 
            _arestas.AddLast(a);
        }
        public int GetNumero() { return _numero; }
        public LinkedList<Aresta> GetArestas() { return _arestas; }
    }
}
