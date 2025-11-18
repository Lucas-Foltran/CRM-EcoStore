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
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        protected readonly IPedidoRepository _pedido;

        public PedidoService(IRepositoryBase<Pedido> irepositoryBase, IPedidoRepository pedido) : base(irepositoryBase)
        {
            _pedido = pedido;
        }

        public IQueryable<Pedido> GetPedidosComItens()
        {
            return _pedido.GetPedidosComItens();
        }
    }
}
