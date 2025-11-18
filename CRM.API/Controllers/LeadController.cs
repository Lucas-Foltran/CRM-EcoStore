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
using Newtonsoft.Json;
using Serilog;
using Stripe;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadAppService _serviceBase;
        private readonly IUsuarioAppService _serviceUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeadController(ILeadAppService serviceBase, IUsuarioAppService serviceUsuario, IHttpContextAccessor httpContextAccessor)
        {
            _serviceBase = serviceBase;
            _serviceUsuario = serviceUsuario;
            _httpContextAccessor = httpContextAccessor;
        }

        //Começa aqui
        [AllowAnonymous]
        [HttpPost("AdicionarLead")]
        public IActionResult AdicionarLead()
        {
            try
            {
                var obj = Request.Form["obj"];
                var objConvertido = JsonConvert.DeserializeObject<Lead>(obj);

                if (objConvertido == null)
                {
                    return BadRequest(new ResponseModel(false, "Lead inválido!", false));
                }

                // Garante que o ID não seja enviado ao banco
                objConvertido.leadId = 0;

                // Gerar token único
                bool tokenExists;
                string token;
                do
                {
                    token = Global.GerarToken();
                    tokenExists = _serviceBase.GetByFilter(lead => lead.token == token).Any();
                } while (tokenExists);

                objConvertido.token = token;
                objConvertido.dataCadastro = DateTime.Now;
                objConvertido.status = "Ativo";
                objConvertido.statusCadastro = "Aberto";
                objConvertido.pgto = false;
                objConvertido.adm = false;

                ResponseModel resposta = _serviceBase.Add(objConvertido);

                // Serviço de e-mail
                var emailService = new EmailService();
                emailService.EnviarEmailNovoLead(objConvertido.nomeContato, objConvertido.link, objConvertido.email, objConvertido.token, objConvertido.leadId);

                // Buscar administradores
                var administradores = _serviceUsuario.GetTodosUsuarios();
                var emailsAdmins = administradores.Select(a => a.email);

                emailService.EnviarEmailInformacaoLead(objConvertido, emailsAdmins);

                if (!resposta.Error)
                {
                    return Ok(resposta);
                }
                else
                {
                    Log.Debug("Erro: " + resposta.Message);
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                ResponseModel resposta = new ResponseModel(true, "Erro ao Salvar Lead: " + ex.Message, false);
                return BadRequest(resposta);
            }
        }


        [AllowAnonymous]
        [HttpPost("LoginLead")]
        public IActionResult LoginLead([FromBody] LeadLoginLeadModel model)
        {
            try
            {
                var lead = _serviceBase
                    .GetByFilter(l =>
                        l.email.ToLower() == model.Email.ToLower() &&
                        l.token == model.Token
                    ).FirstOrDefault();

                if (lead == null)
                {
                    return Unauthorized(new ResponseModel(true, "Token ou e-mail inválido.", false));
                }

                return Ok(new
                {
                    sucesso = true,
                    mensagem = "Login realizado com sucesso.",
                    leadId = lead.leadId,
                    nome = lead.nomeContato,
                    email = lead.email,
                    token = lead.token,
                    status = lead.status,
                    statusCadastro = lead.statusCadastro
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(true, $"Erro ao realizar login: {ex.Message}", false));
            }
        }


        [HttpGet("CountAll")]
        public IActionResult CountAll()
        {
            int count = _serviceBase.CountAll();
            return Ok(count);
        }

        [HttpGet]
        public IActionResult Get(ODataQueryOptions<Lead> options)
        {
            IQueryable results = options.ApplyTo(_serviceBase.GetByOdata());
            return Ok(results);
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
                resposta = new ResponseModel(true, "Lead não encontrado", false);

            if (!resposta.Error) 
            {
                return Ok(resposta);
            }
            else
            {
                return BadRequest(resposta);
            }
        }

        [HttpPut]
        public IActionResult Put(Lead pLead)
        {
            try
            {
                ResponseModel resposta = _serviceBase.Update(pLead);

                if (!resposta.Error) 
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
                ResponseModel resposta = new ResponseModel(true, "Erro ao editar Lead", false);
                return Ok(resposta);
            }
        }

    }
}
