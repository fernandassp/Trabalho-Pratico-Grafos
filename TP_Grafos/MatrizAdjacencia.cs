using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    /*
    internal class MatrizAdjacencia : IArmazenamento //denso
    {

        private string[,] _matriz;

        public MatrizAdjacencia(StreamReader arq) {
            CriarMatriz(arq);
        }

        private void CriarMatriz(StreamReader arq) {
            int numVertices;
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            numVertices = int.Parse(valores[0]);
            _matriz = new string[numVertices,numVertices];
            while (linha!=null) {
                valores=linha.Split(' ');
                _matriz[int.Parse(valores[0])-1, int.Parse(valores[1])-1] = valores[2]+"-"+valores[3];
                linha=arq.ReadLine();                    
            }
        }
        public void AdicionarVertice() {
            string[,] matriz2 = new string[_matriz.Length+1,_matriz.Length+1];
            for (int i = 0; i < _matriz.Length; i++) {
                for (int j = 0; j < _matriz.Length; j++) { 
                    matriz2[i,j] = _matriz[i,j];
                }
            }
            _matriz = matriz2;
        }
        public void AdicionarAresta(int vertA,int vertB,int peso, int capacidade) {
            _matriz[vertA-1,vertB-1] = peso + "-" + capacidade;
        }
        public int GetPeso(int vertA,int vertB) {
            string[] div=_matriz[vertA - 1, vertB - 1].Split('-');
            return int.Parse(div[0]);
        }
        public int GetCapacidade(int vertA,int vertB) {
            string[] div=_matriz[vertA - 1, vertB - 1].Split('-');
            return int.Parse(div[1]);
        }

        
        public List<Aresta> GetArestas()
        {
            List<Aresta> arestas = new List<Aresta>();
            for(int i = 0; i< _matriz.GetLength(0); i++)
            {
                for(int j = 0; j < _matriz.GetLength(1); j++)
                {
                    if (_matriz[i,j] != null)
                    {
                        Aresta aresta = new Aresta(i , j);
                        string[] dados = _matriz[i,j].Split("-");
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
        public List<PseudoAresta> GetArestas() { }
        public List<PseudoVertice> GetArestasIncidentes(int numVertice) {
            List<PseudoVertice>
            for (int i = 0; i < matriz.GetLength(0); i++) {
                if (matriz[numVertice, i] != null)
                {

                }
            }
        }
    }
    */
    //
}
