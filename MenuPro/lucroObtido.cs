using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MenuPro
{
    class lucroObtido
    {
        public SqlConnection cn = new SqlConnection(@"Data Source = LAPTOPZEMBER; Integrated Security = SSPI; Initial Catalog = db_MenuPro");
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;

        public void salvarVenda(decimal valor, DateTime data)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO tbl_LucroDia (valorProduto, dataValor) VALUES (@valor, @data)";
                cmd.Parameters.AddWithValue("@valor", valor);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Connection = cn;
                int linhasAfetadas = cmd.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\nLucro Adicionado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    return;
                }
                else
                {
                    Console.WriteLine($"\n\nLucro Não Adicionado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine($"\a\nErro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                return;
            }
            finally { cn.Close(); }
        }

        public void exibirLucro()
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT valorProduto, dataValor FROM tbl_LucroDia";
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"Valor: {dr["valorProduto"]}R$\nData: {dr["dataValor"]}");
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



    }
}
