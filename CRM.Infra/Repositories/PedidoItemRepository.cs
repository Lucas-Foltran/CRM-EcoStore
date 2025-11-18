using CRM.Domain.Entities;
using CRM.Domain.Interface.Repositories;
using CRM.Infra.Context;

namespace CRM.Infra.Repositories
{
    public class PedidoItemRepository : RepositoryBase<PedidoItem>, IPedidoItemRepository
    {
        private readonly ContextDb _context;

        public PedidoItemRepository(ContextDb context) : base(context)
        {
            _context = context;
        }

    }
}
