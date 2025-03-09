using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace MenuPro
{
    public class conexaoSQL
    {
        public SqlConnection cn = new SqlConnection(@"Data Source = LAPTOPZEMBER; Integrated Security = SSPI; Initial Catalog = db_MenuPro");
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;

        //Metodo para realizar o login
        public void efetueLogin(string nome, string senha, int i)
        {
            funcoes func = new funcoes();
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                if(i == 1)
                {
                    cmd.CommandText = "SELECT * FROM tbl_Usuarios WHERE us_Usuario = @nome AND sn_Usuario = @senha";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@senha", senha);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM tbl_Produtos WHERE nm_Produto = @nome";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    funcoes.nomeParaExibir = nome;
                }

                    cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    switch (i)
                    {
                        case 1: Program.menuPrincipal(); break;
                        case 2:
                            Console.WriteLine($"\a\nProduto Encontrado, Digite O Novo Nome Para Ele");
                            Console.Write("Pressione Qualquer Tecla Para Continuar...");
                            Console.ReadKey();
                            func.adicionarProduto(2);
                            break;
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        Console.WriteLine($"\a\nUsuário Não Encontrado");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"\a\nProduto Não Encontrado");
                        Console.Write("Pressione Qualquer Tecla Para Continuar...");
                        Console.ReadKey();
                        return;
                    }
                    
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
        //Metodo para exibição dos produtos
        public void exibirProdutos()
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT nm_Produto, vlr_Produto FROM tbl_Produtos";
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"Produto: {dr["nm_Produto"]}\nValor: {dr["vlr_Produto"]}R$");
                    for (int i=0; i<=15; i++)
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
        //Metodo Para Obter o preço
        public void obterPrecoProduto(string nomeProduto)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM tbl_Produtos WHERE nm_Produto = @nomeProduto";
                cmd.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcoes.nomeParaExibir = dr.GetString(dr.GetOrdinal("nm_Produto"));
                        funcoes.valorProduto = dr.GetDecimal(dr.GetOrdinal("vlr_Produto"));
                    }
                    return;
                }
                else
                {
                    Console.WriteLine($"\n\aProduto Não Encontrado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuVenda();
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("\a\nEnconrtamos Um Erro");
                Console.WriteLine($"Erro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                Program.menuVenda();
            }
            finally { cn.Close(); }
        }
        //Metodo para adicionar o produto novo
        public void adicionarProduto(string nomeProduto, decimal valorProduto)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO tbl_Produtos (nm_Produto, vlr_Produto) VALUES (@nomeProduto, @valorProduto)";
                cmd.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                cmd.Parameters.AddWithValue("@valorProduto", valorProduto);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\n\aProduto Adicionado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuPrincipal();
                }
                else
                {
                    Console.WriteLine($"\n\aProduto Não Adicionado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine($"\a\nProduto Já Existente {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }

        public void editarProduto(string nomeProduto, decimal valorProduto, string nomeAntigo)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE tbl_Produtos SET nm_Produto = @nomeProduto, vlr_Produto = @valorProduto WHERE nm_Produto = @nomeAntigo";
                cmd.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                cmd.Parameters.AddWithValue("@valorProduto", valorProduto);
                cmd.Parameters.AddWithValue("@nomeAntigo", nomeAntigo);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\n\aProduto Editado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    Program.menuPrincipal();
                }
                else
                {
                    Console.WriteLine($"\n\aProduto Não Editado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine($"\a\nProduto Já Existente {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }
    }
}
