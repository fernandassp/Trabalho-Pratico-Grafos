using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    interface IArmazenamento
    {
        static IArmazenamento EscolherInicio(StreamReader arq)
        {
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            int vertice = int.Parse(valores[0]);
            int arestas = int.Parse(valores[1]);
            arq.BaseStream.Seek(0, SeekOrigin.Begin);
            arq.DiscardBufferedData();
            if (arestas / (vertice * (vertice - 1)) > 0.5)
            {
                return new MatrizAdjacencia(arq);
            }
            else
            {
                return new ListaAdjacencia(arq);
            }
        }
        static bool DeveMudar(int quantVertice, int quantAresta, IArmazenamento arm)
        {
            if (quantAresta / (quantVertice * (quantVertice - 1)) > 0.5)
            {
                if (arm is MatrizAdjacencia)
                    return false;
                else
                    return true;
            }
            else
            {
                if (arm is ListaAdjacencia)
                    return false;
                else
                    return true;
            }
        }
        static IArmazenamento Mudar(IArmazenamento arm, int quantVert, List<Aresta> arestas)
        {
            if (arm is MatrizAdjacencia)
                return new ListaAdjacencia(quantVert, arestas);
            else
                return new MatrizAdjacencia(quantVert, arestas);
        }
        public void AdicionarVertice();
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade);
        public int GetPeso(int vertA, int vertB);
        public int GetCapacidade(int vertA, int vertB);
        public List<Aresta> GetArestas();
        public int GetQuantArestas();
        public int GetQuantVertices();
        public LinkedList<Aresta> GetArestasIncidentes(int numVertice);
    }
}
