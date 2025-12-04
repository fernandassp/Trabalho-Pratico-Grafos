using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Vertice
    {
        private List<Aresta> _arestas;
        private int _numero;
        private int _grau;
        private int _cor;

        public bool Colorido()
        {
            return _cor != 0;
        }
        public void Colorir(int cor)
        {
            if(!Colorido())
            {
                _cor = cor;
            }
        }
        public bool EhAdjacenteA(int num)
        {
            foreach(Aresta a in _arestas)
            {
                if(a.GetSucessor() == num || a.GetAntecessor() == num)
                    return true;
            }
            return false;
        }
        public Vertice(int numero)
        {
            _arestas = new List<Aresta>();
            _numero = numero;
        }
        public void SetGrau(int grau) { 
            _grau = grau;
        }
        public void AddAresta(Aresta a)
        {
            _arestas.Add(a);
        }
        public int GetQuantArestas()
        {
            return _arestas.Count;
        }
        public int GetNumero() { return _numero; }
        public List<Aresta> GetArestas() { return _arestas; }

        public int GrauSaida()
        {
            return _arestas.Count;
        }
        public int GetGrau() {
            return _grau;
        }
    }
}
