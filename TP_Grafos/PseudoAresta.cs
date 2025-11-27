using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class PseudoAresta
    {
        PseudoVertice _sucessor;
        int _peso;
        int _capacidade;

        public PseudoAresta(PseudoVertice sucessor,int peso, int capacidade) { 
            _sucessor = sucessor;
            _peso = peso;
            _capacidade = capacidade;
        }
        public PseudoVertice GetPseudoVertice() { return _sucessor; }
        public int GetPeso() { return _peso; }
        public int GetCapacidade() { return _capacidade; }
    }
}
