using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Aresta
    {
        private int _sucessor;
        private int _antecessor;
        private int _peso;
        private int _capacidade;

        private string _tipo; // busca em prof.

        public Aresta(int antecessor, int sucessor, int peso, int capacidade)
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

        public override string ToString()
        {
            return $"v:{_sucessor}, w:{_antecessor}, peso:{_peso}, capacidade:{_capacidade}";
        }

        public void DefinirTipo(string tipo)
        {
            _tipo = tipo;
        }
        public string GetTipo()
        {
            return _tipo;
        }
    }
}