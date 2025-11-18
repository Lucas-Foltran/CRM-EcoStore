using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        IEnumerable<Usuario> GetTodosUsuarios();
    }
}
