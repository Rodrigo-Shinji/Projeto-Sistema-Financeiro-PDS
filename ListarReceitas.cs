class ListarReceitas { 
static void ListarReceitasCall()
{
    Console.WriteLine(@"-------------------------------
                        ------------Receitas---------
                        -------------------------------");
    foreach (var conta in contas)
    {
        Console.WriteLine($"\n--- Receitas da Conta: {conta.Nome} ---");
        var receitas = conta.Transacoes.Where(t => t.Tipo == "Receita");
        if (!receitas.Any())
        {
            Console.WriteLine("Não foi encontrada nenhuma receita.");
        }
        else
        {
            foreach (var receita in receitas)
            {
                Console.WriteLine($"{receita.Data.ToShortDateString()} | {receita.Descricao} | +{receita.Valor:C}");
            }
        }
    }
}
}