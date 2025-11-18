using CRM.Domain.Entities;
using CRM.Domain.Interface.Repositories;
using CRM.Infra.Context;

namespace CRM.Infra.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        // Campo para contexto, caso precise usar diretamente
        private readonly ContextDb _context;

        // Construtor que recebe o contexto e repassa para a base
        public UsuarioRepository(ContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
