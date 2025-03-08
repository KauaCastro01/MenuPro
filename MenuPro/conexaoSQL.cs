using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace MenuPro
{
    class conexaoSQL
    {
        public SqlConnection cn = new SqlConnection(@"Data Source = LAPTOPZEMBER; Integrated Security = SSPI; Initial Catalog = db_MenuPro");
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;

       public void efetueLogin(string nome, string senha)
        {
            try
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM tbl_Usuarios WHERE us_Usuario = @nome AND sn_Usuario = @senha";
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@senha", senha);
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
                if (dr.HasRows) { Program.menuPrincipal(); }
                else
                {
                    Console.WriteLine($"\nUsuário Não Encontrado");
                    Console.Write("Pressione Qualquer Tecla Para Continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("Enconrtamos Um Erro");
                Console.WriteLine($"\nErro: {Erro.Message}");
                Console.Write("Pressione Qualquer Tecla Para Continuar...");
                Console.ReadKey();
                return;
            }
            finally { cn.Close(); }
        }
    }
}
