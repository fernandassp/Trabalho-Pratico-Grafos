using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoDirecionado
    {
        IArmazenamento armazenamento;

        public void AdicionarVertice()
        {
            armazenamento.AdicionarVertice();
        }
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade)
        {
            armazenamento.AdicionarAresta(vertA, vertB, peso, capacidade);
        }

        public int GetQuantVertices()
        {
            return armazenamento.GetQuantVertices();
        }

        public List<Aresta> GetArestas()
        {
            return armazenamento.GetArestas();
        }

        public int DijkstraEntre(int origem, int destino)
        {
            int[,] resultados = new int[2, GetQuantVertices()]; // [0,0]: dist vertice 1; [1,0]: pred. vertice 1
            // -1 para null, int max value para infinito

            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[1, i] = -1; // definir predecessores como "null"
                resultados[0, i] = int.MaxValue; //distancias: infinito
            }
            resultados[0, origem - 1] = 0; // dist raiz = 0

            List<int> explorados = new List<int>();
            explorados.Add(origem);

            for (int i = 0; i < GetQuantVertices(); i++)
            {
                List<Aresta> corteS = DefinirCorteS(explorados); //att corte

                int menorDist = int.MaxValue;
                Aresta selecionada = corteS.ElementAt(0);

                foreach (Aresta a in corteS)
                {
                    int distV = resultados[0, a.GetAntecessor() - 1];
                    if (distV != int.MaxValue && a.GetPeso() + distV < menorDist)
                    {
                        menorDist = a.GetPeso() + distV;
                        selecionada = a;
                    }
                }

                explorados.Add(selecionada.GetSucessor());
                resultados[1, selecionada.GetSucessor() - 1] = selecionada.GetAntecessor();
                int distanciaVSelecionada = resultados[0, selecionada.GetAntecessor() - 1];
                resultados[0, selecionada.GetSucessor() - 1] = selecionada.GetPeso() + distanciaVSelecionada;
            }

            return resultados[0, destino - 1];
        }

        private List<Aresta> DefinirCorteS(List<int> explorados)
        {
            List<Aresta> corteS = new List<Aresta>();

            foreach (int vertice in explorados)
            {
                LinkedList<Aresta> incidentes = armazenamento.GetArestasIncidentes(vertice);
                foreach (Aresta incidente in incidentes)
                {
                    if (!explorados.Contains(incidente.GetSucessor()))
                    {
                        corteS.Add(incidente);
                    }
                }
            }

            return corteS;
        }
    }
}