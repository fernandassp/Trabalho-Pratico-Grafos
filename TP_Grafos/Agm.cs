using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Agm
    {
        List<Vertice> _vertices;
        List<Aresta> _arestas;

        public int CustoTotal()
        {
            int soma = 0;
            foreach(Aresta aresta in _arestas)
            {
                soma += aresta.GetPeso();
            }
            return soma;
        }
        public Agm()
        {
            _vertices = new List<Vertice>();
            _arestas = new List<Aresta>();
        }

        public void AddVertice(Vertice vertice)
        {
            _vertices.Add(vertice);
        }
        public void AddAresta(Aresta aresta)
        {
            _arestas.Add(aresta);
        }

        public int QuantVertices()
        {
            return _vertices.Count;
        }

        public bool ContemOVertice(int vertice)
        {
            foreach (Vertice vert in _vertices)
            {
                if (vert.GetNumero() == vertice)
                {
                    return true;
                }
            }
            return false;
        }

        public string Imprimir()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _vertices.Count - 1; i++)
            {
                sb.Append($"Hub {_vertices[i].GetNumero()} -> ");
                sb.Append($"Rota ({_vertices[i].GetNumero()}, {_vertices[i + 1].GetNumero()}) -> ");

            }
            sb.Append($"Hub {_vertices[_vertices.Count - 1].GetNumero()}\n");

            sb.AppendLine("CUSTO TOTAL: " + CustoTotal());

            sb.AppendLine();
            return sb.ToString();
        }
    }
}
