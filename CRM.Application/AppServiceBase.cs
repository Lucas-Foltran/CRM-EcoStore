using CRM.Application.Interface;
using CRM.Application.ModelResponse;
using CRM.Application.Utils;
using CRM.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application
{
    public class AppServiceBase<T> : IAppServiceBase<T> where T : class
    {
        private readonly IServiceBase<T> _serviceBase;

        public AppServiceBase(IServiceBase<T> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public T GetById(Guid id)
        {
            try
            {
                return _serviceBase.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public T GetById(string id)
        {
            try
            {
                return _serviceBase.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _serviceBase.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CountAll()
        {
            return _serviceBase.CountAll();
        }
        public IQueryable<T> GetByOdata()
        {
            return _serviceBase.GetByOdata();
        }

        public List<T> GetAll()
        {
            try
            {
                return _serviceBase.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> expressao)
        {
            return _serviceBase.GetByFilter(expressao);
        }

        public ResponseModel Add(T entity)
        {
            ResponseModel resposta = new ResponseModel();
            try
            {
                _serviceBase.Add(entity);

                string message = "Cadastrado com Sucesso";

                resposta.Success = true;
                resposta.Error = false;
                resposta.Message = message;

                return resposta;
            }
            catch (Exception e)
            {
                return new RestException(entity.GetType(), e).TratarErro();
            }

        }

        public ResponseModel Delete(T tabelaGenerica)
        {
            ResponseModel retorno = new ResponseModel();

            try
            {
                _serviceBase.Delete(tabelaGenerica);

                retorno.Success = true;
                retorno.Error = false;
                retorno.Message = "Registro Excluído";

                return retorno;
            }
            catch (Exception e)
            {
                return new RestException(tabelaGenerica.GetType(), e).TratarErro();
            }
        }

        public ResponseModel Update(T tabelaGenerica)
        {
            ResponseModel retorno = new ResponseModel();

            try
            {
                _serviceBase.Update(tabelaGenerica);

                retorno.Success = true;
                retorno.Error = false;
                retorno.Message = "Registro Atualizado";

                return retorno;
            }
            catch (Exception e)
            {
                return new RestException(tabelaGenerica.GetType(), e).TratarErro();
            }
        }

        public ResponseModel AddRange(List<T> lstTabelaGenerica)
        {
            ResponseModel retorno = new ResponseModel();

            try
            {
                _serviceBase.AddRange(lstTabelaGenerica);

                retorno.Success = true;
                retorno.Error = false;
                retorno.Message = "Lista de Registro Adicionada";

                return retorno;
            }
            catch (Exception e)
            {
                return new RestException(lstTabelaGenerica.GetType(), e).TratarErro();
            }
        }

        public ResponseModel UpdateRange(List<T> lstTabelaGenerica)
        {
            ResponseModel retorno = new ResponseModel();

            try
            {
                _serviceBase.UpdateRange(lstTabelaGenerica);

                retorno.Success = true;
                retorno.Error = false;
                retorno.Message = "Lista de Registro Atualizado";

                return retorno;
            }
            catch (Exception e)
            {
                return new RestException(lstTabelaGenerica.GetType(), e).TratarErro();
            }
        }

        public ResponseModel DeleteRange(List<T> lstTabelaGenerica)
        {
            ResponseModel retorno = new ResponseModel();

            try
            {
                _serviceBase.DeleteRange(lstTabelaGenerica);

                retorno.Success = true;
                retorno.Error = false;
                retorno.Message = "Lista de Registro Excluído";

                return retorno;
            }
            catch (Exception e)
            {
                return new RestException(lstTabelaGenerica.GetType(), e).TratarErro();
            }
        }
    }
}
