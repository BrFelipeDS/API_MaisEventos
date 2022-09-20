using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace APIMaisEventos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        // Criar string de conexão com o Banco de Dados
        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2;Integrated Security = true;Initial Catalog = MaisEventos;";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Usuarios WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if(linhasAfetadas == 0)
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        public ICollection<Usuarios> GetAll()
        {
            var usuarios = new List<Usuarios>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Usuarios";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuarios
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Senha = (string)reader[3],
                                Imagem = (string)reader[4].ToString()
                            });
                        }
                    }
                }
            }

            return usuarios;
        }

        public Usuarios GetById(int id)
        {
            var usuario = new Usuarios();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Usuarios WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            usuario.Id = (int)reader[0];
                            usuario.Nome = (string)reader[1];
                            usuario.Email = (string)reader[2];
                            usuario.Senha = (string)reader[3];
                            
                        }
                    }
                }
            }

            return usuario;
        }

        public Usuarios Insert(Usuarios usuario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Usuarios (Nome, Email, Senha, Imagem) VALUES (@Nome, @Email, @Senha, @Imagem)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", System.Data.SqlDbType.NVarChar).Value = usuario.Senha;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = usuario.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return usuario;
        }
        public Usuarios Update(int id, Usuarios usuario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Usuarios SET Nome=@Nome, Email=@Email, Senha=@Senha, Imagem=@Imagem WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", System.Data.SqlDbType.NVarChar).Value = usuario.Senha;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = usuario.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    usuario.Id = id;
                }
            }

            return usuario;
        }
    }
}
