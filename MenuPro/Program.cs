using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuPro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MenuPro";
            menuInicial();
        }

        static void menuInicial()
        {
            Usuario usuario = new Usuario();
            conexaoSQL cSQL = new conexaoSQL();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MenuPro - Aplicativo Para O Seu Restaurante\n");
                    Console.WriteLine("Efetue Seu Login");
                    Console.Write("Usuario: ");
                    usuario.user = Console.ReadLine();
                    Console.Write("Senha: ");
                    usuario.senha = Console.ReadLine();
                    cSQL.efetueLogin(usuario.user, usuario.senha);
                }
                catch(Exception Erro)
                {
                    Console.WriteLine($"Erro: {Erro.Message}");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                }
            }
        }

        public static void menuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MenuPro - Menu Principal");
                Console.ReadKey();
            }
        }
    }
}
