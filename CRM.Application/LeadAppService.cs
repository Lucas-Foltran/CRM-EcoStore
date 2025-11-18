using CRM.Application.Interface;
using CRM.Application.ModelResponse;
using CRM.Application.Utils;
using CRM.Domain.Entities;
using CRM.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application
{
    public class LeadAppService : AppServiceBase<Lead>, ILeadAppService
    {
        protected readonly ILeadService _service;
        public LeadAppService(IServiceBase<Lead> serviceBase, ILeadService service) : base(serviceBase)
        {
            _service = service;
        }

        //public string CriarPedido(string pedido)
        //{
        //    return _service.CriarPedido(pedido);
        //}

        //public ResponseModel Adicionar(Lead objLead)
        //{
        //    try
        //    {
        //        string retorno = _service.Adicionar(objLead);
        //        return new ResponseModel(retorno == "Dados atualizados", retorno, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return new RestException(new Lead().GetType(), e).TratarErro();
        //    }
        //}

        //public ResponseModel Atualizar(Lead objLead)
        //{
        //    try
        //    {
        //        string retorno = _service.Atualizar(objLead);
        //        return new ResponseModel(retorno == "Dados atualizados", retorno, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return new RestException(new Lead().GetType(), e).TratarErro();
        //    }
        //}

        //public ResponseModel RealizarPagamento(Lead objLead)
        //{
        //    try
        //    {
        //        string retorno = _service.RealizarPagamento(objLead);
        //        return new ResponseModel(!retorno.StartsWith("Erro"), retorno, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return new RestException(new Lead().GetType(), e).TratarErro();
        //    }
        //}

        //public ResponseModel FinalizarCadastro(Lead objLead)
        //{
        //    try
        //    {
        //        string retorno = _service.FinalizarCadastro(objLead);
        //        return new ResponseModel(retorno == "Cadastro Finalizado", retorno, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return new RestException(new Lead().GetType(), e).TratarErro();
        //    }
        //}
    }
}
