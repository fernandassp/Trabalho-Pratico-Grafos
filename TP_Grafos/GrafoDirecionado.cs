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
            armazenamento = IArmazenamento.Escolher(arq);
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
        public int DijkstraEntre(int origem, int destino)
        {
            int[,] resultados = new int[2, GetQuantVertices()]; // [0,i]: dist; [1,i]: pred
            // -1 pra null, intMaxValue pra infinito
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[1, i] = -1;           // predecessor
                resultados[0, i] = int.MaxValue; // distância
            }
            resultados[0, origem - 1] = 0; // raiz

            List<int> explorados = new List<int>();
            explorados.Add(origem);


            for (int i = 0; i < GetQuantVertices(); i++)
            {
                List<Aresta> corteS = DefinirCorteS(explorados);

                // corte vazio = não há mais caminhos possíveis
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

                    explorados.Add(selecionada.GetSucessor());
                    resultados[1, selecionada.GetSucessor() - 1] = selecionada.GetAntecessor();
                    int distanciaVSelecionada = resultados[0, selecionada.GetAntecessor() - 1];
                    resultados[0, selecionada.GetSucessor() - 1] = selecionada.GetPeso() + distanciaVSelecionada;
                }
            }

            return resultados[0, destino - 1];
        }


        public Agm Prim()
        {

            Agm agm = new Agm();
            Vertice raiz = new Vertice(1);
            agm.AddVertice(raiz);

            while (agm.QuantVertices() != armazenamento.GetQuantVertices()) 
            {
                //encontrar menor aresta v,w em que v ta em v(t) e w nao
                Aresta menorAresta = ArestaMenorPesoPrim(agm);
                Vertice novoVertice = new Vertice(menorAresta.GetSucessor());
                novoVertice.AddAresta(menorAresta);
                agm.AddVertice(novoVertice);
                agm.AddAresta(menorAresta);
            }

            return agm;

        }

        private Aresta ArestaMenorPesoPrim(Agm agm)
        {
            int menor = int.MaxValue;
            Aresta menorAresta = GetArestas().ElementAt(0);
            foreach (Aresta a in GetArestas())
            {
                if (a.GetPeso() < menor && agm.ContemOVertice(a.GetAntecessor()) && !agm.ContemOVertice(a.GetSucessor()))
                {
                    menor = a.GetPeso();
                    menorAresta = a;
                }
            }

            return menorAresta;
        } 
    }
}