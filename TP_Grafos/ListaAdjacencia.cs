using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class ListaAdjacencia : IArmazenamento
    {
        List<PseudoVertice> lista;

        public ListaAdjacencia(StreamReader arq) {
            CriarLista(arq);
        }
        private void CriarLista(StreamReader arq)
        {
            int numVertices, numArestas;
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            for (int i = 0; i < int.Parse(valores[0]); i++)
            {
                lista.Add(new PseudoVertice());
            }
            while (linha != null)
            {
                lista[int.Parse(valores[0])].Add(new PseudoAresta(lista[int.Parse(valores[1])], int.Parse(valores[2]), int.Parse(valores[3])));
                linha = arq.ReadLine();
            }
        }
        public void AdicionarVertice();
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade);
        public int GetPeso(int vertA, int vertB);
        public int GetCapacidade(int vertA, int vertB);
        public List<Aresta> GetArestas();

        public int GetQuantVertices();
    }
}
