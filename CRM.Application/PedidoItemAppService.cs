using CRM.Application.Interface;
using CRM.Application.ModelResponse;
using CRM.Application.Utils;
using CRM.Domain.Entities;
using CRM.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application
{
    public class PedidoItemAppService : AppServiceBase<PedidoItem>, IPedidoItemAppService
    {
        protected readonly IPedidoItemService _service;
        public PedidoItemAppService(IServiceBase<PedidoItem> serviceBase, IPedidoItemService service) : base(serviceBase)
        {
            _service = service;
        }

        
    }
}
