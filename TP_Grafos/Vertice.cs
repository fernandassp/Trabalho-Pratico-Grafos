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
        private int _grau;

        public Vertice(int numero)
        {
            _arestas = new LinkedList<Aresta>();
            _numero = numero;
        }
        public void SetGrau(int grau) { 
            _grau = grau;
        }
        public void AddAresta(Aresta a)
        {
            _arestas.AddLast(a);
        }
        public int GetQuantArestas()
        {
            return _arestas.Count;
        }
        public int GetNumero() { return _numero; }
        public LinkedList<Aresta> GetArestas() { return _arestas; }

        public int GrauSaida()
        {
            return _arestas.Count;
        }
        public int GetGrau() {
            return _grau;
        }
    }
}
