using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoAuxAGM
    {
        private List<Vertice> _vertices;
        private List<Aresta> _arestas;

        public List<Vertice> Vertices()
        {
            return _vertices;
        }
        public List<Aresta> Arestas()
        {
            return _arestas;
        }
        public void AddVertice(Vertice vertice)
        {
            _vertices.Add(vertice);
        }
        public void AddAresta(Aresta aresta)
        {
            _arestas.Add(aresta);
        }
    }

}
