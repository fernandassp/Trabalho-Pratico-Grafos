using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class MatrizAdjacencia : IArmazenamento //denso
    {

        private Aresta[,] _matriz;
        private Aresta[,] _matrizND;

        private int _quantArestas;

        static int TempoGlobal;

        public MatrizAdjacencia(StreamReader arq)
        {
            CriarMatriz(arq);
        }

        private void CriarMatriz(StreamReader arq)
        {
            int numVertices;
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            numVertices = int.Parse(valores[0]);
            _quantArestas = int.Parse(valores[1]);
            _matriz = new Aresta[numVertices, numVertices];
            _matrizND = new Aresta[numVertices, numVertices];
            linha = arq.ReadLine();
            while (linha != null)
            {
                valores = linha.Split(' ');
                _matriz[int.Parse(valores[0]) - 1, int.Parse(valores[1]) - 1] = new Aresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3])); 

                _matrizND[int.Parse(valores[1]) - 1, int.Parse(valores[0]) - 1] = new Aresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3])); 
                _matrizND[int.Parse(valores[0]) - 1, int.Parse(valores[1]) - 1] = new Aresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3]));
                linha = arq.ReadLine();
            }
            arq.Close();
        }
        public MatrizAdjacencia(int quantVerts, List<Aresta> arestas)
        {
            Traduzir(quantVerts, arestas);
        }
        private void Traduzir(int quantVerts, List<Aresta> arestas)
        {
            _matriz = new Aresta[quantVerts, quantVerts];
            _quantArestas = quantVerts;
            foreach (Aresta a in arestas)
            {
                _matriz[a.GetAntecessor() - 1, a.GetAntecessor() - 1] = a;
            }
        }
        public void AddVertice()
        {
            Aresta[,] matriz2 = new Aresta[_matriz.GetLength(0) + 1, _matriz.GetLength(0) + 1];
            for (int i = 0; i < _matriz.GetLength(0); i++)
            {
                for (int j = 0; j < _matriz.GetLength(1); j++)
                {
                    matriz2[i, j] = _matriz[i, j];
                }
            }
            _matriz = matriz2;
        }
        public void AddAresta(int vertA, int vertB, int peso, int capacidade)
        {
            _matriz[vertA - 1, vertB - 1] = new Aresta(vertA, vertB, peso, capacidade);
            _quantArestas++;
        }
        public int GetPeso(int vertA, int vertB)
        {
            return _matriz[vertA - 1, vertB - 1].GetPeso();
        }
        public int GetCapacidade(int vertA, int vertB)
        {
            return _matriz[vertA - 1, vertB - 1].GetCapacidade();
        }


        public List<Aresta> GetArestas()
        {
            List<Aresta> arestas = new List<Aresta>();
            for (int i = 0; i < _matriz.GetLength(0); i++)
            {
                for (int j = 0; j < _matriz.GetLength(1); j++)
                {
                    if (_matriz[i, j] != null)
                    {
                        arestas.Add(_matriz[i, j]);
                    }
                }
            }
            return arestas;
        }  
        public List<Aresta> GetArestasND()
        {
            List<Aresta> arestas = new List<Aresta>();
            for (int i = 0; i < _matrizND.GetLength(0); i++)
            {
                for (int j = 0; j < _matrizND.GetLength(1); j++)
                {
                    if (_matrizND[i, j] != null)
                    {
                        arestas.Add(_matrizND[i, j]);
                    }
                }
            }
            return arestas;
        }

        public int GetQuantVertices()
        {
            return _matriz.GetLength(0);
        }
        public int GetQuantArestas()
        {
            return _quantArestas;
        }

        public LinkedList<Aresta> GetArestasIncidentes(int numVertice)
        {
            numVertice--;
            LinkedList<Aresta> arestas = new LinkedList<Aresta>();
            for (int w = 0; w < _matriz.GetLength(0); w++)
            {
                if (_matriz[numVertice, w] != null)
                {
                    arestas.AddLast(_matriz[numVertice, w]);
                }
            }
            return arestas;
        }
        public void BuscarEmProfundidade()
        {
            TempoGlobal = 0;

            int[,] resultados = new int[3, GetQuantVertices()]; // -1 pra null
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
                else
                {
                    if (resultados[1, a.GetSucessor() - 1] == 0)
                    {
                        a.DefinirTipo("retorno");
                    }
                    else if (resultados[0, vertice - 1] < resultados[0, a.GetSucessor() - 1])
                    {
                        a.DefinirTipo("avanco");
                    }
                    else
                    {
                        a.DefinirTipo("cruzamento");
                    }
                }
            }
            TempoGlobal++; resultados[1, vertice - 1] = TempoGlobal;
        }
    }
}
