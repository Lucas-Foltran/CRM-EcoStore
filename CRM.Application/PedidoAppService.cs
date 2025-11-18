using CRM.Application.Interface;
using CRM.Application.ModelResponse;
using CRM.Domain.Entities;
using CRM.Domain.Interface.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.Application
{
    public class PedidoAppService : AppServiceBase<Pedido>, IPedidoAppService
    {
        protected readonly IPedidoService _service;

        public PedidoAppService(IServiceBase<Pedido> serviceBase, IPedidoService service) : base(serviceBase)
        {
            _service = service;
        }

        public ResponseModel AddPedidoComItens(Pedido pedido)
        {
            try
            {
                _service.Add(pedido);      
                _service.SaveChanges();     
                return new ResponseModel(false, "Pedido salvo com sucesso.", true);
            }
            catch (Exception ex)
            {
                return new ResponseModel(true, ex.Message, false);
            }
        }

        public Pedido GetPedido(int leadId)
        {
            return _service.GetPedidosComItens()
                           .FirstOrDefault(p => p.LeadId == leadId);
        }

        public Pedido GetPedidoId(int pedidoId)
        {
            return _service.GetPedidosComItens()
                           .FirstOrDefault(p => p.PedidoId == pedidoId);
        }

    }
}
