using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private EventosRepository repositorio = new EventosRepository();

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Eventos evento)
        {
            try
            {
                var validarCategoria = repositorio.Insert(evento);

                if (validarCategoria == null)
                {
                    return NotFound(new
                    {
                        msg = "Id de Categoria não encontrado"
                    });
                }

                return Ok(evento);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var eventos = repositorio.GetAll();
                return Ok(eventos);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult Alterar(int id,[FromForm] Eventos evento)
        {
            try
            {
                var buscarEvento = repositorio.GetById(id);

                if(buscarEvento == null)
                {
                    return NotFound();
                }


                var eventoAlterado = repositorio.Update(id, evento);

                if (eventoAlterado == null)
                {
                    return NotFound(new
                    {
                        msg = "Id de Categoria não encontrado"
                    });
                }
                return Ok(evento);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarEvento = repositorio.GetById(id);

                if(buscarEvento == null)
                {
                    return NotFound();
                }

                repositorio.Delete(id);
                return Ok(new
                {
                    msg = "Evento excluído com sucesso"
                });

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }
    }
}
