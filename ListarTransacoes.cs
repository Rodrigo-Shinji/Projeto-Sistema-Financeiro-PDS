class ListarTransacoes { 
static void ListarTransacoesCall()
{
    Console.WriteLine(@"-------------------------------
                        ------------Transações---------
                        -------------------------------");
    foreach (var conta in contas)
    {
        Console.WriteLine($"\n--- Transações da Conta: {conta.Nome} ---");
        if (conta.Transacoes.Contador == 0)
        {
            Console.WriteLine("Não foi encontrada nenhuma transação.");
        }
        else
        {
            foreach (var transacao in conta.Transacoes)
            {
                Console.WriteLine($"{transacao.Data.ToShortDateString()} | {transacao.Tipo} | {transacao.Descricao} | {(transacao.Tipo == "Despesa" ? "-" : "+")}{transacao.Valor:C}");
            }
        }
    }
}
}