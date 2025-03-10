using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MenuPro
{
    class Program
    {
        public static Usuario usuario = new Usuario();
        public static conexaoSQL cSQL = new conexaoSQL();
        public static funcoes func = new funcoes();
        //Main, resposavel por chamar e inicializar o projeto
        static void Main(string[] args)
        {
            Console.Title = "MenuPro";
            menuInicial();
        }
        //Onde o usuario ira efetuar o login, para acessar
        static void menuInicial()
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Aplicativo Para O Seu Restaurante\n");
                Console.WriteLine("Efetue Seu Login");
                usuario.insirirUsuario();
                cSQL.efetueLogin(usuario.user, usuario.senha, 1);
            }
        }
        //Menu principal, onde existem outros minis menus, para a escolha do usuario
        public static void menuPrincipal()
        {
            var data = DateTime.Now;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MenuPro - Menu Principal");
                    Console.WriteLine($"Usuario Conectado: {Usuario.nomeDoUsuario}");
                    Console.WriteLine(data + "\n");
                    Console.WriteLine("[1] Vender Produto");
                    Console.WriteLine("[2] Exibição Dos Produtos");
                    Console.WriteLine("[3] Alteração Dos Produtos");
                    Console.WriteLine("[4] Lucro");
                    Console.WriteLine("[5] Funcionarios");
                    Console.WriteLine("[X] Sair");
                    Console.Write("Digite Aqui: ");
                    string opcao = Console.ReadLine().ToLower();

                    switch (opcao)
                    {
                        case "1": menuVenda(); break;
                        case "2": exibicaoProduto(); break;
                        case "3": alteracaoProdutos(); break;
                        case "4": obterLucro(); break;
                        case "5": menuParaFuncionarios(); break;
                        case "x":
                            Console.WriteLine($"\nObrigado pelo seu trabalho hoje! Tenha um ótimo descanso.");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine($"\a\nOpção Inválida!");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                            break;
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
        //Menu de venda
        public static void menuVenda()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Exibição Dos Produtos\n");
                cSQL.exibirProdutos();
                func.pesquisarProduto(1);
            }
        }
        //Exibição Dos Produtos
        static void exibicaoProduto()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Menu De Exibição Dos Produtos\n");
                cSQL.exibirProdutos();
                Console.Write("\nPressione Qualquer Tecla Para Voltar Para O Menu...");
                Console.ReadKey();
                return;
            }
        }
        //Alterações Dos Produtos
        static void alteracaoProdutos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Menu De Alterações De Produtos");
                Console.WriteLine("[1] Adicionar Produto");
                Console.WriteLine("[2] Editar Produto");
                Console.WriteLine("[3] Remover Produto");
                Console.WriteLine("[X] Sair");
                Console.Write("Digite Aqui: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "1": func.adicionarProduto(1); break;
                    case "2": func.pesquisarProduto(2); break;
                    case "3": func.pesquisarProduto(3); break;
                    case "x": menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void obterLucro()
        {
            var data = DateTime.Now;
            var dia = data.Date;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Verificação Dos Lucros");
                Console.WriteLine(dia);
                Console.WriteLine("[1] Exibir Lucros");
                Console.WriteLine("[2] Exibir Lucro Final Do Dia: ");
                Console.WriteLine("[X] Sair");
                Console.Write("Digite Aqui: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "1": exibriLucro(); break;
                    case "2": mostarLucroFinal(); break;
                    case "x": menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        //Menu Para Exibir O Lucro Por Venda
        static void exibriLucro()
        {
            Console.Clear();
            Console.WriteLine("MenuPro - Exibir Lucros Do Dia");
            lucroObtido Lo = new lucroObtido();
            Lo.exibirLucro();
            Console.Write("Pressione Qualquer Tecla Para Continuar...");
            Console.ReadKey();
            menuPrincipal();
        }
        //Exibir O Lucro Total 
        static void mostarLucroFinal()
        {
            lucroObtido lO = new lucroObtido();
            var Data = DateTime.Now;
            Console.Clear();
            Console.WriteLine($"MenuPro - Lucro Do Dia {Data}");
            lO.somarLucro();
            return;
        }
        //Menu Para CRUD do Funcionario
        public static void menuParaFuncionarios()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Para Os Funcionarios");
                Console.WriteLine("[1] Adicionar Funcinario");
                Console.WriteLine("[2] Editar Funcinario");
                Console.WriteLine("[3] Remover Funcinario");
                Console.WriteLine("[4] Exibir Funcinario");
                Console.WriteLine("[X] Sair");
                Console.Write("Digite Aqui: ");
                string opcao = Console.ReadLine().ToLower();
                switch (opcao)
                {
                    case "1": func.inserirNovoFuncionario(1); break;
                    case "2": func.editarFuncionario(1); break;
                    case "3": func.editarFuncionario(2); break;
                    case "4": exibicaoFuncionario(); break;
                    case "x": menuPrincipal(); break;
                    default:
                        Console.WriteLine($"\a\nOpção Inválida!");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        //Mostrar Todo Os Funcionarios
        static void exibicaoFuncionario()
        {
            sqlParaUsuario uS = new sqlParaUsuario();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Menu De Exibição Dos Usuarios\n");
                uS.exibirFuncionarios();
                Console.Write("\nPressione Qualquer Tecla Para Voltar Para O Menu...");
                Console.ReadKey();
                return;
            }
        }
    }
}
