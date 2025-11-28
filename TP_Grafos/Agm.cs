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

        public void Imprimir()
        {
            for (int i = 0; i < _vertices.Count - 1; i++)
            {
                Console.Write($"Hub {_vertices[i].GetNumero()} -> ");
                Console.Write($"Rota ({_vertices[i].GetNumero()}, {_vertices[i + 1].GetNumero()}) -> ");

            }
            Console.Write($"Hub {_vertices[_vertices.Count - 1].GetNumero()}");


            Console.WriteLine();
        }
    }
}
