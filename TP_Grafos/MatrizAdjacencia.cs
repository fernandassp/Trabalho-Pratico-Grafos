using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class MatrizAdjacencia : IArmazenamento //denso
    {
        private string[,] matriz;

        public MatrizAdjacencia(StreamReader arq) {
            CriarMatriz(arq);
        }

        private void CriarMatriz(StreamReader arq) {
            int numVertices, numArestas;
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            numVertices = int.Parse(valores[0]);
            matriz = new string[numVertices,numVertices];
            while (linha!=null) {
                valores=linha.Split(' ');
                matriz[int.Parse(valores[0])-1, int.Parse(valores[1])-1] = valores[2]+"-"+valores[3];
                linha=arq.ReadLine();                    
            }
        }
        public void AdicionarVertice() {
            string[,] matriz2 = new string[matriz.Length+1,matriz.Length+1];
            for (int i = 0; i < matriz.Length; i++) {
                for (int j = 0; j < matriz.Length; j++) { 
                    matriz2[i,j] = matriz[i,j];
                }
            }
            matriz = matriz2;
        }
        public void AdicionarAresta(int vertA,int vertB,int peso, int capacidade) {
            matriz[vertA-1,vertB-1] = peso + "-" + capacidade;
        }
        public int GetPeso(int vertA,int vertB) {
            string[] div=matriz[vertA - 1, vertB - 1].Split('-');
            return int.Parse(div[0]);
        }
        public int GetCapacidade(int vertA,int vertB) {
            string[] div=matriz[vertA - 1, vertB - 1].Split('-');
            return int.Parse(div[1]);
        }

        
        public List<Aresta> GetArestas()
        {
            List<Aresta> arestas = new List<Aresta>();
            for(int i = 0; i< matriz.GetLength(0); i++)
            {
                for(int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i,j] != null)
                    {
                        Aresta aresta = new Aresta(i , j);
                        string[] dados = matriz[i,j].Split("-");
                        aresta.DefinirPeso(int.Parse(dados[0]));
                        aresta.DefinirCapacidade(int.Parse(dados[1]));
                        arestas.Add(aresta);
                    }
                }
            }
            return arestas;
        }

        public int GetQuantVertices()
        {
            return matriz.GetLength(0);
        }
    }
}
