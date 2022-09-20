using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIMaisEventos.Repositories
{
    public class EventosRepository : IEventosRepository
    {
        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2;Integrated Security = true;Initial Catalog = MaisEventos;";
        CategoriasRepository repositorioCategorias = new CategoriasRepository();

        public bool Delete(int id)
        {
            using(SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Eventos WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
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

        public ICollection<Eventos> GetAll()
        {
            var eventos = new List<Eventos>();

            using(SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Eventos";

                using(SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventos.Add(new Eventos
                            {
                                Id = (int)reader[0],
                                DataHora = (DateTime)reader[1],
                                Ativo = (bool)reader[2],
                                Preco = (double)reader[3],
                                CategoriaId = (int)reader[4],
                            });
                        }
                    }
                }
            }

            return eventos;
        }

        public Eventos GetById(int id)
        {
            var evento = new Eventos();

            using(SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Eventos WHERE Id=@id";

                    using(SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;


                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evento.DataHora = (DateTime)reader[0];
                            evento.Ativo = (bool)reader[1];
                            evento.Preco = (double)reader[2];
                            evento.CategoriaId = (int)reader[3];
                        }
                    }
                }
            }

            return evento;
        }

        public Eventos Insert(Eventos evento)
        {
            var buscarCategoriaId = repositorioCategorias.GetById(evento.CategoriaId);

            if (buscarCategoriaId == null)
            {
                return null;
            }

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "INSERT INTO Eventos (DataHora, Ativo, Preco, CategoriaId) VALUES (@DataHora, @Ativo, @Preco, @CategoriaID)";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@DataHora", System.Data.SqlDbType.DateTime).Value = evento.DataHora;
                    cmd.Parameters.Add("@Ativo", System.Data.SqlDbType.Bit).Value = evento.Ativo;
                    cmd.Parameters.Add("@Preco", System.Data.SqlDbType.Decimal).Value = evento.Preco;
                    cmd.Parameters.Add("@CategoriaId", System.Data.SqlDbType.Int).Value = evento.CategoriaId;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return evento;
        }

        public Eventos Update(int id, Eventos evento)
        {
            var buscarCategoriaId = repositorioCategorias.GetById(evento.CategoriaId);

            if (buscarCategoriaId == null)
            {
                return null;
            }

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Eventos SET DataHora=@DataHora, Ativo=@Ativo, Preco=@Preco, CategoriaId=@CategoriaId WHERE Id=@id";

                using(SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@DataHora", System.Data.SqlDbType.DateTime).Value = evento.DataHora;
                    cmd.Parameters.Add("@Ativo", System.Data.SqlDbType.Bit).Value = evento.Ativo;
                    cmd.Parameters.Add("@Preco", System.Data.SqlDbType.Decimal).Value = evento.Preco;
                    cmd.Parameters.Add("@CategoriaId", System.Data.SqlDbType.Int).Value = evento.CategoriaId;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    evento.Id = id;
                }
            }

            return evento;
        }
    }
}
