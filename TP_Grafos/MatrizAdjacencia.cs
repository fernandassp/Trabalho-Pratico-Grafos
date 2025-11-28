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
            _matriz = new Aresta[numVertices, numVertices];
            linha = arq.ReadLine();
            while (linha != null)
            {
                valores = linha.Split(' ');
                _matriz[int.Parse(valores[0]) - 1, int.Parse(valores[1]) - 1] = new Aresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3]));
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
            foreach (Aresta a in arestas)
            {
                _matriz[a.GetAntecessor() - 1, a.GetAntecessor() - 1] = a;
            }
        }
        public void AdicionarVertice()
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
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade)
        {
            _matriz[vertA - 1, vertB - 1] = new Aresta(vertA, vertB, peso, capacidade);
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

        public int GetQuantVertices()
        {
            return _matriz.GetLength(0);
        }
        public int GetQuantArestas()
        {
            int quant = 0;
            for (int i = 0; i < _matriz.GetLength(0); i++)
            {
                for (int j = 0; j < _matriz.GetLength(1); j++)
                {
                    if (_matriz[i, j] != null)
                    {
                        quant++;
                    }
                }
            }
            return quant;
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
        public void MostrarMatriz()
        {

        }
    }
}
