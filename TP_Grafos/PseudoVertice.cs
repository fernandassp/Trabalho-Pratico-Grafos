using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class PseudoVertice
    {
        private LinkedList<PseudoAresta> _arestas;
        private int _numero;

        public PseudoVertice(int numero) { 
            _arestas = new LinkedList<PseudoAresta>();
            _numero = numero;
        }
        public void AddAresta(PseudoAresta p) { 
            _arestas.AddLast(p);
        }
        public void AddAresta(int vertA, int vertB, int peso, int capacidade)
        {
            PseudoAresta novaAresta = 
        }
        public int GetNumero() { return _numero; }
        public LinkedList<PseudoAresta> GetArestas() { return _arestas; }
    }
}
