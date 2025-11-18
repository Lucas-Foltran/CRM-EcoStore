using CRM.Domain.Interface.Repositories;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infra.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        //protected readonly ContextDb db = new ContextDb(null);

        protected readonly ContextDb db;

        // Construtor que recebe o ContextDb - para injeção de dependência
        public RepositoryBase(ContextDb context)
        {
            db = context;
        }

        public IConfigurationRoot? Configuration { get; set; }

        public string retornaUrlAPI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetSection("urlAPI").Value;
        }

        public IQueryable<T> GetByOdata()
        {
            return db.Set<T>().AsQueryable();
        }
        public int CountAll()
        {
            int count = db.Set<T>().AsNoTracking().Count();
            return count;
        }

        public T GetById(Guid Id)
        {
            try
            {
                var retorno = db.Set<T>().Find(Id);

                db.Entry(retorno).State = EntityState.Detached;

                return retorno;
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
                var retorno = db.Set<T>().Find(Id);

                db.Entry(retorno).State = EntityState.Detached;

                return retorno;
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
                var retorno = db.Set<T>().Find(Id);

                db.Entry(retorno).State = EntityState.Detached;

                return retorno;
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
                return db.Set<T>().AsNoTracking().ToList();
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
                return db.Set<T>().Where(expressao).AsNoTracking().ToList();
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
                db.Set<T>().Add(tabelaGenerica);
                db.SaveChanges();

                return tabelaGenerica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T tabelaGenerica)
        {
            try
            {
                db.Set<T>().Remove(tabelaGenerica);
                db.SaveChanges();
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
                db.Entry(tabelaGenerica).State = EntityState.Modified;
                db.SaveChanges();
                return true;
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
                db.Set<T>().AddRange(lstTabelaGenerica);
                db.SaveChanges();

                return true;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateRange(List<T> lstTabelaGenerica)
        {
            try
            {
                db.Set<T>().UpdateRange(lstTabelaGenerica);
                db.SaveChanges(true);

                return true;
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
                db.Set<T>().RemoveRange(lstTabelaGenerica);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
