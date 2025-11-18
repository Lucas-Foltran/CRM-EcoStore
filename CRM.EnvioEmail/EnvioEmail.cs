using CRM.Domain.Entities;
using CRM.Domain.Interface.Services;
using CRM.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.EnvioEmail
{
    public class EnvioEmail
    {
        public readonly ILeadService _leadService;

        public EnvioEmail(ILeadService leadService)
        {
            _leadService = leadService;
        }

        public void Enviar()
        {
            try
            {
                string caminhoTemplate = Directory.GetCurrentDirectory() + "\\EmailTemplate\\emailCarroAbandonado.html";
                var dataAtual = DateTime.Now.Date;

                // Data limite para considerar leads: três dias atrás
                var dataInicio = dataAtual.AddDays(-3);
                var leads = _leadService.GetByFilter(l => l.status == "Aberto"
                                                                  && l.statusCadastro == "Aguardando Pagamento"
                                                                  && l.dataCadastro.HasValue
                                                                  && l.dataCadastro.Value.Date >= dataInicio
                                                                  && l.dataCadastro.Value.Date <= dataAtual
                                                                  && l.dataVencimentoBoleto == null);
                foreach (var lead in leads)
                {
                    // Calcula a diferença em dias entre a data de cadastro e a data atual
                    var diasDesdeCadastro = (dataAtual - lead.dataCadastro.Value.Date).Days;

                    // Envia e-mail apenas se a diferença for menor que 3 dias
                    if (diasDesdeCadastro >= 1 && diasDesdeCadastro <= 3)
                    {
                        new Email.Email().EnviarEmailCarroAbandonado(lead);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarEmailBoleto()
        {
            try
            {
                string caminhoTemplate = Directory.GetCurrentDirectory() + "\\EmailTemplate\\emailBoleto.html";
                var leads = _leadService.GetByFilter(l => l.status == "Aberto" && l.statusCadastro == "Aguardando Pagamento" && l.dataVencimentoBoleto != null);

                foreach (var lead in leads)
                {
                    // Calcula a data de criação do boleto
                    var dataCriacaoBoleto = lead.dataVencimentoBoleto.Value.Date.AddDays(-3);

                    // Verifica se a data de cadastro e a data de criação do boleto são iguais
                    if (lead.dataCadastro.Value.Date == dataCriacaoBoleto)
                    {
                        continue; // Se forem iguais, não envia o e-mail e continua para o próximo lead
                    }

                    // Calcula a diferença entre a data de vencimento e a data atual
                    var diasParaVencimento = (lead.dataVencimentoBoleto.Value.Date - DateTime.Now.Date).Days;

                    // Se faltam 3 dias ou menos para o vencimento, envia o e-mail
                    if (diasParaVencimento >= 1 && diasParaVencimento <= 3)
                    {
                        new Email.Email().EnviarEmailBoleto(lead);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
