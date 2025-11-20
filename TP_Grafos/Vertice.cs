using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Vertice
    {
        int _numero; // ex.: vertice 1, vertice 2...
        int _id; // caso precise
        static int _proxId = 1;

        int _distancia;     //bellmanford
        Vertice _predecessor; //bellmanford

        public Vertice(int num)
        {
            _id = _proxId;
            _proxId++;
            _numero = num;
        }

        public int GetNumero()
        {
            return _numero;
        }

        public void SetDistancia(int distancia)
        {
            _distancia = distancia;
        }
        public int GetDistancia()
        {
            return _distancia;
        }

        public void SetPredecessor(Vertice predecessor)
        {
            _predecessor = predecessor;
        }
      
    }
}
