using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace APIMaisEventos.Repositories
{
    public class UsuarioEventoRepository : IUsuarioEventoRepository
    {
        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2;Integrated Security = true;Initial Catalog = MaisEventos;";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM UsuarioEvento WHERE Id=@id";

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

                }
            }
            return true;
        }

        public ICollection<UsuarioEvento> GetAll()
        {
            var usuarioEventos = new List<UsuarioEvento>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM UsuarioEvento";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarioEventos.Add(new UsuarioEvento
                            {
                                Id = (int)reader[0],
                                UsuarioId = (int)reader[1],
                                EventoId = (int)reader[2]
                            });
                        }
                    }
                }
            }

            return usuarioEventos;
        }

        public UsuarioEvento GetById(int id)
        {
            var usuarioEvento = new UsuarioEvento();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM UsuarioEvento WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarioEvento.Id = (int)reader[0];
                            usuarioEvento.UsuarioId = (int)reader[1];
                            usuarioEvento.EventoId = (int)reader[2];                            
                        }
                    }
                }
            }

            return usuarioEvento;
        }

        public UsuarioEvento Insert(UsuarioEvento usuarioEvento)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO UsuarioEvento (UsuarioId, EventoId) VALUES (@UsuarioId, @EventoId)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@UsuarioId", System.Data.SqlDbType.Int).Value = usuarioEvento.UsuarioId;
                    cmd.Parameters.Add("@EventoId", System.Data.SqlDbType.Int).Value = usuarioEvento.EventoId;
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return usuarioEvento;
        }

        public UsuarioEvento Update(int id, UsuarioEvento usuarioEvento)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE UsuarioEvento SET UsuarioId=@UsuarioId, EventoId=@EventoId WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@UsuarioId", System.Data.SqlDbType.NVarChar).Value = usuarioEvento.UsuarioId;
                    cmd.Parameters.Add("@EventoId", System.Data.SqlDbType.NVarChar).Value = usuarioEvento.EventoId;
                    

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    usuarioEvento.Id = id;
                }
            }

            return usuarioEvento;
        }
    }
}
