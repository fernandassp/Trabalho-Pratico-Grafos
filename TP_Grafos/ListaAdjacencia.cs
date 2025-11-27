using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class ListaAdjacencia : IArmazenamento //esparso
    {
        List<PseudoVertice> _lista;

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
                _lista.Add(new PseudoVertice(i+1));
            }
            while (linha != null)
            {
                _lista[int.Parse(valores[0])].AddAresta(new PseudoAresta(int.Parse(valores[0]), int.Parse(valores[1]), int.Parse(valores[2]), int.Parse(valores[3])));
                linha = arq.ReadLine();
            }
        }
        public void AdicionarVertice()
        {
            _lista.Add(new PseudoVertice(_lista.Count));
        }
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade)
        {
            _lista[vertA].AddAresta(new PseudoAresta(vertA,vertB,peso,capacidade)); 
        }
        public int GetPeso(int vertA, int vertB) {
            foreach (PseudoAresta a in _lista[vertA-1].GetArestas())
            {
                if (_lista[a.GetSucessor()].GetNumero()-1==vertB) { 
                    return a.GetPeso();
                }
            }
            return -1;
        }
        public int GetCapacidade(int vertA, int vertB) {
            foreach (PseudoAresta a in _lista[vertA - 1].GetArestas())
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

        public List<PseudoAresta> GetArestas()
        {
            List<PseudoAresta> arestas = new List<PseudoAresta>();
            foreach (PseudoVertice v in _lista) {
                foreach (PseudoAresta a in v.GetArestas()) 
                {
                    arestas.Add(a);
                }
            }
            return arestas;
        }

        public LinkedList<PseudoAresta> GetArestasIncidentes(int numVertice)
        {
            return _lista[numVertice - 1].GetArestas();
        }
        public void Mostarlista() { 
            foreach (PseudoVertice v in _lista)
            {
                Console.Write(v.GetNumero()+": ");
                foreach(PseudoAresta a in v.GetArestas())
                {
                    Console.Write(a.GetSucessor()+", ");
                }
                Console.WriteLine();
            }
        }
    }
}
