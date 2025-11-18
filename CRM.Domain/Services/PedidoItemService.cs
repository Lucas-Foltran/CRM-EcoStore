using CRM.Domain.Entities;
using CRM.Domain.Interface.Repositories;
using CRM.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Services
{
    public class PedidoItemService : ServiceBase<PedidoItem>, IPedidoItemService
    {
        protected readonly IPedidoItemRepository _pedidoItem;

        public PedidoItemService(IRepositoryBase<PedidoItem> irepositoryBase, IPedidoItemRepository pedidoItem) : base(irepositoryBase)
        {
            _pedidoItem = pedidoItem;
        }

    }
}
