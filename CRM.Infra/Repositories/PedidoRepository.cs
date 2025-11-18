using CRM.Domain.Entities;
using CRM.Domain.Interface.Repositories;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CRM.Infra.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly ContextDb _context; 
        public IConfigurationRoot? Configuration { get; set; }

        // Recebe o contexto via injeção de dependência
        public PedidoRepository(ContextDb context) : base(context)
        {
            _context = context; 
        }

        // Método para obter pedidos com itens carregados
        public IQueryable<Pedido> GetPedidosComItens()
        {
            return _context.Pedido
                    .Include(p => p.Itens)
                    .Include(p => p.Lead);
        }
    }
}
