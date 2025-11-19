namespace TP_Grafos
{
    internal class Program
    {
        static void ExibirMenu()
        {
            Console.WriteLine("\t\t\t--- MÁXIMA LOGÍSTICA S.A. ---\n");
            Console.WriteLine("\tDigite o número correspondente à opção desejada:");
            Console.WriteLine("1- Roteamento de menor custo\n2- Capacidade Máxima de Escoamento\n3- Expansão da Rede de Comunicação\n4- Agendamento de Manutenções sem Conflito\n5- Rota Única de Inspeção\n6- Sair");
        }

        static GrafoDirecionado LerDadosGrafo(StreamReader arq) 
        {
            GrafoDirecionado grafo = new GrafoDirecionado();
            int numVertices, numArestas; string linha = arq.ReadLine();
            string[] valores = linha.Split(' ');
            numVertices = int.Parse(valores[0]);

            for(int i = 1; i<=numVertices; i++)
            {
                grafo.AdicionarVertice(new Vertice(i));
            }
            numArestas = int.Parse(valores[1]);
            linha = arq.ReadLine();
            for(int i = 0; i<numArestas; i++)
            {
                valores = linha.Split(" ");

                // 0: origem (v), 1: destino (w), 2: peso; 3: capacidade

                int origem = int.Parse(valores[0]);
                int destino = int.Parse(valores[1]);
                Aresta aresta = new Aresta(grafo.VerticeDeNumero(origem), grafo.VerticeDeNumero(destino));
                aresta.DefinirPeso(int.Parse(valores[2]));
                aresta.DefinirCapacidade(int.Parse(valores[3]));
                grafo.AdicionarAresta(aresta);

                linha = arq.ReadLine();

            }

            return grafo;
        }

        // método para calcular densidade fica aqui ou na classe grafo? outra?
        static void Main(string[] args)
        {
            //criar a estrutura de dados que armazena a rede
            int opc;

            // arquivo de logs que sera usado para escrever os resultados depois
            StreamWriter arquivoLog = new StreamWriter("log.txt", false);

            do
            {
                ExibirMenu();
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        // hubs vão ser ints, representando o número dos vértices? 
                        int origem, destino;
                        Console.WriteLine("Informe o hub de origem: ");
                        origem = int.Parse(Console.ReadLine());
                        Console.WriteLine("Informe o hub de destino: ");
                        destino = int.Parse(Console.ReadLine());

                        //chamar método que calcule (caminho mínimo)
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        Console.WriteLine("O programa será encerrado.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Escolha de 1 a 6.");
                        break;
                }

            } while (opc != 6); 
          
        }
    }
}
