using CRM.Domain.Interface.Repositories;
using CRM.Domain.Interface.Services;
using System.Linq.Expressions;

namespace CRM.Domain.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repositoryBase;

        public int CountAll()
        {
            return _repositoryBase.CountAll();
        }

        public IQueryable<T> GetByOdata()
        {
            return _repositoryBase.GetByOdata();
        }

        public ServiceBase(IRepositoryBase<T> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public T GetById(Guid Id)
        {
            try
            {
                return _repositoryBase.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T GetById(int Id)
        {
            try
            {
                return _repositoryBase.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T GetById(string Id)
        {
            try
            {
                return _repositoryBase.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _repositoryBase.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> expressao)
        {
            try
            {
                return _repositoryBase.GetByFilter(expressao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Add(T tabelaGenerica)
        {
            try
            {
                return _repositoryBase.Add(tabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(T tabelaGenerica)
        {
            try
            {
                _repositoryBase.Delete(tabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(T tabelaGenerica)
        {
            try
            {
                return _repositoryBase.Update(tabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddRange(List<T> lstTabelaGenerica)
        {
            try
            {
                return _repositoryBase.AddRange(lstTabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateRange(List<T> lstTabelaGenerica)
        {
            try
            {
                return _repositoryBase.UpdateRange(lstTabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteRange(List<T> lstTabelaGenerica)
        {
            try
            {
                _repositoryBase.DeleteRange(lstTabelaGenerica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveChanges()
        {
            _repositoryBase.SaveChanges(); 
        }
    }
}
