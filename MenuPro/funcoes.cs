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
        public static string nomeFuncionario { get; set; }
        public static string nomeParaExibir { get; set; }
        public static decimal valorProduto { get; set; }
        public decimal valorFinal { get; set; }
        public int quantidade { get; set; }
        List<decimal> valoresTotalProduto = new List<decimal>();

        //Açoes e Funçoes
        //Pesquisar Os Produtos
        public void pesquisarProduto(int i)
        {
            Console.WriteLine("\n");
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
                        case 3: cSql.efetueLogin(nomeProduto, "", 3); break;
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
            Console.WriteLine("\n");
            while (true)
            {
                try
                {
                    Console.Write("Adicionar Mais Itens[S||N]: ");
                    string opcao = Console.ReadLine().ToLower();
                    switch (opcao)
                    {
                        case "s": Program.menuVenda(); break;
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
            Console.WriteLine("\n");
            while (true)
            {
                try
                {
                    Console.WriteLine("Insira O Valor Entregue Pelo Cliente:");
                    Console.WriteLine("Caso Queria Adicionar Os Centavos Use ',' [Exemplo: 4,20]\n");
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
            lucroObtido lO = new lucroObtido();
            var data = DateTime.Now;
            while (true)
            {
                Console.Write("\nComrpa Finalizada[S||N]: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "s": 
                        Console.WriteLine($"Compra Finalizada!");
                        lO.salvarVenda(valorFinal, data);
                        Console.ReadKey();
                        valoresTotalProduto.Clear();
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
        //Pesquisar Os Produtos
        public void removerProduto()
        {
            Console.WriteLine("\n");
            conexaoSQL cSql = new conexaoSQL();
            while (true)
            {
                Console.WriteLine($"Produto Selecionado: {nomeParaExibir}");
                Console.Write("Você Deseaja Remover Este Produto[S||N]: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "s":
                        cSql.deletarProduto(nomeParaExibir);
                        break;
                    case "n": Program.menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        //
        public void inserirNovoFuncionario(int i)
        {
            sqlParaUsuario sqlUser = new sqlParaUsuario();
            string nome = "";
            string usuario = "";
            string senha = "";
            string nomeAntigo = nomeFuncionario;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MenuPro\n");
                    Console.WriteLine("Digite 'Sair', Para Retonar Para O Menu");
                    Console.Write("Insira O Nome Do Funcionario: ");
                    nome = Console.ReadLine();
                    if (nome.ToLower() == "sair") { Program.menuPrincipal(); }
                    if (nome.Length >= 1 && nome.Length <= 60)
                    {
                        Console.Write("Insira O Usuario Do Funcionario: ");
                        usuario = Console.ReadLine();
                        if (usuario.Length >= 1 && usuario.Length <= 20)
                        {
                            Console.Write("Insira A Senha Do Funcionario: ");
                            senha = Console.ReadLine();
                            if (senha.Length >= 1 && senha.Length <= 20)
                            {
                                switch (i)
                                {
                                    case 1: sqlUser.adicionarFuncionario(nome, usuario, senha); break;
                                    case 2: sqlUser.editarFuncionario(nome, usuario, senha, nomeAntigo); break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\a\nA Senha Do Funcionario Deve Possuir Entre 1 a 20 Caracteres!");
                                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\a\nO Usuario Do Funcionario Deve Possuir Entre 1 a 20 Caracteres!");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\a\nO Nome Do Funcionario Deve Possuir Entre 1 a 60 Caracteres!");
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

        public void editarFuncionario(int i)
        {
            sqlParaUsuario sqlUser = new sqlParaUsuario();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Caso Deseje Voltar Para O Menu Principal, Digite 'Sair'");
                    Console.Write("\nUsuario Do Funcionario: ");
                    nomeFuncionario = Console.ReadLine();
                    if (nomeFuncionario.ToLower() == "sair") { Program.menuPrincipal(); }
                    if (nomeFuncionario.Length >= 1 && nomeFuncionario.Length <= 20)
                    {
                        switch (i)
                        {
                            case 1: sqlUser.pesquisarFuncionario(nomeFuncionario); break;
                            case 2: removerFuncionario(); break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n\aO Nome Do Produto Deve Possuir Entre 1 a 20 Caracteres!");
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
        public void removerFuncionario()
        {
            sqlParaUsuario sU = new sqlParaUsuario();
            conexaoSQL cSql = new conexaoSQL();
            while (true)
            {
                Console.WriteLine($"Funcionario Selecionado: {nomeFuncionario}");
                Console.Write("Você Deseaja Remover Este Funcionario[S||N]: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "s":
                        sU.deletarFuncionario(nomeFuncionario);
                        break;
                    case "n": Program.menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}