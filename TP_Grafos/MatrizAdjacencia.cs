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

        public MatrizAdjacencia(MatrizAdjacencia original)
        {
            int n = original._matrizND.GetLength(0);
            _matrizND = new Aresta[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    _matrizND[i, j] = original._matrizND[i, j]; // cópia superficial é suficiente

            _quantArestas = original._quantArestas;
        }
        public MatrizAdjacencia(StreamReader arq)
        {
            CriarMatriz(arq);
        }
        public MatrizAdjacencia(int quantVertices)
        {
            _quantArestas = 0;
            _matrizND = new Aresta[quantVertices,quantVertices];
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
        public void AddArestaND(Aresta add)
        {
            _matrizND[add.GetAntecessor()-1, add.GetSucessor()-1] = add;
            _matrizND[add.GetSucessor() - 1, add.GetAntecessor() - 1] = new Aresta(add.GetSucessor(), add.GetAntecessor(),add.GetPeso(),add.GetCapacidade());
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
        public List<Vertice> GetVerticesND() //NAO DIR
        {
            List<Vertice> vertices = new List<Vertice>();
            for (int i = 0; i < _matrizND.GetLength(0); i++)
            {
                Vertice novo = new Vertice(i+1);
                for (int j = 0; j < _matrizND.GetLength(1); j++)
                {
                    if (_matrizND[i, j] != null)
                    {
                        novo.SetGrau(GetGrauEntrada(i+1)+GetGrauSaida(i+1));
                        novo.AddAresta(_matrizND[i, j]);
                    }
                }
                Console.WriteLine("vertice nd: "+(i+1));
            }
            return vertices;
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

        public List<Aresta> GetArestasIncidentes(int numVertice)
        {
            numVertice--;
            List<Aresta> arestas = new List<Aresta>();
            for (int w = 0; w < _matriz.GetLength(0); w++)
            {
                if (_matriz[numVertice, w] != null)
                {
                    arestas.Add(_matriz[numVertice, w]);
                }
            }
            return arestas;
        }
        public int GetGrauSaida(int vertice)
        {
            int count = 0;
            for (int i = 0; i < _matriz.GetLength(0); i++)
            {
                if (_matriz[vertice - 1, i] != null)
                    count++;
            }
            return count;
        }
        public int GetGrauEntrada(int vertice)
        {
            int count = 0;
            for (int i = 0; i < _matriz.GetLength(0); i++)
            {
                if (_matriz[i, vertice - 1] != null)
                    count++;
            }
            return count;
        }
        public bool TemArestaDeRetorno()
        {
            int n = _matrizND.GetLength(0);
            bool[] visitado = new bool[n];

            for (int i = 0; i < n; i++)
            {
                if (!visitado[i])
                {
                    if (BuscaProfundidade(i, -1, visitado))
                        return true;
                }
            }
            return false;
        }
        private bool BuscaProfundidade(int v, int antecessor, bool[] visitado)
        {
            visitado[v] = true;

            for (int i = 0; i < _matrizND.GetLength(0); i++)
            {
                if (_matrizND[v, i] != null)
                {
                    if (!visitado[i])
                    {
                        if (BuscaProfundidade(i, v, visitado))
                            return true;
                    }
                    else if (i != antecessor)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
