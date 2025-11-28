using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class ArestaResidual
    {
        public int _para;
        public int _capacidade;
        public int _fluxo;
        public int _reverso; // Índice da aresta reversa na lista do vizinho

        public ArestaResidual(int para, int capacidade, int fluxo, int reverso)
        {
            _para = para;
            _capacidade = capacidade;
            _fluxo = fluxo;
            _reverso = reverso;
        }
        public int GetPara() { return _para; }
        public int GetCapacidade() { return _capacidade; }
        public int GetFluxo() { return _fluxo; }
        public void SetFluxo(int fluxo) { _fluxo = fluxo; }
        public int GetReverso() { return _reverso; }
    }
}
