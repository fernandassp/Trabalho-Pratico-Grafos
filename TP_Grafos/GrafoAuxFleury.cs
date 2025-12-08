using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class GrafoAuxFleury
    {
        private List<Vertice> _vertices;
        private List<Aresta> _arestas;
        static int TempoGlobal;

        public GrafoAuxFleury(int quantV, List<Aresta> arestas)
        {
            _vertices = new List<Vertice>();
            _arestas = arestas;
            for (int i = 1; i <= quantV; i++)
            {
                _vertices.Add(new Vertice(i));
            }
        }

        // Método de Fleury seguindo o pseudocódigo
        public List<int> EncontrarCicloEuleriano(int verticeInicialParametro)
        {
            // 1. Se V(G) possuir 3 ou mais vértices de grau ímpar então PARE;
            // Calculando graus (considerando entrada + saída para definir "grau" no contexto do algoritmo)
            int[] graus = new int[_vertices.Count + 1];

            foreach (Aresta a in _arestas)
            {
                graus[a.GetAntecessor()] = graus[a.GetAntecessor()] + 1;
                graus[a.GetSucessor()] = graus[a.GetSucessor()] + 1;
            }

            int contagemImpares = 0;
            List<int> verticesImpares = new List<int>();

            for (int i = 1; i < graus.Length; i++)
            {
                if (graus[i] % 2 != 0)
                {
                    contagemImpares++;
                    verticesImpares.Add(i);
                }
            }

            if (contagemImpares >= 3)
            {
                return null; // PARE
            }

            // 2. Seja G’ = (V’, E’) tal que V’ = V(G)e E’ = E(G);
            List<Aresta> arestasRestantes = new List<Aresta>();
            foreach (Aresta a in _arestas)
            {
                arestasRestantes.Add(a);
            }

            // 3. Selecionar vértice inicial v = V’ (escolher v cujo grau seja ímpar, se houver)
            int v;
            if (verticesImpares.Count > 0)
            {
                v = verticesImpares[0];
            }
            else
            {
                v = _vertices[0].GetNumero();
            }

            List<int> ciclo = new List<int>();
            ciclo.Add(v);

            // 4. Enquanto E’ diferente de vazio efetuar
            while (arestasRestantes.Count > 0)
            {
                // Obter arestas que saem de v (antecessor == v)
                List<Aresta> incidentes = new List<Aresta>();
                foreach (Aresta a in arestasRestantes)
                {
                    if (a.GetAntecessor() == v)
                    {
                        incidentes.Add(a);
                    }
                }

                // Se não houver arestas saindo, mas a lista global não está vazia, o caminho travou.
                // Isso previne loop infinito se o grafo for desconexo ou direcionado incorretamente.
                if (incidentes.Count == 0)
                {
                    // Força a saída do laço alterando a condição do while indiretamente ou retornando o que tem
                    return ciclo;
                }

                Aresta arestaEscolhida = null;

                // a. se d(v) > 1 então Selecionar aresta {v,w} que não seja ponte em G’;
                if (incidentes.Count > 1)
                {
                    bool achouNaoPonte = false;

                    // Procura uma aresta que não seja ponte sem usar break
                    for (int k = 0; k < incidentes.Count; k++)
                    {
                        if (!achouNaoPonte)
                        {
                            Aresta candidata = incidentes[k];
                            bool ePonte = EhPonte(v, candidata, arestasRestantes);

                            if (!ePonte)
                            {
                                arestaEscolhida = candidata;
                                achouNaoPonte = true;
                            }
                        }
                    }

                    // Se todas forem pontes (ou não achou nenhuma não-ponte), pega a primeira
                    if (!achouNaoPonte)
                    {
                        arestaEscolhida = incidentes[0];
                    }
                }
                else
                {
                    // b. senão Selecionar a única aresta {v,w} disponível em G’;
                    arestaEscolhida = incidentes[0];
                }

                // c. v = w;
                int w = arestaEscolhida.GetSucessor();

                // E’ = E’ - {v,w};
                arestasRestantes.Remove(arestaEscolhida);

                v = w;
                ciclo.Add(v);
            }

            return ciclo;
        }

        private bool EhPonte(int u, Aresta arestaAvaliada, List<Aresta> arestasAtuais)
        {
            // 1. Conta acessíveis COM a aresta
            int acessiveisCom = ContarAcessiveisBFS(u, arestasAtuais);

            // 2. Remove temporariamente
            arestasAtuais.Remove(arestaAvaliada);

            // 3. Conta acessíveis SEM a aresta
            int acessiveisSem = ContarAcessiveisBFS(u, arestasAtuais);

            // 4. Adiciona de volta
            arestasAtuais.Add(arestaAvaliada);

            return acessiveisSem < acessiveisCom;
        }

        private int ContarAcessiveisBFS(int inicio, List<Aresta> arestasDisponiveis)
        {
            List<int> visitados = new List<int>();
            List<int> fila = new List<int>();

            fila.Add(inicio);
            visitados.Add(inicio);

            while (fila.Count > 0)
            {
                int atual = fila[0];
                fila.RemoveAt(0);

                foreach (Aresta a in arestasDisponiveis)
                {
                    // Considerando adjacência para fins de conectividade (grafo tratado como não direcionado para verificação de ponte)
                    int vizinho = -1;

                    if (a.GetAntecessor() == atual)
                    {
                        vizinho = a.GetSucessor();
                    }
                    else
                    {
                        if (a.GetSucessor() == atual)
                        {
                            vizinho = a.GetAntecessor();
                        }
                    }

                    if (vizinho != -1)
                    {
                        bool jaVisitado = false;
                        foreach (int vis in visitados)
                        {
                            if (vis == vizinho)
                            {
                                jaVisitado = true;
                            }
                        }

                        if (!jaVisitado)
                        {
                            visitados.Add(vizinho);
                            fila.Add(vizinho);
                        }
                    }
                }
            }
            return visitados.Count;
        }

        // --- MÉTODOS DE BUSCA EM PROFUNDIDADE JÁ EXISTENTES (Mantidos para compatibilidade) ---

        public List<int> BuscarEmProfundidade(bool inverter)
        {
            TempoGlobal = 0;
            List<int> visitados = new List<int>();

            // Lógica de inversão
            List<Aresta> arestasParaBusca = new List<Aresta>();
            foreach (Aresta a in _arestas)
            {
                if (inverter)
                {
                    arestasParaBusca.Add(new Aresta(a.GetSucessor(), a.GetAntecessor(), a.GetPeso(), a.GetCapacidade()));
                }
                else
                {
                    arestasParaBusca.Add(a);
                }
            }

            int[,] resultados = new int[3, _vertices.Count];
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[2, i] = -1;
            }

            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                if (resultados[0, i] == 0)
                {
                    BuscaProfundidade(i + 1, resultados, visitados, arestasParaBusca);
                }
            }

            return visitados;
        }

        private void BuscaProfundidade(int vertice, int[,] resultados, List<int> visitados, List<Aresta> arestasContexto)
        {
            TempoGlobal++;
            resultados[0, vertice - 1] = TempoGlobal;
            visitados.Add(vertice);

            List<Aresta> incidentes = new List<Aresta>();
            foreach (Aresta a in arestasContexto)
            {
                if (a.GetAntecessor() == vertice)
                {
                    incidentes.Add(a);
                }
            }

            foreach (Aresta a in incidentes)
            {
                int w = a.GetSucessor();
                if (resultados[0, w - 1] == 0)
                {
                    resultados[2, w - 1] = vertice;
                    BuscaProfundidade(w, resultados, visitados, arestasContexto);
                }
            }
            TempoGlobal++;
            resultados[1, vertice - 1] = TempoGlobal;
        }

        public List<Vertice> Vertices()
        {
            return _vertices;
        }
        public List<Aresta> Arestas()
        {
            return _arestas;
        }
    }
}