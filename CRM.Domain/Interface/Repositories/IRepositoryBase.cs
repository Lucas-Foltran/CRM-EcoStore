using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        public IQueryable<T> GetByOdata();
        public T GetById(Guid Id);
        public T GetById(int Id);
        public T GetById(string Id);
        public int CountAll();
        public List<T> GetAll();
        public List<T> GetByFilter(Expression<Func<T, bool>> expressao);
        public T Add(T tabelaGenerica);
        public void Delete(T tabelaGenerica);
        public bool Update(T tabelaGenerica);
        public bool AddRange(List<T> lstTabelaGenerica);
        public bool UpdateRange(List<T> lstTabelaGenerica);
        public void DeleteRange(List<T> lstTabelaGenerica);
        public void  SaveChanges();
    }
}
