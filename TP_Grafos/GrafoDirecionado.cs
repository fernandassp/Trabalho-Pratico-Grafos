using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoDirecionado
    {
        IArmazenamento _armazenamento;

        public GrafoDirecionado(StreamReader arq)
        {
            _armazenamento = IArmazenamento.EscolherInicio(arq);
        }
        public void DeveMudar()
        {
            if (IArmazenamento.DeveMudar(_armazenamento.GetQuantVertices(), _armazenamento.GetQuantArestas(), _armazenamento))
            {
                _armazenamento = IArmazenamento.Mudar(_armazenamento, GetQuantVertices(), GetArestas());
            }
        }
        public void AdicionarVertice()
        {
            _armazenamento.AddVertice();
            DeveMudar();
        }
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade)
        {
            _armazenamento.AddAresta(vertA, vertB, peso, capacidade);
            DeveMudar();
        }

        public int GetQuantVertices()
        {
            return _armazenamento.GetQuantVertices();
        }

        public List<Aresta> GetArestas()
        {
            return _armazenamento.GetArestas();
        }
        private List<Aresta> DefinirCorteS(List<int> explorados)
        {
            List<Aresta> corteS = new List<Aresta>();

            foreach (int vertice in explorados)
            {

                LinkedList<Aresta> incidentes = _armazenamento.GetArestasIncidentes(vertice);

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
        public int[,] DijkstraEntre(int origem, int destino)
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

            return resultados;
        }

        public int DinicEntre(int s, int t)
        {

            int n = GetQuantVertices();
            // estrutura a rede residual G'(f) e coloca lista de adjacência
            List<ArestaResidual>[] adj = new List<ArestaResidual>[n + 1];
            for (int i = 0; i <= n; i++) adj[i] = new List<ArestaResidual>();

            // constroi a rede residual G’(f)
            foreach (Aresta aresta in GetArestas())
            {
                int w = aresta.GetAntecessor();
                int v = aresta.GetSucessor();
                int cap = aresta.GetCapacidade();

                // adiciona aresta direta (w -> v) 
                ArestaResidual direta = new ArestaResidual(v, cap, 0, adj[v].Count);
                // adiciona aresta reversa (v -> w) 
                ArestaResidual reversa = new ArestaResidual(w, 0, 0, adj[w].Count);

                adj[w].Add(direta);
                adj[v].Add(reversa);
            }

            int fluxoMaximo = 0;
            int[] nivel = new int[n + 1];

            // constroi a rede em níveis GL a partir de G’(f)
            while (BuscaLarguraDinic(s, t, adj, nivel))
            {
                // array q controla a próxima aresta a ser visitada
                int[] prox = new int[n + 1];

                int fluxoBloqueio;

                // determina um fluxo de bloqueio fb em GL
                while ((fluxoBloqueio = BuscaProfundidadeDinic(s, t, int.MaxValue, adj, nivel, prox)) != 0)
                {
                    fluxoMaximo += fluxoBloqueio; // atualiza o fluxo
                }
            }

            return fluxoMaximo;
        }

        // busca em largura 
        private bool BuscaLarguraDinic(int s, int t, List<ArestaResidual>[] adj, int[] nivel)
        {
            Array.Fill(nivel, -1); // marca todos os vértices como não visitados
            nivel[s] = 0;
            Queue<int> fila = new Queue<int>();
            fila.Enqueue(s);

            while (fila.Count > 0)
            {
                int u = fila.Dequeue();
                foreach (ArestaResidual aresta in adj[u])
                {
                    // verifica se há capacidade residual e se o vértice ainda não foi nivelado
                    if (aresta.GetCapacidade() - aresta.GetFluxo() > 0 && nivel[aresta.GetPara()] == -1)
                    {
                        nivel[aresta.GetPara()] = nivel[u] + 1;
                        fila.Enqueue(aresta.GetPara());
                    }
                }
            }
            // retorna verdadeiro se o destino é alcançável 
            return nivel[t] != -1;
        }

        // busca em profundidade com fluxo
        private int BuscaProfundidadeDinic(int u, int t, int fluxoEmpurrado, List<ArestaResidual>[] adj, int[] nivel, int[] prox)
        {
            if (fluxoEmpurrado == 0 || u == t) return fluxoEmpurrado;

            for (; prox[u] < adj[u].Count; prox[u]++)
            {
                ArestaResidual aresta = adj[u][prox[u]];
                int para = aresta.GetPara();
                int capResidual = aresta.GetCapacidade() - aresta.GetFluxo();

                // verifica se o vértice é válido
                if (nivel[u] + 1 == nivel[para] && capResidual > 0)
                {
                    int delta = BuscaProfundidadeDinic(para, t, Math.Min(fluxoEmpurrado, capResidual), adj, nivel, prox);

                    // se encontrou fluxo, atualiza e retorna
                    if (delta > 0)
                    {
                        aresta.SetFluxo(aresta.GetFluxo() + delta);

                        ArestaResidual arestaReversa = adj[para][aresta.GetReverso()];
                        arestaReversa.SetFluxo(arestaReversa.GetFluxo() - delta);

                        return delta;
                    }
                }
            }
            return 0;
        }

        public AgmK AGM_Kruskal()
        {
            AgmK agm = new AgmK();

            List<Aresta> ordenadas = new List<Aresta>(_armazenamento.GetArestas().OrderBy(a => a.GetPeso()));
            
            agm.AddVertices(GetQuantVertices());
            

            agm.AddAresta(ordenadas.ElementAt(0));

            int j = 1;
            int quantVertices = _armazenamento.GetQuantVertices();
            while (agm.GetArestasT().Count < quantVertices - 1 && j < ordenadas.Count)
            {
                Aresta nova = ordenadas.ElementAt(j);
               
                if (!agm.ContemAresta(nova) && !agm.ArestaFazCiclo(nova))
                {
                    Console.WriteLine("add");
                    agm.AddAresta(nova);
                }
                j++;
            }

            return agm;
        }

        public Agm Prim()
        {

            Agm agm = new Agm();
            Vertice raiz = new Vertice(1);
            agm.AddVertice(raiz);

            while (agm.QuantVertices() != _armazenamento.GetQuantVertices())
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

        
        public bool FortementeConexo(GrafoAuxFleury grafo)
        {
            List<int> visitados1 = grafo.BuscarEmProfundidade(false);
            
            if (visitados1.Count != GetQuantVertices())
            { return false; }
            List<int> visitados2 = grafo.BuscarEmProfundidade(true);

            return visitados2.Count == GetQuantVertices();

        }

        public string ImprimirCicloFleury(List<int> ciclo)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<ciclo.Count-1; i++)
            {
                sb.Append(ciclo[i] + " -> ");
            }
            sb.AppendLine(ciclo[ciclo.Count-1] + "");
            sb.AppendLine();
            return sb.ToString();
        }
        public List<int> MetodoFleury()
        {
            GrafoAuxFleury auxFleury = new GrafoAuxFleury(GetQuantVertices(), GetArestas());

            if (FortementeConexo(auxFleury))
            {
                int verticeInicial = 1;
                List<int> ciclo = auxFleury.EncontrarCicloEuleriano(verticeInicial);
                return ciclo;
            }


            return null;
        }
    }
}