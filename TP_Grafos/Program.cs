namespace TP_Grafos
{
    internal class Program
    {
        static void ExibirMenu()
        {
            Console.WriteLine("\n\t\t\t--- MÁXIMA LOGÍSTICA S.A. ---\n");
            Console.WriteLine("\tDigite o número correspondente à opção desejada:");
            Console.WriteLine("1- Roteamento de menor custo\n2- Capacidade Máxima de Escoamento\n3- Expansão da Rede de Comunicação\n4- Agendamento de Manutenções sem Conflito\n5- Rota Única de Inspeção\n6- Sair\n");
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

            // arquivo de logs que sera usado para escrever os resultados depois
            StreamWriter arquivoLog = new StreamWriter("log.txt", false);

            do
            {
                ExibirMenu();
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        int i = 1;
                        foreach (GrafoDirecionado rede in redes)
                        {
                            Console.WriteLine($"\n\tGRAFO {i}\n");
                            int origem, destino;
                            Console.WriteLine("Informe o hub de origem: ");
                            origem = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o hub de destino: ");
                            destino = int.Parse(Console.ReadLine());

                            int distancia = rede.DijkstraEntre(origem, destino);
                            Console.WriteLine($"A distância entre os hubs {origem} e {destino} é: {distancia}.");
                            i++;
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        int y = 1;
                        foreach(GrafoDirecionado rede in redes)
                        {
                            Console.WriteLine($"\n\tGRAFO {y}\n");
                            Console.WriteLine("Possível rota: \n");
                            Agm agm = rede.Prim();
                            agm.Imprimir();
                            y++;
                        }
                       
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
