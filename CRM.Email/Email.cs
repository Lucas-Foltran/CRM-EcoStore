

using CRM.Domain.Entities;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System.Diagnostics;
using System.Drawing;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRM.Email
{
    public class Email
    {
        public IConfigurationRoot Configuration { get; set; }

        public async void EnviarEmailEsqueciSenha(string nome, string email, string senha)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\EsqueciSenha.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                url += "/login";

                string body = path
                     .Replace("<%NomeDoUsuario%>", nome)
                     .Replace("<%Senha%>", senha)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailFrom), new EmailAddress(email), "TAFNER SMART VOTE: ESQUECI A SENHA", string.Empty,body);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async void EnviarEmailAlteraSenha(string nome, string email, string senha)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailTemplateResetaSenha.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                url += "/login";

                string body = path
                     .Replace("<%NomeDoUsuario%>", nome)
                    .Replace("<%Senha%>", senha)
                    .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailFrom), new EmailAddress(email), "TAFNER SMART VOTE: REDEFINIÇÃO DE SENHA", string.Empty, body);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void EnviarEmailNovoUsuario(string nome, string email, string senha)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailNovoUsuario.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                url += "/login";

                string body = path
                     .Replace("<%NomeDoUsuario%>", nome)
                     .Replace("<%login%>", email)
                     .Replace("<%Senha%>", senha)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailFrom), new EmailAddress(email), "TAFNER SMART VOTE: DADOS DE LOGIN", string.Empty, body);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void EnviarEmailNovoLead(string nomeContato,string link, string email, string token, int leadId)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailNovoLead.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")    
                    url = Configuration["urlSistemaLocal2"];
                else
                    url = Configuration["urlSistemaProd2"];

                string body = path
                     .Replace("<%nomeContato%>", nomeContato)
                     .Replace("<%leadId%>", leadId.ToString())
                     .Replace("<%link%>", link)
                     .Replace("<%token%>", token)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(email),
                $"TAFNER SMART VOTE: #{leadId} - BOAS-VINDAS",
                string.Empty,
                body
                );

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //EMAIL PAGAMENTO
        public async void EnviarEmailPagamento(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailPagamentoLead.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal2"];
                else
                    url = Configuration["urlSistemaProd"];

                url += $"?authenticateLead=true&leadId={lead.leadId}";

                string dataPrevista = lead.dataPrevista.HasValue ? lead.dataPrevista.Value.ToString("dd/MM/yyyy") : "Data não disponível";
                string qtdDias = lead.qtdDias.ToString();
                string valor = lead.valor.ToString("C");
                string qtdVotantes = lead.qtdVotantes.ToString();
                string sms = lead.sms ? "Sim" : "Não";
                string whatsApp = lead.whatsApp ? "Sim" : "Não";

                string body = path
                     .Replace("<%url%>", url)
                     .Replace("<%leadId%>", lead.leadId.ToString())
                     .Replace("<%cnpj%>", lead.cnpj)
                     .Replace("<%ie%>", lead.inscricaoEstadual)
                     .Replace("<%nomeFantasia%>", lead.nomeFantasia)
                     .Replace("<%razaoSocial%>", lead.razaoSocial)
                     .Replace("<%nomeContato%>", lead.nomeContato)
                     .Replace("<%cargo%>", lead.cargo)
                     .Replace("<%telefone1%>", lead.telefone1)
                     .Replace("<%telefone2%>", lead.telefone2)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%cep%>", lead.cep)
                     .Replace("<%endereco%>", lead.endereco)
                     .Replace("<%numero%>", lead.numero)
                     .Replace("<%bairro%>", lead.bairro)
                     .Replace("<%estado%>", lead.estado)
                     .Replace("<%cidade%>", lead.cidade)
                     .Replace("<%tipoVotacao%>", lead.tipoVotacao)
                     .Replace("<%tipoVotacao2%>", lead.tipoVotacao2)
                     .Replace("<%dataPrevista%>", dataPrevista)
                     .Replace("<%qtdDias%>", qtdDias)
                     .Replace("<%qtdVotantes%>", qtdVotantes)
                     .Replace("<%sms%>", sms)
                     .Replace("<%whatsApp%>", whatsApp)
                     .Replace("<%valor%>", valor)
                     .Replace("<%formaPagto%>", lead.formaPagto);

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(lead.email),
                $"TAFNER SMART VOTE: #{lead.leadId} - PAGAMENTO CONCLUÍDO",
                string.Empty,
                body
                );

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //EMAIL PAGAMENTO RECUSADO
        public async void EnviarEmailPagamentoRecusado(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailPagamentoRecusado.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal2"];
                else
                    url = Configuration["urlSistemaProd"];

                url += $"?authenticateLead=true&leadId={lead.leadId}";

                string dataPrevista = lead.dataPrevista.HasValue ? lead.dataPrevista.Value.ToString("dd/MM/yyyy") : "Data não disponível";
                string qtdDias = lead.qtdDias.ToString();
                string valor = lead.valor.ToString("C");
                string qtdVotantes = lead.qtdVotantes.ToString();
                string sms = lead.sms ? "Sim" : "Não";
                string whatsApp = lead.whatsApp ? "Sim" : "Não";

                string body = path
                     .Replace("<%url%>", url)
                     .Replace("<%leadId%>", lead.leadId.ToString())
                     .Replace("<%cnpj%>", lead.cnpj)
                     .Replace("<%ie%>", lead.inscricaoEstadual)
                     .Replace("<%nomeFantasia%>", lead.nomeFantasia)
                     .Replace("<%razaoSocial%>", lead.razaoSocial)
                     .Replace("<%nomeContato%>", lead.nomeContato)
                     .Replace("<%cargo%>", lead.cargo)
                     .Replace("<%telefone1%>", lead.telefone1)
                     .Replace("<%telefone2%>", lead.telefone2)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%cep%>", lead.cep)
                     .Replace("<%endereco%>", lead.endereco)
                     .Replace("<%numero%>", lead.numero)
                     .Replace("<%bairro%>", lead.bairro)
                     .Replace("<%estado%>", lead.estado)
                     .Replace("<%cidade%>", lead.cidade)
                     .Replace("<%tipoVotacao%>", lead.tipoVotacao)
                     .Replace("<%tipoVotacao2%>", lead.tipoVotacao2)
                     .Replace("<%dataPrevista%>", dataPrevista)
                     .Replace("<%qtdDias%>", qtdDias)
                     .Replace("<%qtdVotantes%>", qtdVotantes)
                     .Replace("<%sms%>", sms)
                     .Replace("<%whatsApp%>", whatsApp)
                     .Replace("<%valor%>", valor)
                     .Replace("<%formaPagto%>", lead.formaPagto);

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(lead.email),
                $"TAFNER SMART VOTE: #{lead.leadId} - PAGAMENTO RECUSADO",
                string.Empty,
                body
                );

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void EnviarEmailCadastro(string nomeContato, string usuario1, string email1, string usuario2, string email2, string email, int leadId, string tipoVotacao2)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailCadastroUsers.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                // Defina os valores e os displays dinamicamente
                string displayUsuario2 = string.IsNullOrEmpty(usuario2) ? "display: none;" : "display: table-row;";
                string displayEmail2 = string.IsNullOrEmpty(email2) ? "display: none;" : "display: table-row;";
                string displayButton = string.IsNullOrEmpty(email2) ? "none" : "table-row";

                string body = path
                 .Replace("<%nomeContato%>", nomeContato)
                 .Replace("<%leadId%>", leadId.ToString())
                 .Replace("<%tipoVotacao2%>", tipoVotacao2)
                 .Replace("<%usuario1%>", usuario1 ?? "")
                 .Replace("<%email1%>", email1 ?? "")
                 .Replace("<%usuario2%>", usuario2 ?? "")
                 .Replace("<%email2%>", email2 ?? "")
                 .Replace("<%displayUsuario2%>", displayUsuario2)
                 .Replace("<%displayEmail2%>", displayEmail2)
                 .Replace("<%displayButton%>", displayButton);

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(email),
                $"TAFNER SMART VOTE: #{leadId} - CADASTRO CONCLUÍDO",
                string.Empty,
                body
                );
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void EnviarEmailNovoToken(string nomeContato, string link, string token, string email)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailNovoToken.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                string body = path
                     .Replace("<%nomeContato%>", nomeContato)
                     .Replace("<%link%>", link)
                     .Replace("<%token%>", token)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailFrom), new EmailAddress(email), "TAFNER SMART VOTE: REDEFINIÇÃO DE TOKEN", string.Empty, body);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //enviar email para contato@tafner.net.br
        public async void EnviarEmailInformacaoLead(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailInformacoesLead.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal2"];
                else
                    url = Configuration["urlSistemaProd2"];

                string dataPrevista = lead.dataPrevista.HasValue ? lead.dataPrevista.Value.ToString("dd/MM/yyyy") : "Data não disponível";
                string qtdDias = lead.qtdDias.ToString();
                string leadId = lead.leadId.ToString();
                string valor = lead.valor.ToString("C");
                string qtdVotantes = lead.qtdVotantes.ToString();
                string sms = lead.sms ? "Sim" : "Não";
                string whatsApp = lead.whatsApp ? "Sim" : "Não";

                string body = path
                     .Replace("<%leadId%>", leadId)
                     .Replace("<%cnpj%>", lead.cnpj)
                     .Replace("<%ie%>", lead.inscricaoEstadual)
                     .Replace("<%nomeFantasia%>", lead.nomeFantasia)
                     .Replace("<%razaoSocial%>", lead.razaoSocial)
                     .Replace("<%nomeContato%>", lead.nomeContato)
                     .Replace("<%cargo%>", lead.cargo)
                     .Replace("<%telefone1%>", lead.telefone1)
                     .Replace("<%telefone2%>", lead.telefone2)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%cep%>", lead.cep)
                     .Replace("<%endereco%>", lead.endereco)
                     .Replace("<%numero%>", lead.numero)
                     .Replace("<%bairro%>", lead.bairro)
                     .Replace("<%estado%>", lead.estado)
                     .Replace("<%cidade%>", lead.cidade)
                     .Replace("<%tipoVotacao%>", lead.tipoVotacao)
                     .Replace("<%tipoVotacao2%>", lead.tipoVotacao2)
                     .Replace("<%dataPrevista%>", dataPrevista)
                     .Replace("<%qtdDias%>", qtdDias)
                     .Replace("<%qtdVotantes%>", qtdVotantes)
                     .Replace("<%sms%>", sms)
                     .Replace("<%whatsApp%>", whatsApp)
                     .Replace("<%valor%>", valor)
                     .Replace("<%formaPagto%>", lead.formaPagto);

                string emailEnvio = "contato@tafner.net.br";

                if (Configuration["chave"].ToString() == "H")
                    emailEnvio = lead.email;

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(emailEnvio),
                $"TAFNER SMART VOTE: #{lead.leadId} - NOVO PEDIDO", 
                string.Empty,
                body
                );

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarEmailCarroAbandonado(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailCarroAbandonado.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                url += $"?authenticateLead=true&leadId={lead.leadId}";

                string leadId = lead.leadId.ToString();

                string body = path
                     .Replace("<%leadId%>", leadId)
                     .Replace("<%nomeContato%>", lead.nomeContato)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailFrom), new EmailAddress(lead.email), "TAFNER SMART VOTE – Sua votação online te espera!", string.Empty, body);

                var response = client.SendEmailAsync(msg).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
                else
                    Console.WriteLine("aqui: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarEmailBoleto(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailBoleto.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal"];
                else
                    url = Configuration["urlSistemaProd"];

                url += "/login";

                string leadId = lead.leadId.ToString();

                string body = path
                     .Replace("<%leadId%>", leadId)
                     .Replace("<%linkPdf%>", lead.linkPdf)
                     .Replace("<%linkBarCode%>", lead.linkBarCode)
                     .Replace("<%codigoBarrasBoleto%>", lead.codigoBarrasBoleto)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%url%>", url);

                var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailFrom),
                new EmailAddress(lead.email),
                $"TAFNER SMART VOTE: #{lead.leadId} - PAGAMENTO PENDENTE",
                string.Empty,
                body
                );
                var response = client.SendEmailAsync(msg).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
                else
                    Console.WriteLine("aqui: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //EMAIL PAGAMENTO PARA ADMIN
        public async void EnviarEmailPagamentoConfirmado(Lead lead)
        {
            try
            {
                string path = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\EmailTemplate\\emailPagamentoConfirmado.html");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                string apiKey = Configuration["keySendGrid"];
                var client = new SendGridClient(apiKey);
                string emailFrom = Configuration["emailFrom"];

                string ambiente = Configuration["ambiente"];
                string url = "";

                if (ambiente == "L")
                    url = Configuration["urlSistemaLocal2"];
                else
                    url = Configuration["urlSistemaProd2"];

                string dataPrevista = lead.dataPrevista.HasValue ? lead.dataPrevista.Value.ToString("dd/MM/yyyy") : "Data não disponível";
                string qtdDias = lead.qtdDias.ToString();
                string valor = lead.valor.ToString("C");
                string qtdVotantes = lead.qtdVotantes.ToString();
                string sms = lead.sms ? "Sim" : "Não";
                string whatsApp = lead.whatsApp ? "Sim" : "Não";

                string body = path
                     .Replace("<%url%>", url)
                     .Replace("<%leadId%>", lead.leadId.ToString())
                     .Replace("<%cnpj%>", lead.cnpj)
                     .Replace("<%ie%>", lead.inscricaoEstadual)
                     .Replace("<%nomeFantasia%>", lead.nomeFantasia)
                     .Replace("<%razaoSocial%>", lead.razaoSocial)
                     .Replace("<%nomeContato%>", lead.nomeContato)
                     .Replace("<%cargo%>", lead.cargo)
                     .Replace("<%telefone1%>", lead.telefone1)
                     .Replace("<%telefone2%>", lead.telefone2)
                     .Replace("<%email%>", lead.email)
                     .Replace("<%cep%>", lead.cep)
                     .Replace("<%endereco%>", lead.endereco)
                     .Replace("<%numero%>", lead.numero)
                     .Replace("<%bairro%>", lead.bairro)
                     .Replace("<%estado%>", lead.estado)
                     .Replace("<%cidade%>", lead.cidade)
                     .Replace("<%tipoVotacao%>", lead.tipoVotacao)
                     .Replace("<%tipoVotacao2%>", lead.tipoVotacao2)
                     .Replace("<%dataPrevista%>", dataPrevista)
                     .Replace("<%qtdDias%>", qtdDias)
                     .Replace("<%qtdVotantes%>", qtdVotantes)
                     .Replace("<%sms%>", sms)
                     .Replace("<%whatsApp%>", whatsApp)
                     .Replace("<%valor%>", valor)
                     .Replace("<%formaPagto%>", lead.formaPagto);


                List<EmailAddress> lista = new List<EmailAddress>();

                if (Configuration["chave"].ToString() == "H")
                    lista.Add(new EmailAddress(lead.email));
                else
                {
                    lista.Add(new EmailAddress("comercial@tafner.net.br"));
                    lista.Add(new EmailAddress("adm@tafner.net.br"));
                }

                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                new EmailAddress(emailFrom),
                lista,
                $"TAFNER SMART VOTE: #{lead.leadId} - PAGAMENTO CONCLUÍDO",
                string.Empty,
                body
                );

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    Console.WriteLine("E-mail enviado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}