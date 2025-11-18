using CRM.API.Models;
using CRM.Application.Interface;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System;
using CRM.API.Utils;
using CRM.Application.ModelResponse;
using Microsoft.AspNetCore.OData.Query;
using Hanssens.Net;
using CRM.API.Models.DTO;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _serviceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioController(IUsuarioAppService serviceBase, IHttpContextAccessor httpContextAccessor)
        {
            _serviceBase = serviceBase;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody] LoginModel usuario)
        {
            try
            {
                string senha = usuario.Senha;

                var retorno = _serviceBase.GetByFilter(x =>
                    x.email == usuario.Email &&
                    x.senha == senha
                ).FirstOrDefault();

                if (retorno != null)
                {
                    if (!retorno.status)
                    {
                        return new
                        {
                            authenticate = false,
                            message = "Usuário inativo, contate o administrador do sistema"
                        };
                    }

                    return new
                    {
                        authenticate = true,
                        message = "Usuário válido",
                        nome = retorno.nome,
                        usuarioId = retorno.UsuarioId,
                        email = retorno.email,
                        adm = retorno.adm
                    };
                }
                else
                {
                    return new
                    {
                        authenticate = false,
                        message = "Usuário ou senha inválido(s)"
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    authenticate = false,
                    message = "Erro ao autenticar: " + ex.Message
                };
            }
        }


        [HttpPost("EsqueciSenha")]
        public IActionResult EsqueciSenha([FromBody] EmailModel email)
        {
            try
            {
                var usuario = _serviceBase.GetByFilter(x => x.email == email.Email).FirstOrDefault();
                if (usuario == null)
                {
                    ResponseModel resposta = new ResponseModel(true, "E-mail não encontrado.", false);
                    return Ok(resposta);
                }
                else if (!usuario.status)
                {
                    ResponseModel resposta = new ResponseModel(true, "Usuário inativo, contate o administrador do sistema", false);
                    return Ok(resposta);
                }

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[6];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                string novaSenha = finalString;
                usuario.senha = Global.Criptografa(novaSenha);
                _ = _serviceBase.Update(usuario);

                ResponseModel resp = new ResponseModel(true, "A nova senha foi enviada para o e-mail.", false);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                ResponseModel resposta = new ResponseModel(false, "Erro ao gerar nova senha.", true);
                return BadRequest(resposta);
            }
        }

        [HttpGet("CountAll")]
        public IActionResult CountAll()
        {
            int count = _serviceBase.CountAll();
            return Ok(count);
        }


        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Usuario> options)
        {
            IQueryable results = options.ApplyTo(_serviceBase.GetByOdata());
            return Ok(results);
        }


        [HttpPost("AdicionarUsuario")]
        public IActionResult AdicionarUsuario([FromBody] Usuario pUsuario)
        {
            try
            {
                // Gerar uma senha aleatória
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[6];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                string novaSenha = finalString;

                pUsuario.senha = novaSenha;
                bool buscaPorEmail = _serviceBase.GetByFilter(a => a.email.ToUpper() == pUsuario.email.ToUpper()).Any();

                if (buscaPorEmail)
                {
                    ResponseModel resp = new ResponseModel(false, "Já existe um usuário com este e-mail.", false);
                    return Ok(resp);
                }

                // Adicionar o usuário
                pUsuario.adm = true;
                ResponseModel resposta = _serviceBase.Add(pUsuario);

                if (!resposta.Error)
                {
                    //Instanciar o serviço de e - mail
                    var emailService = new EmailService();

                    //Chamar o método de envio de e - mail
                    emailService.EnviarEmailNovoUsuario(pUsuario.nome, pUsuario.email, novaSenha, pUsuario.email);

                }

                return Ok(resposta);
            }
            catch (Exception)
            {
                ResponseModel resposta = new ResponseModel(true, "Erro ao cadastrar usuário", false);
                return Ok(resposta);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ResponseModel resposta;
            var model = _serviceBase.GetById(id);

            if (model != null)
            {
                resposta = _serviceBase.Delete(model);
            }
            else
                resposta = new ResponseModel(true, "Usuário não encontrado", false);

            if (!resposta.Error) // Verifica se resposta não é nula
            {
                return Ok(resposta);
            }
            else
            {
                return BadRequest(resposta);
            }
        }

        [HttpPut]
        public IActionResult Put(Usuario pUsuario)
        {
            try
            {
                ResponseModel resposta = _serviceBase.Update(pUsuario);

                if (!resposta.Error) // Verifica se resposta não é nula
                {
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception)
            {
                ResponseModel resposta = new ResponseModel(true, "Erro ao editar Usuário", false);
                return Ok(resposta);
            }
        }
    }
}
