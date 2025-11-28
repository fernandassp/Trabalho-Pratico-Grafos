using TP_Grafos;

internal class AgmK
{
    private LinkedList<Vertice> _verticesT;
    private LinkedList<Aresta> _arestasT;
    private IArmazenamento _grafo;

    public AgmK()
    {
        _verticesT = new LinkedList<Vertice>();
        _arestasT = new LinkedList<Aresta>();
        this._grafo = new ListaAdjacencia();
    }
    public LinkedList<Vertice> GetVerticesT()
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
        _verticesT.AddLast(new Vertice(i+1));
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
}