using System.Text;
using TP_Grafos;

internal class AgmK
{
    private List<Vertice> _verticesT;
    private List<Aresta> _arestasT;
    private IArmazenamento _grafo;

    public AgmK()
    {
        _verticesT = new List<Vertice>();
        _arestasT = new List<Aresta>();
        this._grafo = new ListaAdjacencia();
    }
    public List<Vertice> GetVerticesT()
    {
        return _verticesT;
    }

    public List<Aresta> GetArestasT()
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
        _arestasT.Add(aresta);
    }

    public bool ArestaFazCiclo(Aresta a)
    {
        //_grafo = new ListaAdjacencia();
        GrafoAuxAGM grafoAux = new GrafoAuxAGM();


        foreach (Vertice v in _verticesT)
        {
            grafoAux.AddVertice(v);
        }
        foreach (Aresta aresta in _arestasT)
        {
            grafoAux.AddAresta(aresta);
        }
        grafoAux.AddAresta(a);

        grafoAux.BuscarEmProfundidade();

        foreach (Aresta aresta in grafoAux.Arestas())
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
        sb.Append("Rota possível: ");
        for (int i = 0; i<_arestasT.Count-1; i++)
        {
            sb.Append($"({_arestasT[i].GetAntecessor()}, {_arestasT[i].GetSucessor()})-> ");
        }
        sb.Append($"({_arestasT[_arestasT.Count-1].GetAntecessor()}, {_arestasT[_arestasT.Count-1].GetSucessor()})");
        sb.AppendLine();
        sb.AppendLine("CUSTO TOTAL: " + CustoTotal());

        sb.AppendLine();
        return sb.ToString();
    }

    public bool ContemAresta(Aresta aresta)
    {
        foreach(Aresta a in _arestasT)
        {
            if (a.GetSucessor() == aresta.GetSucessor() && a.GetAntecessor() == aresta.GetAntecessor() || a.GetSucessor() == aresta.GetAntecessor() && a.GetAntecessor() == aresta.GetSucessor())
                return true;
        }
        return false;
    }
}