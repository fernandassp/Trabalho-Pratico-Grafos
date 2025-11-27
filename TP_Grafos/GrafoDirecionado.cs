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

        public GrafoDirecionado(StreamReader arq)
        {
            armazenamento = new ListaAdjacencia(arq);
        }
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
            int[,] resultados = new int[2, GetQuantVertices()]; // [0,i]: dist; [1,i]: pred
            // -1 para null, int max value para infinito

            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[1, i] = -1;           // predecessor
                resultados[0, i] = int.MaxValue; // distância
            }
            resultados[0, origem - 1] = 0; // raiz

            List<int> explorados = new List<int>();
            explorados.Add(origem);

            
            while (explorados.Count < GetQuantVertices())
            {
                List<Aresta> corteS = DefinirCorteS(explorados);

                //corte vazio = não há mais caminhos possíveis
                if (corteS.Count != 0)
                {
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

                    // impedir adicionar o mesmo vértice duas vezes
                    if (!explorados.Contains(selecionada.GetSucessor()))
                        explorados.Add(selecionada.GetSucessor());
                    resultados[1, selecionada.GetSucessor() - 1] = selecionada.GetAntecessor();
                    int distanciaVSelecionada = resultados[0, selecionada.GetAntecessor() - 1];
                    resultados[0, selecionada.GetSucessor() - 1] = selecionada.GetPeso() + distanciaVSelecionada;
                } 
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