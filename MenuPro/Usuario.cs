using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace MenuPro
{
    public class Usuario
    {
        public string nome { get; set; }
        public string user { get; set; }
        public string senha { get; set; }
        public static string nomeDoUsuario { get; set; }
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

    class sqlParaUsuario : Usuario
    {
        public SqlConnection cn = new SqlConnection(@"Data Source = LAPTOPZEMBER; Integrated Security = SSPI; Initial Catalog = db_MenuPro");
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;

        public void pesquisarFuncionario(string nome)
        {
            funcoes func = new funcoes();
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM tbl_Usuarios WHERE us_Usuario = @nome";
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Console.WriteLine($"Usuário Encontrado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    func.inserirNovoFuncionario(2);
                }
                else
                {
                        Console.WriteLine($"\a\nUsuário Não Encontrado");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("\a\nEnconrtamos Um Erro");
                Console.WriteLine($"Erro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }

        public void adicionarFuncionario(string nomeF, string usuarioF, string senhaF)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO tbl_Usuarios (nm_Usuario, us_Usuario, sn_Usuario) VALUES (@nome, @usuario, @senha)";
                cmd.Parameters.AddWithValue("@nome", nomeF);
                cmd.Parameters.AddWithValue("@usuario", usuarioF);
                cmd.Parameters.AddWithValue("@senha", senhaF);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Usuário Adicionado!");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuPrincipal();
                }
                else
                {
                    Console.WriteLine("\a\nUsuário Não Adicionado!");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuParaFuncionarios();
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("\a\nEnconrtamos Um Erro");
                Console.WriteLine($"Erro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }

        public void editarFuncionario(string nomeF, string usuarioF, string senhaF, string nomeAntigo)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE tbl_Usuarios SET nm_Usuario = @nome, us_Usuario = @usuario, sn_Usuario = @senha WHERE us_Usuario = @nomeAntigo";
                cmd.Parameters.AddWithValue("@nome", nomeF);
                cmd.Parameters.AddWithValue("@usuario", usuarioF);
                cmd.Parameters.AddWithValue("@senha", senhaF);
                cmd.Parameters.AddWithValue("@nomeAntigo", nomeAntigo);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Usuário Editado!");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuPrincipal();
                }
                else
                {
                    Console.WriteLine("\n\aUsuário Não Editado!");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuParaFuncionarios();
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("\a\nEnconrtamos Um Erro");
                Console.WriteLine($"Erro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }

        public void exibirFuncionarios()
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT us_Usuario, nm_Usuario FROM tbl_Usuarios";
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"Nome: {dr["nm_Usuario"]} \nUsuario: {dr["us_Usuario"]}");
                    for (int i = 0; i <= 15; i++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine("\n");
                }
                return;
            }
            catch (Exception Erro)
            {
                Console.WriteLine("\a\nEnconrtamos Um Erro");
                Console.WriteLine($"Erro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }

        public void deletarFuncionario(string nomeFuncionario)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM tbl_Usuarios WHERE us_Usuario = @nomeFuncionario";
                cmd.Parameters.AddWithValue("@nomeFuncionario", nomeFuncionario);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\nFuncionario Deletado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuPrincipal();
                }
                else
                {
                    Console.WriteLine($"\n\aFuncionario Não Deletado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine($"\a\nFuncionario Não Encontrado {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }
    }
}
