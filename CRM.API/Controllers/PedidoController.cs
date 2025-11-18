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
using Microsoft.EntityFrameworkCore;
using CRM.API.Models.DTO;
using System.Globalization;
using Stripe;
using System.Net.Http;
using System.IO;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _servicePedido;
        private readonly ILeadAppService _serviceBase;
        private readonly IUsuarioAppService _serviceUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PedidoController(IPedidoAppService servicePedido, ILeadAppService serviceBase, IUsuarioAppService serviceUsuario, IHttpContextAccessor httpContextAccessor)
        {
            _servicePedido = servicePedido;
            _serviceBase = serviceBase;
            _serviceUsuario = serviceUsuario;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("SalvarPedido")]
        public IActionResult SalvarPedido([FromBody] PedidoDTO pedidoDto)
        {
            try
            {
                if (pedidoDto == null || pedidoDto.LeadId == 0)
                    return BadRequest("Pedido inválido.");

                // Converte DTO para entidade Pedido
                var pedido = new Pedido
                {
                    PedidoId = pedidoDto.PedidoId ?? 0,
                    LeadId = pedidoDto.LeadId,
                    Data = DateTime.Parse(pedidoDto.Data, null, DateTimeStyles.RoundtripKind),
                    Total = pedidoDto.Total,
                    Itens = pedidoDto.Itens?.Select(i => new PedidoItem
                    {
                        PedidoItemId = i.PedidoItemId ?? 0,
                        PedidoId = i.PedidoId ?? 0,
                        NomeProduto = i.NomeProduto,
                        PrecoUnitario = i.PrecoUnitario,
                        Quantidade = i.Quantidade
                    }).ToList() ?? new List<PedidoItem>()
                };

                Pedido pedidoRetorno;

                if (pedido.PedidoId > 0)
                {
                    // Carrega pedido existente
                    var pedidoExistente = _servicePedido.GetPedidoId(pedido.PedidoId);
                    if (pedidoExistente == null)
                        return NotFound("Pedido não encontrado para atualização.");

                    bool houveAlteracao = false;

                    // Atualiza dados
                    if (pedidoExistente.Data != pedido.Data)
                    {
                        pedidoExistente.Data = pedido.Data;
                        pedidoExistente.Status = "Aguardando Pagamento";
                        houveAlteracao = true;
                    }

                    decimal totalCalculado = pedido.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
                    if (pedidoExistente.Total != totalCalculado)
                    {
                        pedidoExistente.Total = totalCalculado;
                        houveAlteracao = true;
                    }

                    // Remove itens que não existem mais
                    var itensParaRemover = pedidoExistente.Itens
                        .Where(i => !pedido.Itens.Any(n => n.NomeProduto == i.NomeProduto))
                        .ToList();

                    foreach (var itemRemover in itensParaRemover)
                    {
                        pedidoExistente.Itens.Remove(itemRemover);
                        houveAlteracao = true;
                    }

                    // Atualiza itens existentes ou adiciona novos
                    foreach (var itemNovo in pedido.Itens)
                    {
                        var itemExistente = pedidoExistente.Itens
                            .FirstOrDefault(i => i.NomeProduto == itemNovo.NomeProduto);

                        if (itemExistente != null)
                        {
                            if (itemExistente.PrecoUnitario != itemNovo.PrecoUnitario ||
                                itemExistente.Quantidade != itemNovo.Quantidade)
                            {
                                itemExistente.PrecoUnitario = itemNovo.PrecoUnitario;
                                itemExistente.Quantidade = itemNovo.Quantidade;
                                houveAlteracao = true;
                            }
                        }
                        else
                        {
                            pedidoExistente.Itens.Add(itemNovo);
                            houveAlteracao = true;
                        }
                    }

                    if (houveAlteracao)
                    {
                        _servicePedido.Update(pedidoExistente);
                    }

                    pedidoRetorno = pedidoExistente;
                }
                else
                {
                    // Pedido novo
                    _servicePedido.Add(pedido);
                    pedidoRetorno = pedido;
                }

                // Atualiza status do lead
                var lead = _serviceBase.GetById(pedidoDto.LeadId);
                if (lead != null)
                {
                    lead.statusCadastro = "Aguardando Pagamento";
                    lead.pgto = false;
                    lead.formaPagto = null;
                    _serviceBase.Update(lead);

                    // Inclui o lead no retorno (evita Lead = null)
                    pedidoRetorno.Lead = lead;
                }

                return Ok(new { success = true, message = "Pedido salvo.", data = pedidoRetorno });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        [HttpGet("obterPedido/{leadId}")]
        public IActionResult ObterPedido(int leadId)
        {
            try
            {
                var pedido = _servicePedido.GetPedido(leadId);

                if (pedido == null)
                    return NotFound(new { success = false, message = "Nenhum pedido aguardando pagamento encontrado." });

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        [HttpPost("pagamento")]
        public async Task<IActionResult> Pagamento([FromBody] PagamentoDTO pagamento)
        {
          //COLOQUE AQUI SUA CHAVE STRIPE

            try
            {
                // Cria PaymentIntent apenas para cartão
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(pagamento.Valor * 100), // valor em centavos
                    Currency = "brl",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();
                var intent = await service.CreateAsync(options);

                // Retorna clientSecret para o frontend confirmar o pagamento
                return Ok(new { clientSecret = intent.ClientSecret });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DTO simplificado
        public class PagamentoDTO
        {
            public decimal Valor { get; set; }
        }

        [HttpPut("atualizaStatus/{pedidoId}/{status}")]
        public IActionResult AtualizarStatusPedido(int pedidoId, string status)
        {
            try
            {
                var pedido = _servicePedido.GetPedidoId(pedidoId);
                if (pedido == null)
                    return NotFound("Pedido não encontrado.");

                pedido.Status = status;
                _servicePedido.Update(pedido);

                // Se o pagamento foi confirmado, envia email para o admin
                if (status.Equals("Pago", StringComparison.OrdinalIgnoreCase))
                {
                    // Carregar também o Lead relacionado
                    var lead = _serviceBase.GetById(pedido.LeadId);
                    pedido.Lead = lead;

                    // Buscar administradores
                    var administradores = _serviceUsuario.GetTodosUsuarios();
                    var emailsAdmins = administradores.Select(a => a.email);

                    var emailService = new EmailService();
                    emailService.EnviarEmailConfirmacaoPedido(pedido, emailsAdmins);
                }

                return Ok(new { success = true, message = "Status do pedido atualizado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



    }
}
