using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GestaoTarefas
{
    // Definição da classe para representar uma transação financeira (receita ou despesa)
    class Transacao
    {
        public string Descricao { get; set; } // Descrição da transação
        public decimal Valor { get; set; }     // Valor da transação
        public DateTime Data { get; set; }    // Data da transação
        public string Tipo { get; set; }     // Tipo da transação: "Receita" ou "Despesa"
        public string Conta { get; set; }    // Conta à qual a transação está associada
    }

    // Definição da classe para representar uma conta bancária ou carteira
    class Conta
    {
        public string Nome { get; set; }       // Nome da conta
        public decimal Saldo { get; set; }      // Saldo atual da conta
        public List<Transacao> Transacoes { get; set; } // Lista de transações associadas à conta

        public Conta(string nome)
        {
            Nome = nome;
            Saldo = 0;
            Transacoes = new List<Transacao>();
        }
    }

    class Program
    {
        // Método principal que executa o sistema
        static void Main(string[] args)
        {
            // Define a cultura para pt-BR para garantir a formatação correta de números e datas
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

            string opcao = "0";
            // Dicionário para armazenar as contas usando o nome da conta como chave
            Dictionary<string, Conta> contas = new Dictionary<string, Conta>();

            string menu = @"
                1 - Cadastrar nova conta
                2 - Cadastrar despesa
                3 - Cadastrar receita
                4.1 - Exibir o saldo total
                4.2 - Exibir o saldo por conta
                5.1 - Listar transações
                5.2 - Listar receitas
                5.3 - Listar despesas
                6.1 - Listar transações por mês
                6.2 - Listar receitas por mês
                6.3 - Listar despesas por mês
                0 - Sair";

            do
            {
                Console.WriteLine(menu);
                Console.Write("Digite a opção desejada: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarNovaConta(contas); // Chama o método para cadastrar uma nova conta
                        break;
                    case "2":
                        CadastrarTransacao(contas, "Despesa"); // Chama o método para cadastrar uma despesa
                        break;
                    case "3":
                        CadastrarTransacao(contas, "Receita"); // Chama o método para cadastrar uma receita
                        break;
                    case "4.1":
                        ExibirSaldoTotal(contas); // Chama o método para exibir o saldo total de todas as contas
                        break;
                    case "4.2":
                        ExibirSaldoPorConta(contas); // Chama o método para exibir o saldo de cada conta individualmente
                        break;
                    case "5.1":
                        ListarTransacoes(contas); // Chama o método para listar todas as transações de todas as contas
                        break;
                    case "5.2":
                        ListarTransacoes(contas, "Receita"); // Chama o método para listar apenas as receitas
                        break;
                    case "5.3":
                        ListarTransacoes(contas, "Despesa"); // Chama o método para listar apenas as despesas
                        break;
                    case "6.1":
                        ListarTransacoesPorMes(contas); // Chama o método para listar todas as transações por mês
                        break;
                    case "6.2":
                        ListarTransacoesPorMes(contas, "Receita"); // Chama o método para listar as receitas por mês
                        break;
                    case "6.3":
                        ListarTransacoesPorMes(contas, "Despesa"); // Chama o método para listar as despesas por mês
                        break;
                    case "0":
                        Console.WriteLine("Saindo do sistema...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine(); // Adiciona uma linha em branco para melhorar a legibilidade
            } while (opcao != "0");
        }

        // Método para cadastrar uma nova conta
        static void CadastrarNovaConta(Dictionary<string, Conta> contas)
        {
            Console.Write("Insira o nome da conta: ");
            string nomeConta = Console.ReadLine();

            if (contas.ContainsKey(nomeConta))
            {
                Console.WriteLine("Erro: Já existe uma conta com este nome.");
            }
            else
            {
                contas[nomeConta] = new Conta(nomeConta); // Cria um novo objeto Conta e adiciona ao dicionário
                Console.WriteLine("Conta cadastrada com sucesso!");
            }
        }

        // Método para cadastrar uma transação (receita ou despesa)
        static void CadastrarTransacao(Dictionary<string, Conta> contas, string tipo)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas. Cadastre uma conta primeiro.");
                return;
            }

            Console.WriteLine($"Contas disponíveis para cadastrar {tipo.ToLower()}:");
            // Exibe as contas com índices
            List<string> nomesContas = contas.Keys.ToList();
            for (int i = 0; i < nomesContas.Count; i++)
            {
                Console.WriteLine($"[{i}] - {nomesContas[i]}");
            }

            int indiceConta = -1;
            bool indiceValido = false;
            while (!indiceValido)
            {
                Console.Write("Digite o número da conta: ");
                string input = Console.ReadLine();
                indiceValido = int.TryParse(input, out indiceConta) && indiceConta >= 0 && indiceConta < nomesContas.Count;
                if (!indiceValido)
                {
                    Console.WriteLine("Índice inválido. Digite um número da lista.");
                }
            }
            string nomeContaSelecionada = nomesContas[indiceConta];

            Console.Write($"Insira a descrição da {tipo.ToLower()}: ");
            string descricao = Console.ReadLine();

            decimal valor = 0;
            bool valorValido = false;
            while (!valorValido)
            {
                Console.Write($"Insira o valor da {tipo.ToLower()}: ");
                string valorInput = Console.ReadLine();
                valorValido = decimal.TryParse(valorInput, out valor);
                if (!valorValido)
                {
                    Console.WriteLine("Valor inválido. Use o formato numérico correto (ex: 100,00).");
                }
            }

            DateTime data = DateTime.Now;
            bool dataValida = false;
            while (!dataValida)
            {
                Console.Write($"Insira a data da {tipo.ToLower()} (dd/mm/aaaa): ");
                string dataInput = Console.ReadLine();
                dataValida = DateTime.TryParseExact(dataInput, "dd/MM/yyyy", null, DateTimeStyles.None, out data);
                if (!dataValida)
                {
                    Console.WriteLine("Data inválida. Use o formato dd/mm/aaaa.");
                }
            }

            // Adiciona a transação à lista de transações da conta
            Transacao transacao = new Transacao
            {
                Descricao = descricao,
                Valor = valor,
                Data = data,
                Tipo = tipo,
                Conta = nomeContaSelecionada
            };

            contas[nomeContaSelecionada].Transacoes.Add(transacao);

            // Atualiza o saldo da conta
            if (tipo == "Receita")
            {
                contas[nomeContaSelecionada].Saldo += valor;
            }
            else // Despesa
            {
                contas[nomeContaSelecionada].Saldo -= valor;
            }

            Console.WriteLine($"{tipo} cadastrada com sucesso!");
        }

        // Método para exibir o saldo total de todas as contas
        static void ExibirSaldoTotal(Dictionary<string, Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas.");
                return;
            }

            decimal saldoTotal = 0;
            foreach (var conta in contas.Values)
            {
                saldoTotal += conta.Saldo;
            }
            Console.WriteLine($"Saldo total: {saldoTotal:C}"); // Formata o saldo como moeda
        }

        // Método para exibir o saldo de cada conta individualmente
        static void ExibirSaldoPorConta(Dictionary<string, Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas.");
                return;
            }

            foreach (var conta in contas)
            {
                Console.WriteLine($"Conta: {conta.Key}, Saldo: {conta.Value.Saldo:C}"); // Formata o saldo como moeda
            }
        }

        // Método para listar as transações (receitas e/ou despesas)
        static void ListarTransacoes(Dictionary<string, Conta> contas, string tipo = null)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas.");
                return;
            }

            bool encontrouTransacao = false; // Variável para verificar se alguma transação foi encontrada

            foreach (var conta in contas.Values)
            {
                Console.WriteLine($"\nConta: {conta.Nome}");
                if (conta.Transacoes.Count == 0)
                {
                    Console.WriteLine("Não há transações para esta conta.");
                    continue; // Vai para a próxima conta
                }

                foreach (var transacao in conta.Transacoes)
                {
                    // Se tipo for null, lista todas as transações.  Caso contrário, filtra por tipo.
                    if (tipo == null || transacao.Tipo == tipo)
                    {
                        Console.WriteLine($"  Descrição: {transacao.Descricao}, Valor: {transacao.Valor:C}, Data: {transacao.Data:dd/MM/yyyy}, Tipo: {transacao.Tipo}");
                        encontrouTransacao = true; // Marca que uma transação foi encontrada
                    }
                }
            }
            if (!encontrouTransacao && tipo != null)
            {
                Console.WriteLine($"Não há transações do tipo {tipo.ToLower()}.");
            }
        }

        // Método para listar transações por mês
        static void ListarTransacoesPorMes(Dictionary<string, Conta> contas, string tipo = null)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas.");
                return;
            }

            Console.Write("Digite o mês (1 a 12): ");
            if (!int.TryParse(Console.ReadLine(), out int mes) || mes < 1 || mes > 12)
            {
                Console.WriteLine("Mês inválido.");
                return;
            }

            Console.Write("Digite o ano: ");
            if (!int.TryParse(Console.ReadLine(), out int ano))
            {
                Console.WriteLine("Ano inválido.");
                return;
            }

            bool encontrouTransacao = false;
            foreach (var conta in contas.Values)
            {
                Console.WriteLine($"\nConta: {conta.Nome}");
                foreach (var transacao in conta.Transacoes)
                {
                    // Filtra por mês, ano e tipo
                    if (transacao.Data.Month == mes && transacao.Data.Year == ano && (tipo == null || transacao.Tipo == tipo))
                    {
                        Console.WriteLine($"  Descrição: {transacao.Descricao}, Valor: {transacao.Valor:C}, Data: {transacao.Data:dd/MM/yyyy}, Tipo: {transacao.Tipo}");
                        encontrouTransacao = true;
                    }
                }
            }
            if (!encontrouTransacao)
            {
                Console.WriteLine($"Não há transações para o mês {mes}/{ano}.");
            }
        }
    }
}
