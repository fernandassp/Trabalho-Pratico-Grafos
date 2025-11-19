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

        static void LerDadosGrafo() // colocar retorno certo depois
        {
            // implementar para ler dados de um arquivo e a partir disso montar um grafo 
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
