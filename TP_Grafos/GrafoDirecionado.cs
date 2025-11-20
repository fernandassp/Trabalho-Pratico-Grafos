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

        public GrafoDirecionado()
        {
            _vertices = new List<Vertice>();
            _arestas = new List<Aresta>();
        }
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

        public List<Vertice> GetVertices()
        {
            return _vertices;
        }
        public List<Aresta> GetArestas()
        {
            return _arestas;
        }

        private bool ExisteNoGrafo(Vertice v)
        {
            if (_vertices.Contains(v)) //n sei se isso funciona mesmo
            {
                return true;
            }
            return false;
        }
        public int BellmanFordEntre(Vertice origem, Vertice destino)
        {
            if(!ExisteNoGrafo(origem) || !ExisteNoGrafo(destino))
            {
                throw new ArgumentException("Os vértices de origem e destino informados não fazem parte do grafo.");
            }

            for (int i = 0; i < GetVertices().Count; i++)
            {
                GetVertices().ElementAt(i).SetDistancia(int.MaxValue);
                GetVertices().ElementAt(i).SetPredecessor(null);
            }
            Vertice raiz = origem;
            raiz.SetDistancia(0);

            for (int i = 1; i <= GetVertices().Count - 1; i++)
            {
                bool altera = false;
                foreach (Aresta aresta in GetArestas())
                {
                    int distV = aresta.GetV().GetDistancia();
                    int peso = aresta.GetPeso();

                    if (distV != int.MaxValue && aresta.GetW().GetDistancia() > distV + peso) // evitar erro do int.MaxValue
                    {
                        aresta.GetW().SetDistancia(distV + peso);
                        aresta.GetW().SetPredecessor(aresta.GetV());
                        altera = true;
                    }
                }
                if (altera == false)
                {
                    i = GetVertices().Count;
                }
            }

            return destino.GetDistancia(); // ?? tem que testar
        }
    }
}
