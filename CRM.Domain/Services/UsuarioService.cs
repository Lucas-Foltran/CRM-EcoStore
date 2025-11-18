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
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        protected readonly IUsuarioRepository _usuario;

        public UsuarioService(IRepositoryBase<Usuario> irepositoryBase, IUsuarioRepository usuario) : base(irepositoryBase)
        {
            _usuario = usuario;
        }

    }
}
