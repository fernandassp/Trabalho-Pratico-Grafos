using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoAuxFleury
    {
        private List<Vertice> _vertices;
        private List<Aresta> _arestas;
        static int TempoGlobal;

        public GrafoAuxFleury(int quantV, List<Aresta> arestas)
        {
            _vertices = new List<Vertice>();
            _arestas = new List<Aresta>();
            for(int i =1; i <= quantV; i++)
            {
                _vertices.Add(new Vertice(i));
            }
            _arestas = arestas;
        }
        public List<int> EncontrarCicloEuleriano(int verticeInicial)
        {
            List<int> ciclo = new List<int>();
            List<Aresta> arestasRestantes = new List<Aresta>();
            foreach (Aresta a in _arestas)
            {
                arestasRestantes.Add(new Aresta(a.GetAntecessor(), a.GetSucessor(), a.GetPeso(), a.GetCapacidade()));
            }

            int verticeAtual = verticeInicial;
            ciclo.Add(verticeAtual);

            
            while (arestasRestantes.Count > 0)
            {
                //arestas que saem do vértice atual
                List<Aresta> arestasSaindo = new List<Aresta>();
                foreach (Aresta a in arestasRestantes)
                {
                    if (a.GetAntecessor() == verticeAtual)
                    {
                        arestasSaindo.Add(a);
                    }
                }

                Aresta arestaEscolhida;

                // escolher uma que não seja ponte 
                if (arestasSaindo.Count > 1)
                {
                    arestaEscolhida = EscolherArestaNaoPonte(arestasRestantes, verticeAtual, arestasSaindo);
                }
                else
                {
                    arestaEscolhida = arestasSaindo[0];
                }

                int proximoVertice = arestaEscolhida.GetSucessor();
                ciclo.Add(proximoVertice);

               
                arestasRestantes.Remove(arestaEscolhida);

                
                verticeAtual = proximoVertice;
            }

            return ciclo;
        }
        public List<Aresta> GetArestasIncidentes(int v)
        {
            List<Aresta> arestas = new List<Aresta>();
            foreach (Aresta a in _arestas)
            {
                if (a.GetAntecessor() == v)
                    arestas.Add(a);
            }
            return arestas;
        }
        public List<int> BuscarEmProfundidade(bool inverter)
        {
            TempoGlobal = 0;
            List<int> visitados = new List<int>();
            if (inverter)
            {
                foreach (Aresta aresta in _arestas)
                {
                    int aux = aresta.GetSucessor();
                    aresta.SetSucessor(aresta.GetAntecessor());
                    aresta.SetAntecessor(aux);
                }
            }

            int[,] resultados = new int[3, _vertices.Count]; // -1 pra null
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[2, i] = -1;
            }

            // p/ cada vértice v, se seu TD = 0, chama BuscaProfundidade(v)
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                if (resultados[0, i] == 0)
                    BuscaProfundidade(i + 1, resultados, visitados);
            }

            return visitados;
        }
        private void BuscaProfundidade(int vertice, int[,] resultados, List<int> visitados)
        {
            TempoGlobal++;
            resultados[0, vertice - 1] = TempoGlobal;
            visitados.Add(vertice);


            List<Aresta> arestasIncidentes = GetArestasIncidentes(vertice);
           

            foreach (Aresta a in arestasIncidentes)
            {
                
                int w =  a.GetSucessor();

               
                if (resultados[0, w - 1] == 0)
                {
                    a.DefinirTipo("arvore");
                    resultados[2, w - 1] = vertice;
                    BuscaProfundidade(w, resultados, visitados);
                }
                
                else
                {
                    if (resultados[1,w-1] == 0)
                    {
                        
                        a.DefinirTipo("retorno");
                    }
                    else if (resultados[0,vertice-1] < resultados[0,w-1])
                    {
                        
                        a.DefinirTipo("avanco");
                    }
                    else
                    {
                        a.DefinirTipo("cruzamento");
                    }
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
