public class ListarDespesas
{
static void ListarDespesasCall()
{
    Console.WriteLine(@"-------------------------------
                        ------------Despesas---------
                        -------------------------------");
    foreach (var conta in contas)
    {
        Console.WriteLine($"\n--- Despesas da Conta: {conta.Nome} ---");
        var despesas = conta.Transacoes.Where(t => t.Tipo == "Despesa");
        if (!despesas.Any())
        {
            Console.WriteLine("Nenhuma despesa encontrada para esta conta.");
        }
        else
        {
            foreach (var despesa in despesas)
            {
                Console.WriteLine($"{despesa.Data.ToShortDateString()} | {despesa.Descricao} | -{despesa.Valor:C}");
            }
        }
    }
}
}