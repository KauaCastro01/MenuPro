using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuPro
{
    public class Usuario
    {
        public string nome { get; set; }
        public string user { get; set; }
        public string senha { get; set; }

        public void insirirUsuario()
        {
            try
            {
                Console.Write("Usuario: ");
                user = Console.ReadLine();
                Console.Write("Senha: ");
                senha = Console.ReadLine();
                return;
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
