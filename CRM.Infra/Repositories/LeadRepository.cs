using CRM.Domain.Entities;
using CRM.Domain.Interface.Repositories;
using CRM.Infra.Context;

namespace CRM.Infra.Repositories
{
    public class LeadRepository : RepositoryBase<Lead>, ILeadRepository
    {
        private readonly ContextDb _context;

        public LeadRepository(ContextDb context) : base(context)
        {
            _context = context;
        }

    }
}
