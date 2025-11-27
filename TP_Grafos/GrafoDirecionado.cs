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

        /*public Vertice VerticeDeNumero(int num)
        {
            foreach(Vertice vertice in _vertices)
            {
                if(vertice.GetNumero() == num) return vertice;
            }
            return null;
        }
        */

        public int GetQuantVertices()
        {
            return armazenamento.GetQuantVertices();
        }

        public List<PseudoAresta> GetArestas()
        {
            return armazenamento.GetArestas();
        }

        /*
        private bool ExisteNoGrafo(Vertice v)
        {
            if (_vertices.Contains(v)) //n sei se isso funciona mesmo
            {
                return true;
            }
            return false;
        }*/

        /*
        public int BellmanFordEntre(Vertice origem, Vertice destino)
        {
            if(!ExisteNoGrafo(origem) || !ExisteNoGrafo(destino))
            {
                throw new ArgumentException("Os vértices de origem e destino informados não fazem parte do grafo.");
            }

            for (int i = 0; i < GetVertices().Count; i++)
            {
                GetVertices().ElementAt(i).SetDistancia(int.MaxValue);
                GetVertices().ElementAt(i).SetPredecessor(null);
            }
            Vertice raiz = origem;
            raiz.SetDistancia(0);

            for (int i = 1; i <= GetVertices().Count - 1; i++)
            {
                bool altera = false;
                foreach (Aresta aresta in GetArestas())
                {
                    int distV = aresta.GetV().GetDistancia();
                    int peso = aresta.GetPeso();

                    if (distV != int.MaxValue && aresta.GetW().GetDistancia() > distV + peso) // evitar erro do int.MaxValue
                    {
                        aresta.GetW().SetDistancia(distV + peso);
                        aresta.GetW().SetPredecessor(aresta.GetV());
                        altera = true;
                    }
                }
                if (altera == false)
                {
                    i = GetVertices().Count;
                }
            }

            return destino.GetDistancia(); // ?? tem que testar
        }
        */

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
                List<PseudoAresta> corteS = DefinirCorteS(explorados); //att corte

                int menorDist = int.MaxValue;
                PseudoAresta selecionada = corteS.ElementAt(0);

                foreach (PseudoAresta a in corteS)
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

        private List<PseudoAresta> DefinirCorteS(List<int> explorados)
        {
            List<PseudoAresta> corteS = new List<PseudoAresta>();

            foreach (int vertice in explorados)
            {
                LinkedList<PseudoAresta> incidentes = armazenamento.GetArestasIncidentes(vertice);
                foreach (PseudoAresta incidente in incidentes)
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