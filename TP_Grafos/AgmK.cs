using System.Text;
using TP_Grafos;

internal class AgmK
{
    private List<Vertice> _verticesT;
    private LinkedList<Aresta> _arestasT;
    private IArmazenamento _grafo;

    public AgmK()
    {
        _verticesT = new List<Vertice>();
        _arestasT = new LinkedList<Aresta>();
        this._grafo = new ListaAdjacencia();
    }
    public List<Vertice> GetVerticesT()
    {
        return _verticesT;
    }

    public LinkedList<Aresta> GetArestasT()
    {
        return _arestasT;
    }

    public void AddVertices(int verts)
    {
        for (int i = 0; i < verts; i++) {
        _verticesT.Add(new Vertice(i+1));
        }

    }

    public void AddAresta(Aresta aresta)
    {
        _arestasT.AddLast(aresta);
    }

    public bool ArestaNaoFazCiclo(Aresta a)
    {
        
        foreach (Vertice v in _verticesT)
        {
            _grafo.AddVertice();
        }
        foreach (Aresta aresta in _arestasT)
        {
            _grafo.AddAresta(aresta.GetAntecessor(),aresta.GetSucessor(),aresta.GetPeso(),aresta.GetCapacidade());
        }
        _grafo.AddAresta(a.GetAntecessor(), a.GetSucessor(), a.GetPeso(), a.GetCapacidade());

        _grafo.BuscarEmProfundidade();

        foreach (Aresta aresta in _grafo.GetArestas())
        {
            if (aresta.GetTipo() == "retorno")
                return false;
        }
        return true;
    }

    public int CustoTotal()
    {
        int soma = 0;
        foreach (Aresta aresta in _arestasT)
        {
            soma += aresta.GetPeso();
        }
        return soma;
    }

    public string Imprimir()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i<_verticesT.Count-1; i++)
        {
            sb.Append($"Hub {_verticesT[i].GetNumero()} -> ");
            sb.Append($"Rota ({_verticesT[i].GetNumero()}, {_verticesT[i + 1].GetNumero()}) -> ");
        }
        sb.Append($"Hub {_verticesT[_verticesT.Count - 1].GetNumero()}\n");

        sb.AppendLine("CUSTO TOTAL: " + CustoTotal());

        sb.AppendLine();
        return sb.ToString();
    }
}