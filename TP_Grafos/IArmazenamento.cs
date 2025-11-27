using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    interface IArmazenamento
    {
        public void AdicionarVertice();
        public void AdicionarAresta(int vertA, int vertB, int peso, int capacidade);
        public int GetPeso(int vertA, int vertB); 
        public int GetCapacidade(int vertA, int vertB);
        public List<Aresta> GetArestas();

        public int GetQuantVertices();
        public LinkedList<Aresta> GetArestasIncidentes(int numVertice);
    }
}
