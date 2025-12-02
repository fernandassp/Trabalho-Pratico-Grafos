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
            foreach (Aresta a in _arestas)
            {
                if(a.GetAntecessor()==v||a.GetSucessor()==v)
                    arestas.Add(a);
            }
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

            List<Aresta> arestasIncidentes = GetArestasIncidentes(vertice);

            foreach (Aresta a in arestasIncidentes)
            {
                // Descobrir quem é o outro vértice da aresta (pois é não direcionada)
                int w = (a.GetAntecessor() == vertice) ? a.GetSucessor() : a.GetAntecessor();

                // Aresta de árvore
                if (resultados[0, w - 1] == 0)
                {
                    a.DefinirTipo("arvore");
                    resultados[2, w - 1] = vertice;
                    BuscaProfundidade(w, resultados);
                }
                // Aresta de retorno (não direcionado)
                else if (resultados[1, w - 1] == 0 && w != resultados[2, vertice - 1])
                {
                    a.DefinirTipo("retorno");
                }
            }

            TempoGlobal++;
            resultados[1, vertice - 1] = TempoGlobal;
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
        public void AddVertices(List<Vertice> vertice)
        {
            _vertices = vertice;
        }
        public void AddArestas(List<Aresta> aresta)
        {
            _arestas = aresta;
        }
    }

}
