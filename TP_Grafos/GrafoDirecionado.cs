using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoDirecionado
    {
        List<Vertice> _vertices;
        List<Aresta> _arestas;
        // lista ou matriz de adjacência de acordo com a densidade

        public void AdicionarAresta(Aresta aresta)
        {
            _arestas.Add(aresta);
        }
        public void AdicionarVertice(Vertice vertice)
        {
            _vertices.Add(vertice);
        }

        public Vertice VerticeDeNumero(int num)
        {
            foreach(Vertice vertice in _vertices)
            {
                if(vertice.GetNumero() == num) return vertice;
            }
            return null;
        }
    }
}
