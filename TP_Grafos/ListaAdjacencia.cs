using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class ListaAdjacencia : IArmazenamento //esparso
    {
        List<Vertice> _lista;

        static int TempoGlobal;

        public List<Aresta> GetArestasND() // MUDAR
        {
            return new List<Aresta>();
        }
        public void BuscarEmProfundidade()
        {
            TempoGlobal = 0;

            int[,] resultados = new int[3, GetQuantVertices()]; // -1 pra null
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                resultados[2, i] = -1;
            }

            // p/ cada vértice v, se seu TD = 0, chama BuscaProfundidade(v)
            for (int i = 0; i < resultados.GetLength(1); i++)
            {
                if (resultados[0, i] == 0)
                    BuscaProfundidade(i + 1, resultados);
            }
        }
        private void BuscaProfundidade(int vertice, int[,] resultados)
        {
            TempoGlobal++;
            resultados[0, vertice - 1] = TempoGlobal;

            LinkedList<Aresta> arestasIncidentes = GetArestasIncidentes(vertice);

            foreach (Aresta a in arestasIncidentes)
            {
                if (resultados[0, a.GetSucessor() - 1] == 0)
                {
                    a.DefinirTipo("arvore");
                    resultados[2, a.GetSucessor() - 1] = vertice;
                    BuscaProfundidade(a.GetSucessor(), resultados);
                }
                else
                {
                    if (resultados[1, a.GetSucessor() - 1] == 0)
                    {
                        a.DefinirTipo("retorno");
                    }
                    else if (resultados[0, vertice - 1] < resultados[0, a.GetSucessor() - 1])
                    {
                        a.DefinirTipo("avanco");
                    }
                    else
                    {
                        a.DefinirTipo("cruzamento");
                    }
                }
            }
            TempoGlobal++; resultados[1, vertice - 1] = TempoGlobal;
        }
        public ListaAdjacencia(StreamReader arq)
        {
            _lista = new List<Vertice>();
            CriarLista(arq);
        } 
        public ListaAdjacencia()
        {
            _lista = new List<Vertice>();
        }
        private void CriarLista(StreamReader arq)
        {
            string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            for (int i = 0; i < int.Parse(valores[0]); i++)
            {
                _lista.Add(new Vertice(i + 1));
            }
            linha = arq.ReadLine();
            while (linha != null)
            {
                valores = linha.Split(" ");
                Aresta novaAresta = new Aresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3]));
                _lista.ElementAt(int.Parse(valores[0]) - 1).AddAresta(novaAresta);
                linha = arq.ReadLine();
            }
            arq.Close();
        }
        public ListaAdjacencia(int quantVerts, List<Aresta> arestas)
        {
            Traduzir(quantVerts, arestas);
        }

        private void Traduzir(int quantVerts, List<Aresta> arestas)
        {
            _lista = new List<Vertice>();
            for (int i = 0; i < quantVerts; i++)
            {
                _lista.Add(new Vertice(i + 1));
            }
            foreach (Aresta a in arestas)
            {
                _lista[a.GetAntecessor() - 1].AddAresta(a);
            }
        }
        public void AddVertice()
        {
            _lista.Add(new Vertice(_lista.Count));
        }
        public void AddAresta(int vertA, int vertB, int peso, int capacidade)
        {
            _lista[vertA].AddAresta(new Aresta(vertA, vertB, peso, capacidade));
        }
        public int GetPeso(int vertA, int vertB)
        {
            foreach (Aresta a in _lista[vertA - 1].GetArestas())
            {
                if (_lista[a.GetSucessor()].GetNumero() - 1 == vertB)
                {
                    return a.GetPeso();
                }
            }
            return -1;
        }
        public int GetCapacidade(int vertA, int vertB)
        {
            foreach (Aresta a in _lista[vertA - 1].GetArestas())
            {
                if (_lista[a.GetSucessor()].GetNumero() - 1 == vertB)
                {
                    return a.GetCapacidade();
                }
            }
            return -1;
        }

        public int GetQuantVertices()
        {
            return _lista.Count;
        }
        public int GetQuantArestas()
        {
            int quant = 0;
            foreach (Vertice v in _lista)
            {
                quant += v.GetQuantArestas();
            }
            return quant;
        }

        public List<Aresta> GetArestas()
        {
            List<Aresta> arestas = new List<Aresta>();
            foreach (Vertice v in _lista)
            {
                foreach (Aresta a in v.GetArestas())
                {
                    arestas.Add(a);
                }
            }
            return arestas;
        }

        public LinkedList<Aresta> GetArestasIncidentes(int numVertice)
        {
            return _lista[numVertice - 1].GetArestas();
        }
        public void Mostrarlista()
        {
            foreach (Vertice v in _lista)
            {
                Console.Write(v.GetNumero() + ": ");
                foreach (Aresta a in v.GetArestas())
                {
                    Console.Write(a.GetSucessor() + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
