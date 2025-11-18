using CRM.Application.ModelResponse;
using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IPedidoAppService : IAppServiceBase<Pedido>
    {
        ResponseModel AddPedidoComItens(Pedido pedido);

        Pedido GetPedido(int leadId);

        Pedido GetPedidoId(int pedidoId);
    }
}
