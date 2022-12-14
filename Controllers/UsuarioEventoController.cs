using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using APIMaisEventos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using Microsoft.AspNetCore.JsonPatch;
using APIMaisEventos.Interfaces;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioEventoController : ControllerBase
    {
        private readonly IUsuarioEventoRepository repositorio;

        public UsuarioEventoController(IUsuarioEventoRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Inserção de um objeto no banco de dados. MANTENHA O VALOR DE TODOS OS "Id" COMO "0"!
        /// </summary>
        /// <param name="usuarioEvento">Objeto completo a ser inserido</param>
        /// <returns>Objeto inserido</returns>
        [HttpPost]
        public IActionResult Cadastrar(UsuarioEvento usuarioEvento)
        {
            try
            {
                var retorno = repositorio.Insert(usuarioEvento);
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        /// <summary>
        /// Lista todos os objetos presentes no banco de dados
        /// </summary>
        /// <returns>Lista de todos os objetos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.GetAll();
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }

        }

        /// <summary>
        /// Seleciona apenas 1 objeto com o Id especificado
        /// </summary>
        /// <param name="id">Id do objeto a ser selecionado</param>
        /// <returns>Objeto com o Id inserido</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario/evento com esse Id." });
                }

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        /// <summary>
        /// Altera um objeto no banco de dados conforme o Id. ATENÇÃO: NO JSON, COLOQUE O VALOR DOS "Id" CONFORME SEUS VALORES NO BANCO DE DADOS!
        /// </summary>
        /// <param name="id">Id do objeto a ser alterado</param>
        /// <param name="usuarioEvento">O objeto completado que substituirá o existente no banco de dados</param>
        /// <returns>Objeto alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, UsuarioEvento usuarioEvento)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (id != usuarioEvento.Id)
                {
                    return BadRequest(new { Message = "O id indicado deve ser o mesmo id do paciente" });
                }

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario/evento com esse Id." });
                }

                repositorio.Update(usuarioEvento);

                return Ok(usuarioEvento);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        /// <summary>
        /// Altera apenas informações específicas do objeto
        /// </summary>
        /// <param name="id">Id do objeto a ser alterado</param>
        /// <param name="patch">Informações que serão alteradas no objeto destino</param>
        /// <returns>Novo objeto com as alterações realizadas</returns>
        [HttpPatch("{id}")]
        public IActionResult AlterarParcialmente(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch is null)
                {
                    return BadRequest(new { Message = "Não houve alterações no objeto" });
                }

                var usuarioEvento = repositorio.GetById(id);

                if (usuarioEvento is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario/evento com esse Id." });
                }

                repositorio.UpdateParcial(patch, usuarioEvento);

                return Ok(usuarioEvento);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }

        }

        /// <summary>
        /// Deleta um objeto do banco de dados de acordo com o id informado
        /// </summary>
        /// <param name="id">Id do objeto a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var busca = repositorio.GetById(id);

                if (busca is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario/evento com esse Id." });
                }

                repositorio.Delete(busca);

                return Ok(new { Message = "Usuario/evento excluído com sucesso" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }
    }
}
