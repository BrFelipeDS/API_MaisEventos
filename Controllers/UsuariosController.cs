using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using APIMaisEventos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository repositorio;

        public UsuariosController(IUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Inserção de um objeto no banco de dados. MANTENHA O VALOR DE TODOS OS "Id" COMO "0"!
        /// </summary>
        /// <param name="usuario">Objeto completo a ser inserido</param>
        /// <param name="arquivo">Arquivo de imagem a ser postado</param>
        /// <returns>Objeto inserido</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Usuarios usuario, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                usuario.Imagem = uploadResultado;
                #endregion

                var retorno = repositorio.Insert(usuario);
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
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
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
        /// <param name="usuario">O objeto completado que substituirá o existente no banco de dados</param>
        /// <param name="arquivo">Arquivo de imagem a ser alterado</param>
        /// <returns>Objeto alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm] Usuarios usuario, IFormFile arquivo)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (id != usuario.Id)
                {
                    return BadRequest(new { Message = "O id indicado deve ser o mesmo id do paciente" });
                }

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
                }

                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                usuario.Imagem = uploadResultado;
                #endregion

                repositorio.Update(usuario);

                return Ok(usuario);
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

                var usuario = repositorio.GetById(id);

                if (usuario is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
                }

                repositorio.UpdateParcial(patch, usuario);

                return Ok(usuario);
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
                    return NotFound(new { Message = "Não foi encontrado um usuario com esse Id." });
                }

                repositorio.Delete(busca);

                return Ok(new { Message = "Usuario excluído com sucesso" });
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
