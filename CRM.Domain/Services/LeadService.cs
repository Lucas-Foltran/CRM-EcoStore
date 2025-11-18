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
    public class LeadService : ServiceBase<Lead>, ILeadService
    {
        protected readonly ILeadRepository _lead;

        public LeadService(IRepositoryBase<Lead> irepositoryBase, ILeadRepository lead) : base(irepositoryBase)
        {
            _lead = lead;
        }

        //public string CriarPedido(string pedido)
        //{
        //    return _lead.CriarPedido(pedido);
        //}

        //public string Adicionar(Lead objLead)
        //{
        //    try
        //    {
        //        return _lead.Adicionar(objLead);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string Atualizar(Lead objLead)
        //{
        //    try
        //    {
        //        return _lead.Atualizar(objLead);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string RealizarPagamento(Lead objLead)
        //{
        //    try
        //    {
        //        return _lead.RealizarPagamento(objLead);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string FinalizarCadastro(Lead objLead)
        //{
        //    try
        //    {
        //        return _lead.FinalizarCadastro(objLead);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
