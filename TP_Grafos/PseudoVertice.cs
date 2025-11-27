using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class PseudoVertice
    {
        public LinkedList<PseudoAresta> _arestas;

        public PseudoVertice() { 
            _arestas = new LinkedList<PseudoAresta>();
        }
        public void Add(PseudoAresta p) { 
            _arestas.AddLast(p);
        }
    }
}
