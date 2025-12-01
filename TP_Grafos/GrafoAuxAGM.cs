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
        static int TempoGlobal;



        public List<Aresta> GetArestasIncidentes(int v)
        {
            List<Aresta> arestas = new List<Aresta>();
            

            return arestas;
        }
        public void BuscarEmProfundidade()
        {
            TempoGlobal = 0;

            int[,] resultados = new int[3, _vertices.Count]; // -1 pra null
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[2, i] = -1;
            }

            // p/ cada vértice v, se seu TD = 0, chama BuscaProfundidade(v)
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                if (resultados[0, i] == 0)
                    BuscaProfundidade(i + 1, resultados);
            }
        }
        private void BuscaProfundidade(int vertice, int[,] resultados)
        {
            TempoGlobal++;
            resultados[0, vertice - 1] = TempoGlobal;

            LinkedList<Aresta> arestasIncidentes = GetArestasIncidentes(vertice);

            foreach (Aresta a in arestasIncidentes)
            {
                if (resultados[0, a.GetSucessor() - 1] == 0)
                {
                    a.DefinirTipo("arvore");
                    resultados[2, a.GetSucessor() - 1] = vertice;
                    BuscaProfundidade(a.GetSucessor(), resultados);
                }
                // NÃO DIRECIONADO
                else if (resultados[1, a.GetSucessor() - 1] == 0 && a.GetSucessor() != resultados[2, a.GetAntecessor() - 1])
                {
                    a.DefinirTipo("retorno");
                }

            }
            TempoGlobal++; resultados[1, vertice - 1] = TempoGlobal;
        }

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
