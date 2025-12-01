namespace TP_Grafos
{
    internal class Program
    {
        static void ExibirMenu()
        {
            Console.WriteLine("\n\t\t\t--- MÁXIMA LOGÍSTICA S.A. ---\n");
            Console.WriteLine("\tDigite o número correspondente à opção desejada:");
            Console.WriteLine("1- Roteamento de menor custo\n2- Capacidade Máxima de Escoamento\n3- Expansão da Rede de Comunicação\n4- Agendamento de Manutenções sem Conflito\n5- Rota Única de Inspeção\n6- Sair\n7-Adicionar Hubs ou Rotas numa rede\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo a ser lido para gerar o grafo.</param>
        /// <returns></returns>
        static GrafoDirecionado CriarRede(string nomeArquivo)
        {
            StreamReader arq = new StreamReader(nomeArquivo, false);
            GrafoDirecionado rede = new GrafoDirecionado(arq);
            arq.Close();
            return rede;
        }
        static void Main(string[] args)
        {
            GrafoDirecionado rede1 = CriarRede("grafo01.txt");
            GrafoDirecionado rede2 = CriarRede("grafo02.txt");
            GrafoDirecionado rede3 = CriarRede("grafo03.txt");
            GrafoDirecionado rede4 = CriarRede("grafo04.txt");
            GrafoDirecionado rede5 = CriarRede("grafo05.txt");
            GrafoDirecionado rede6 = CriarRede("grafo06.txt");
            GrafoDirecionado rede7 = CriarRede("grafo07.txt");

            List<GrafoDirecionado> redes = new List<GrafoDirecionado>(); //lista de grafos
            redes.Add(rede1); redes.Add(rede2); redes.Add(rede3); redes.Add(rede4); redes.Add(rede5);
            redes.Add(rede6); redes.Add(rede7);
            int quantGrafos = redes.Count;

            int opc;

            StreamWriter arquivoLog = new StreamWriter("log.txt", false);
            arquivoLog.WriteLine($"\t\t-->Execução - {DateTime.Now}\n\n");
            do
            {
                ExibirMenu();
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        arquivoLog.WriteLine("\n----- Roteamento de menor custo -----\n");
                        int i = 1;
                        foreach (GrafoDirecionado rede in redes)
                        {
                            Console.WriteLine($"\n\tGRAFO {i}\n");
                            arquivoLog.WriteLine($"\n\tGRAFO {i}\n");
                            int origem, destino;
                            Console.WriteLine("Informe o hub de origem: ");
                            origem = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o hub de destino: ");
                            destino = int.Parse(Console.ReadLine());

                            int[,] distancias = rede.DijkstraEntre(origem, destino);
                            int distancia = distancias[0, destino-1];
                            Stack<int> rotaEncontrada = new Stack<int>();
                            rotaEncontrada.Push(destino);
                            int atual = distancias[1,destino-1];
                            while (atual != origem)
                            {
                                rotaEncontrada.Push(atual);
                                atual = distancias[1,atual-1];
                            }
                            rotaEncontrada.Push(origem);
                            for (int x =0; x<rotaEncontrada.Count; x++)
                            {
                                int hubAtual = rotaEncontrada.Pop();
                                Console.Write($"Hub {hubAtual} -> ");
                                arquivoLog.Write($"Hub {hubAtual} -> ");
                            }
                            Console.WriteLine($"Hub {destino}.");
                            arquivoLog.WriteLine($"Hub {destino}.");
                            Console.WriteLine($"A distância entre os hubs {origem} e {destino} é: {distancia}.");
                            arquivoLog.WriteLine($"A distância entre os hubs {origem} e {destino} é: {distancia}.");
                            i++;
                        }
                        arquivoLog.WriteLine("--------------------\n");
                        break;
                    case 2:
                        arquivoLog.WriteLine("\n----- Capacidade Máxima de Escoamento -----\n");
                        i = 1;
                        foreach (GrafoDirecionado rede in redes)
                        {
                            Console.WriteLine($"\n\tGRAFO {i}\n");
                            arquivoLog.WriteLine($"\n\tGRAFO {i}\n");
                            int s, t;
                            Console.WriteLine("Informe o hub de origem: ");
                            s = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o hub de destino: ");
                            t = int.Parse(Console.ReadLine());
                            i++;

                            int fluxomaximo = rede.DinicEntre(s, t);
                            Console.WriteLine($"O fluxo máximo entre {s} e {t} é {fluxomaximo}");
                            arquivoLog.WriteLine($"O fluxo máximo entre {s} e {t} é {fluxomaximo}");
                        }
                        arquivoLog.WriteLine("--------------------\n");
                        break;
                    case 3:  //ver de mudar pra kruskal; problema do Prim
                        arquivoLog.WriteLine("\n----- Expansão da Rede de Comunicação -----\n");
                        i = 1;
                        foreach(GrafoDirecionado rede in redes)
                        {
                            arquivoLog.WriteLine($"\n\tGRAFO {i}\n");
                            arquivoLog.WriteLine("Possível rota: \n");
                            Console.WriteLine($"\n\tGRAFO {i}\n");
                            Console.WriteLine("Possível rota: \n");
                            AgmK agm = rede.AGM_Kruskal();
                            Console.WriteLine(agm.Imprimir());
                            arquivoLog.WriteLine(agm.Imprimir());
                            i++;
                        }
                        arquivoLog.WriteLine("--------------------\n");
                        break;
                    case 4: // coloração - welsh powell
                        break;
                    case 5: // euleriano: fleury
                        break;
                    case 6:
                        Console.WriteLine("O programa será encerrado.");
                        break;
                    case 7:
                        GrafoDirecionado redeModificar = CriarRede("redeTesteMudar.txt");

                        Console.WriteLine("Adicionar vértices? (s/n)");
                        char resp = char.Parse(Console.ReadLine().ToLower());
                        if(resp == 's')
                        {
                            Console.WriteLine("Quantos vértices adicionar?");
                            int quant = int.Parse(Console.ReadLine());
                            for (int y = 0; y < quant; y++)
                            {
                                redeModificar.AdicionarVertice();
                            }
                        }

                        Console.WriteLine("Adicionar arestas? (s/n)");
                        resp = char.Parse(Console.ReadLine().ToLower());
                        if (resp == 's')
                        {
                            Console.WriteLine("Quantas arestas adicionar?");
                            int quant = int.Parse(Console.ReadLine());
                            for (int y = 0; y < quant; y++)
                            {
                                Console.WriteLine("Informe o número do vértice V da aresta:");
                                int vertV = int.Parse(Console.ReadLine());
                                Console.WriteLine("Informe o número do vértice W da aresta:");
                                int vertW = int.Parse(Console.ReadLine());
                                Console.WriteLine("Informe o peso da aresta:");
                                int peso = int.Parse(Console.ReadLine());
                                Console.WriteLine("Informe a capacidade da aresta:");
                                int capacidade = int.Parse(Console.ReadLine());
                                redeModificar.AdicionarAresta(vertV, vertW, peso, capacidade);
                            }
                        }

                        Console.WriteLine($"Quant vertices grafo: {redeModificar.GetQuantVertices()}");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Escolha de 1 a 6.");
                        break;
                }

            } while (opc != 6);

            arquivoLog.Close();

        }
    }
}
