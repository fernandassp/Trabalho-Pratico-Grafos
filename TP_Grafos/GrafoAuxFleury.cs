using System;
using System.Collections.Generic;

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
            _arestas = new List<Aresta>(arestas); // copia para contiuar com a original
            for (int i = 1; i <= quantV; i++)
            {
                _vertices.Add(new Vertice(i));
            }
        }

        public List<int> EncontrarCicloEuleriano()
        {
            // para todo vértice, grau entrada = grau saída.
            int[] grauEntrada = new int[_vertices.Count + 1];
            int[] grauSaida = new int[_vertices.Count + 1];

            foreach (Aresta a in _arestas)
            {
                grauSaida[a.GetAntecessor()]++;
                grauEntrada[a.GetSucessor()]++;
            }

            // se qualquer vértice desbalanceado, não existe ciclo euleriano.
            for (int i = 1; i <= _vertices.Count; i++)
            {
                if (grauSaida[i] != grauEntrada[i])
                {
                    return null;
                }
            }

            // procura um ponto de partida válido, qualquer vértice que tenha arestas
            int inicio = -1;
            for (int i = 1; i <= _vertices.Count; i++)
            {
                if (grauSaida[i] > 0)
                {
                    inicio = i;
                    break;
                }
            }

            if (inicio == -1) return null; // grafo sem arestas

            // execução do metodo de fleury
            List<Aresta> arestasRestantes = new List<Aresta>(_arestas);
            List<int> ciclo = new List<int>();
            int v = inicio;
            ciclo.Add(v);

            while (arestasRestantes.Count > 0)
            {
                // filtra arestas que saem do vértice atual 
                List<Aresta> incidentes = arestasRestantes.Where(a => a.GetAntecessor() == v).ToList();

                if (incidentes.Count == 0) return null; // beco sem saída inesperado (grafo desconexo)

                Aresta arestaEscolhida = null;
                int maxAlcance = -1;

                // escolhe a aresta que permite alcançar mais vértices no futuro
                foreach (Aresta candidata in incidentes)
                {
                    arestasRestantes.Remove(candidata); // tira temporariamente

                    // verifica alcance a partir do destino
                    int alcance = BuscaLarguraContarAlcancaveis(candidata.GetSucessor(), arestasRestantes);

                    if (alcance > maxAlcance)
                    {
                        maxAlcance = alcance;
                        arestaEscolhida = candidata;
                    }

                    arestasRestantes.Add(candidata); // coloca de volta
                }

                // tira a aresta para seguir com o metodo
                v = arestaEscolhida.GetSucessor();
                arestasRestantes.Remove(arestaEscolhida);
                ciclo.Add(v);
            }

            return ciclo;
        }

        // busca em largura para contar quantos vértices é possivel atingir a partir de do vertice de inicio
        private int BuscaLarguraContarAlcancaveis(int inicio, List<Aresta> arestasDisponiveis)
        {
            HashSet<int> visitados = new HashSet<int>();
            Queue<int> fila = new Queue<int>();

            // agrupa as arestas por origem para facilitar a busca 
            var adj = arestasDisponiveis
                .GroupBy(a => a.GetAntecessor())
                .ToDictionary(g => g.Key, g => g.Select(a => a.GetSucessor()).ToList());

            fila.Enqueue(inicio);
            visitados.Add(inicio);

            while (fila.Count > 0)
            {
                int atual = fila.Dequeue();
                if (adj.ContainsKey(atual))
                {
                    foreach (int vizinho in adj[atual])
                    {
                        if (!visitados.Contains(vizinho))
                        {
                            visitados.Add(vizinho);
                            fila.Enqueue(vizinho);
                        }
                    }
                }
            }
            return visitados.Count;
        }

        public List<int> BuscarEmProfundidade(bool inverter)
        {
            TempoGlobal = 0;
            List<int> visitados = new List<int>();

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