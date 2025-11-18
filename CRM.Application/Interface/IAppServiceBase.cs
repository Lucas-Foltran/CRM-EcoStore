using CRM.Application.ModelResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IAppServiceBase<T> where T : class
    {
        public IQueryable<T> GetByOdata();
        public int CountAll();
        public T GetById(Guid id);
        public T GetById(int id);
        public T GetById(string id);
        public List<T> GetAll();

        public List<T> GetByFilter(Expression<Func<T, bool>> expressao);

        public ResponseModel Add(T tabelaGenerica);

        public ResponseModel Delete(T tabelaGenerica);

        public ResponseModel Update(T tabelaGenerica);

        public ResponseModel AddRange(List<T> lstTabelaGenerica);

        public ResponseModel UpdateRange(List<T> lstTabelaGenerica);

        public ResponseModel DeleteRange(List<T> lstTabelaGenerica);
    }
}
