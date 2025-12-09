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

                List<Aresta> incidentes = _armazenamento.GetArestasIncidentes(vertice);

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

        public string Kruskal()
        {
            int quantVertices = _armazenamento.GetQuantVertices();
            int pesoTotal = 0;
            string resultado = "";

            List<Aresta> arestas = _armazenamento.GetArestas().OrderBy(a=> a.GetPeso()).ToList();
            MatrizAdjacencia matriz = new MatrizAdjacencia(quantVertices);
            
            matriz.AddArestaND(arestas[0]);
            resultado += arestas[0].GetAntecessor() + "->" + arestas[0].GetSucessor()+" ";
            pesoTotal += arestas[0].GetPeso();
            int quantVezes= 1;
            int arestaVez= 1;
            while (quantVertices-1 > quantVezes) {
                MatrizAdjacencia teste = new MatrizAdjacencia(matriz);
                teste.AddArestaND(arestas[arestaVez]);
                if (!teste.TemArestaDeRetorno())
                {
                    matriz = new MatrizAdjacencia(teste);
                    quantVezes++;
                    resultado += arestas[arestaVez].GetAntecessor() + "->" + arestas[arestaVez].GetSucessor()+" ";
                    pesoTotal += arestas[arestaVez].GetPeso();
                }
                arestaVez++;
            }
            resultado= "peso total: "+ pesoTotal + "\n" + resultado;
            return resultado;
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



        public string CondicaoSuficienteHamiltoniano()
        {
            //se não for forte, não há ciclo hamiltoniano.
         
            GrafoAuxFleury grafoAux = new GrafoAuxFleury(GetQuantVertices(), GetArestas());
            if (!FortementeConexo(grafoAux))
            {
                return "não é";
            }
           
            foreach(Vertice v in grafoAux.Vertices())
            {
                if(_armazenamento.GetGrauSaida(v.GetNumero()) == 0 || _armazenamento.GetGrauEntrada(v.GetNumero()) == 0)
                {
                    return "não é";
                }
            }
            // entrada + saida >= n: é (mas pode ser e nao atender a isso - suficiente mas nao necessario)
            bool valeTodos = true;
            foreach(Vertice v in grafoAux.Vertices())
            {
                if (_armazenamento.GetGrauSaida(v.GetNumero()) + _armazenamento.GetGrauEntrada(v.GetNumero()) < GetQuantVertices())
                {
                    valeTodos = false;
                }
            }

            if (valeTodos)
                return "é";
            else
            {
                return "pode ser";
            }

        }


        public string WelshPowell()
        {
            List<Vertice> ordenados = _armazenamento.GetVerticesND().OrderByDescending(v => v.GetGrau()).ToList();
            string resultado = "";

            List<int> cores = new List<int>();
            cores.Add(1);
            int corAtual = cores[0];
            ordenados[0].Colorir(corAtual);
            int c = 1;
            while (HaVerticesNaoColoridos(ordenados))
            {
                for (int i = 1; i < ordenados.Count; i++)
                {
                    Vertice vAtual = ordenados[i];
                    if (!vAtual.Colorido())
                    {
                        List<Aresta> arestas = vAtual.GetArestas();
                        bool pode = true;
                        foreach (Aresta aresta in arestas)
                        {

                            Vertice sucessor = new Vertice(9999);
                            foreach (Vertice v in ordenados) {
                                if (v.GetNumero() == aresta.GetSucessor()) {
                                    sucessor = v;
                                }

                            }
                            if (sucessor.GetCor()==corAtual)
                            {
                                pode=false;
                                break;
                            }
                        }
                        if(pode)
                            vAtual.Colorir(corAtual);
                    }
                }

                c++;
                cores.Add(c);
                corAtual = cores[c-1];
            }
            cores.RemoveAt(cores.Count-1);
            resultado += "quantidade de turnos: " + cores.Count+ "\n";
            foreach (int turno in cores) {
                resultado += $"turno {turno}: ";
                foreach (Vertice v in ordenados)
                {
                    if (v.GetCor() == turno)
                    {
                        resultado += $"{v.GetNumero()} ";
                    }
                }
                resultado += "\n";
            }
            return resultado;
        }

        private bool HaVerticesNaoColoridos(List<Vertice> vertices)
        {
            foreach(Vertice v in vertices)
            {
                if (!v.Colorido())
                {
                    return true;
                }
            }
            return false;
        }


        // --------------- sugestão deepseek
        public bool EhHamiltoniano(out List<int> cicloHamiltoniano)
        {
            cicloHamiltoniano = new List<int>();


            // Grau mínimo de entrada e saída deve ser pelo menos n/2 para garantir hamiltoniano (Teorema de Dirac)
            // Mas para um algoritmo exato, vamos usar backtracking

            int n = GetQuantVertices();

            // Inicia o array para armazenar o caminho
            int[] caminho = new int[n];
            for (int i = 0; i < n; i++)
                caminho[i] = -1;

            // Começa do vértice 1
            caminho[0] = 1;

            // Array para marcar vértices já visitados
            bool[] visitado = new bool[n + 1];
            visitado[1] = true;

            // Tenta completar o ciclo começando do vértice 1
            if (!BuscarCicloHamiltoniano(1, caminho, visitado, 1))
            {
                cicloHamiltoniano = null;
                return false;
            }

            // Converte o array para lista
            cicloHamiltoniano = new List<int>(caminho);
            // Adiciona o primeiro vértice no final para formar ciclo completo
            cicloHamiltoniano.Add(caminho[0]);

            return true;
        }

        private bool BuscarCicloHamiltoniano(int pos, int[] caminho, bool[] visitado, int contador)
        {
            int n = GetQuantVertices();

            // Se todos os vértices foram incluídos no caminho
            if (contador == n)
            {
                // Verifica se há aresta do último vértice para o primeiro
                int ultimoVertice = caminho[pos - 1];
                int primeiroVertice = caminho[0];

                // Verifica se existe aresta do último para o primeiro
                List<Aresta> arestasUltimo = _armazenamento.GetArestasIncidentes(ultimoVertice);
                foreach (Aresta a in arestasUltimo)
                {
                    if (a.GetSucessor() == primeiroVertice)
                    {
                        return true;
                    }
                }
                return false;
            }

            // Tenta todos os vértices como próximo candidato
            for (int v = 1; v <= n; v++)
            {
                // Verifica se pode adicionar v ao caminho
                if (PodeAdicionarAoHamiltoniano(caminho[pos - 1], v, visitado))
                {
                    caminho[pos] = v;
                    visitado[v] = true;

                    // Recursão
                    if (BuscarCicloHamiltoniano(pos + 1, caminho, visitado, contador + 1))
                        return true;

                    // Backtracking
                    caminho[pos] = -1;
                    visitado[v] = false;
                }
            }

            return false;
        }

        private bool PodeAdicionarAoHamiltoniano(int atual, int proximo, bool[] visitado)
        {
            // Se o vértice já foi visitado, não pode
            if (visitado[proximo])
                return false;

            // Verifica se existe aresta de 'atual' para 'proximo'
            List<Aresta> arestasAtual = _armazenamento.GetArestasIncidentes(atual);
            foreach (Aresta a in arestasAtual)
            {
                if (a.GetSucessor() == proximo)
                    return true;
            }

            return false;
        }

        public string VerificarEExibirHamiltoniano()
        {
            List<int> ciclo;

            if (EhHamiltoniano(out ciclo))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("O grafo É hamiltoniano!");
                sb.AppendLine("Ciclo Hamiltoniano encontrado:");

                for (int i = 0; i < ciclo.Count - 1; i++)
                {
                    sb.Append(ciclo[i] + " → ");
                }
                sb.AppendLine(ciclo[ciclo.Count - 1].ToString());

                return sb.ToString();
            }
            else
            {
                return "O grafo NÃO é hamiltoniano.";
            }
        }
    }
}