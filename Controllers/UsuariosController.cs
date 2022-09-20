using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using APIMaisEventos.Utils;
using Microsoft.AspNetCore.Http;
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
        private UsuarioRepository repositorio = new UsuarioRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra usuários na aplicação
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <param name="arquivo">Arquivo a ser subido</param>
        /// <returns>Dados do usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Usuarios usuario, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if(uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                usuario.Imagem = uploadResultado;
                #endregion


                repositorio.Insert(usuario);               
                return Ok(usuario);
                
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


        // GET - Listar
        /// <summary>
        /// Lista todos os usuário da apliacação
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var usuarios = repositorio.GetAll();
                return Ok(usuarios);
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

        // PUT - Alterar
        /// <summary>
        /// Altera os dados de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="usuario">Todas as informações do usuário</param>
        /// <param name="arquivo">Arquivo anexado</param>
        /// <returns>Usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm]  Usuarios usuario, IFormFile arquivo)
        {
            try
            {
                var buscarUsuario = repositorio.GetById(id);

                if(buscarUsuario == null)
                {
                    return NotFound();
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

                var usuarioAlterado = repositorio.Update(id, usuario);

                return Ok(usuario);
                
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

        // DELETE - Excluir
        /// <summary>
        /// Exclui um usuário da aplicação
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarUsuario = repositorio.GetById(id);

                if (buscarUsuario == null)
                {
                    return NotFound();
                }
                
                repositorio.Delete(id);

                return Ok(new
                    {
                        msg = "Usuário excluído com sucesso"
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


        // Repository Pattern

        // Singleton
        // Upload de Imagens
    }
}
