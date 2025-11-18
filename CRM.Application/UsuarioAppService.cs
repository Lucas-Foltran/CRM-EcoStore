using CRM.Application.Interface;
using CRM.Domain.Entities;
using CRM.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        protected readonly IUsuarioService _service;
        public UsuarioAppService(IServiceBase<Usuario> serviceBase, IUsuarioService service) : base(serviceBase)
        {
            _service = service;
        }

        public IEnumerable<Usuario> GetTodosUsuarios()
        {
            return _service.GetAll();
        }
    }
}
