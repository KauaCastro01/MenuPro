using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuPro
{
    public class funcoes
    {
        public bool caixaAberto { get; set; } = false;
        public static string nomeProduto { get; set; }
        public static string nomeParaExibir { get; set; }
        public static decimal valorProduto { get; set; }
        public decimal valorFinal { get; set; }
        public int quantidade { get; set; }
        List<decimal> valoresTotalProduto = new List<decimal>();

        //Açoes e Funçoes
        //Pesquisar Os Produtos
        public void pesquisarProduto(int i)
        {
            conexaoSQL cSql = new conexaoSQL();
            try
            {
                Console.WriteLine("Insira As Informações Do Produto, Abaixo:");
                Console.WriteLine("Caso Deseje Voltar Para O Menu Principal, Digite 'Sair'");
                Console.Write("\nNome Do Produto: ");
                nomeProduto = Console.ReadLine().ToLower();
                if (nomeProduto == "sair") { Program.menuPrincipal();  }
                if (nomeProduto.Length >= 1 && nomeProduto.Length <= 60)
                {
                    switch (i)
                    {
                        case 1: cSql.obterPrecoProduto(nomeProduto); inserirValores(); break;
                        case 2: cSql.efetueLogin(nomeProduto, "", 2); break;
                    }
                }
                else
                {
                    Console.WriteLine($"\n\aO Nome Do Produto Deve Possuir Entre 1 a 60 Caracteres!");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine($"\a\nErro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
            }
        }
        //Inserir As Quantidades Dos Produtos
        public void inserirValores()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MenuPro - Quantidade De Produto");
                    Console.WriteLine($"Produto Selecionado: {nomeParaExibir}");
                    Console.WriteLine($"Preço: {valorProduto}R$");
                    Console.Write("\nDigite A Quantidade Solicitada: ");
                    quantidade = int.Parse(Console.ReadLine());
                    if (quantidade >= 0 && quantidade <= 1000) { somarValores(); desejaAdicionarMais(); }
                    else
                    {
                        Console.WriteLine($"\a\nA Quantidade Permetida É De 1 a 1.000!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                    }
                }
                catch (Exception Erro)
                {
                    Console.WriteLine($"\a\nErro: {Erro.Message}");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                }
            }
        }
        //Soma Dos Valores Dos Produtos
        public void somarValores()
        {
            valoresTotalProduto.Add(valorProduto * quantidade);
            return;
        }
        //Adicionar Mais Produtos
        public void desejaAdicionarMais()
        {
            while (true)
            {
                try
                {
                    Console.Write("\nAdicionar Mais Itens[S||N]: ");
                    string opcao = Console.ReadLine().ToLower();
                    switch (opcao)
                    {
                        case "s": Console.Clear(); pesquisarProduto(1); break;
                        case "n": formaPagamento(); break;
                        default:
                            Console.WriteLine($"\a\nOpção Inválida!");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception Erro)
                {
                    Console.WriteLine($"\a\nErro: {Erro.Message}");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        //Forma De Pagamento
        public void formaPagamento()
        {
                foreach (var v in valoresTotalProduto)
                {
                    valorFinal += v;
                }
            valoresTotalProduto.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Forma De Pagamento\n");
                Console.WriteLine($"Total: {valorFinal}R$");
                Console.WriteLine("[1] Dinheiro");
                Console.WriteLine("[2] Pix");
                Console.WriteLine("[3] Credito");
                Console.WriteLine("[4] Debito");
                Console.WriteLine("[5] Outro");
                Console.WriteLine("[C] Cancelar");
                Console.Write("Digite Aqui: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "1": pagamentoDinheiro(); break;
                    case "2": case "3": case "4": case "5":
                        compraFinalizada();
                        break;
                    case "c": Program.menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        //Caso O Pagamento Seja Em Dinheiro
        public void pagamentoDinheiro()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Insira O Valor Entregue Pelo Cliente:");
                    Console.WriteLine("Caso Queria Adicionar Os Centavos Use ',' Exemplo: 4,20");
                    Console.Write("Digite O Valor: ");
                    decimal valor = decimal.Parse(Console.ReadLine());
                    if (valor >= 0 && valor <= 10000)
                    {
                        decimal valorTroco = valor - valorFinal;
                        if (valorTroco > 0) { Console.WriteLine($"Troco: {valorTroco}"); compraFinalizada();  }
                        if(valorTroco == 0){ Console.WriteLine("Valor Pago"); compraFinalizada(); }
                        else
                        {
                            Console.WriteLine($"\a\nDinheiro Faltando {valorTroco}");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\a\nOs Valores Permetido É De 0R$ a 10.000R$");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception Erro)
                {
                    Console.WriteLine($"\a\nErro: {Erro.Message}");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        //Compra Finalizada
        public void compraFinalizada()
        {
            while (true)
            {
                Console.Write("\nComrpa Finalizada[S||N]: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "s": valoresTotalProduto.Clear();
                        Console.WriteLine($"Compra Finalizada!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        Program.menuPrincipal(); 
                        break;
                    case "n": formaPagamento(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        //Pesquisar Os Produtos
        public void adicionarProduto(int i)
        {
            conexaoSQL cSql = new conexaoSQL();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MenuPro - Adicionar Novo Produto\n");
                    Console.WriteLine("Digite 'Sair', Para Retonar Para O Menu");
                    Console.Write("Insira O Nome Do Produto: ");
                    nomeProduto = Console.ReadLine().ToLower();
                    if (nomeProduto == "sair") { Program.menuPrincipal(); }
                    Console.Write("Insira O Valor Do Produto: ");
                    valorProduto = decimal.Parse(Console.ReadLine());
                    if (nomeProduto.Length >= 1 && nomeProduto.Length <= 60)
                    {
                        if (valorProduto > 0 && valorProduto <= 10000)
                        {
                            switch (i)
                            {
                                case 1:
                                    cSql.adicionarProduto(nomeProduto, valorProduto);
                                    break;
                                case 2:
                                    cSql.editarProduto(nomeProduto, valorProduto, nomeParaExibir);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\a\nValor Deve Ser De 1R$ a 10.000R$!");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\a\nO Nome Do Produto Deve Possuir Entre 1 a 60 Caracteres!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                    }
                }
                catch (Exception Erro)
                {
                    Console.WriteLine($"\a\nErro: {Erro.Message}");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                }
            }
        }
        
    }
}

