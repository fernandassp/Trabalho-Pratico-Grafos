using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class PseudoAresta
    {
        int _sucessor;
        int _antecessor;
        int _peso;
        int _capacidade;

        public PseudoAresta(int antecessor, int sucessor, int peso, int capacidade)
        {
            _antecessor = antecessor;
            _sucessor = sucessor;
            _peso = peso;
            _capacidade = capacidade;
        }
        public int GetSucessor() { return _sucessor; }

        public int GetAntecessor() { return _antecessor; }
        public int GetPeso() { return _peso; }
        public int GetCapacidade() { return _capacidade; }
    }
}