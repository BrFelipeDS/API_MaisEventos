using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace APIMaisEventos.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2;Integrated Security = true;Initial Catalog = MaisEventos;";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Categorias WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }

                    return true;
                }
            }

        }

        public ICollection<Categorias> GetAll()
        {
            var categorias = new List<Categorias>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Categorias";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new Categorias
                            {
                                Id = (int)reader[0],
                                Categoria = (string)reader[1]                                
                            });
                        }
                    }
                }
            }

            return categorias;
        }

        public Categorias GetById(int id)
        {
            var categoria = new Categorias();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Categorias WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoria.Id = (int)reader[0];
                            categoria.Categoria = (string)reader[1]; 
                        }
                    }
                }
            }

            return categoria;
        }

        public Categorias Insert(Categorias categoria)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Categorias (Categoria) VALUES (@Categoria)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Categoria", System.Data.SqlDbType.NVarChar).Value = categoria.Categoria;                   

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return categoria;
        }

        public Categorias Update(int id, Categorias categoria)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Categorias SET Categoria=@Categoria WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Categoria", System.Data.SqlDbType.NVarChar).Value = categoria.Categoria;
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    categoria.Id = id;
                }
            }

            return categoria;
        }
    }
}

