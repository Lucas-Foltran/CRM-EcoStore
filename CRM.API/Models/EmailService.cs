using CRM.Domain.Entities;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace CRM.API.Models
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailSender = "ecostore.tcc@gmail.com";
        private readonly string _emailPassword = "sudjkqhpfckkcuix";

        public void EnviarEmailNovoUsuario(string nomeUsuario, string email, string senha, string destinatario)
        {
            try
            {
                var corpoEmail = File.ReadAllText("C:\\Users\\lucas\\OneDrive\\Área de Trabalho\\TCC\\CRM_TCC\\CRM_API\\CRM.API\\EmailTemplate\\emailNovoUsuario.html")
                    .Replace("<%NomeDoUsuario%>", nomeUsuario)
                    .Replace("<%email%>", email)
                    .Replace("<%Senha%>", senha);

                var message = new MailMessage
                {
                    From = new MailAddress(_emailSender),
                    Subject = "Bem-vindo ao EcoStore!",
                    Body = corpoEmail,
                    IsBodyHtml = true
                };
                message.To.Add(destinatario);

                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_emailSender, _emailPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o email: {ex.Message}");
                throw;
            }
        }

        public void EnviarEmailNovoLead(string nomeContato, string link, string email, string token, int leadId)
        {
            try
            {
                var templatePath = "C:\\Users\\lucas\\OneDrive\\Área de Trabalho\\TCC\\CRM_TCC\\CRM_API\\CRM.API\\EmailTemplate\\emailNovoLead.html";
                var corpoEmail = File.ReadAllText(templatePath)
                    .Replace("<%NomeContato%>", nomeContato)
                    .Replace("<%Link%>", link)
                    .Replace("<%Email%>", email)
                    .Replace("<%Token%>", token)
                    .Replace("<%LeadId%>", leadId.ToString());

                var message = new MailMessage
                {
                    From = new MailAddress(_emailSender),
                    Subject = "Boas Vindas- EcoStore",
                    Body = corpoEmail,
                    IsBodyHtml = true
                };
                message.To.Add(email);

                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_emailSender, _emailPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o email de novo lead: {ex.Message}");
                throw;
            }
        }

        public void EnviarEmailInformacaoLead(Lead lead, IEnumerable<string> destinatarios)
        {
            try
            {
                var templatePath = "C:\\Users\\lucas\\OneDrive\\Área de Trabalho\\TCC\\CRM_TCC\\CRM_API\\CRM.API\\EmailTemplate\\emailInformacaoLead.html";
                var corpoEmail = File.ReadAllText(templatePath)
                    .Replace("<%NomeContato%>", lead.nomeContato)
                    .Replace("<%Telefone1%>", lead.telefone1)
                    .Replace("<%Email%>", lead.email)
                    .Replace("<%CEP%>", lead.cep)
                    .Replace("<%Endereco%>", lead.endereco)
                    .Replace("<%Numero%>", lead.numero.ToString())
                    .Replace("<%Bairro%>", lead.bairro)
                    .Replace("<%Estado%>", lead.estado)
                    .Replace("<%Cidade%>", lead.cidade)
                    .Replace("<%Complemento%>", lead.complemento ?? "");

                var message = new MailMessage
                {
                    From = new MailAddress(_emailSender),
                    Subject = "Informações do Lead - EcoStore",
                    Body = corpoEmail,
                    IsBodyHtml = true
                };

                // Adiciona todos os destinatários
                foreach (var email in destinatarios)
                {
                    message.To.Add(email);
                }

                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_emailSender, _emailPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o email de informações do lead: {ex.Message}");
                throw;
            }
        }

        public void EnviarEmailConfirmacaoPedido(Pedido pedido, IEnumerable<string> destinatarios)
        {
            try
            {
                if (pedido == null || pedido.Itens == null || !pedido.Itens.Any())
                    return;

                var lead = pedido.Lead;
                if (lead == null) return;

                // Caminho do template
                var templatePath = "C:\\Users\\lucas\\OneDrive\\Área de Trabalho\\TCC\\CRM_TCC\\CRM_API\\CRM.API\\EmailTemplate\\emailPagamento.html";

                var corpoEmail = File.ReadAllText(templatePath)
                    .Replace("<%NomeContato%>", lead.nomeContato)
                    .Replace("<%Email%>", lead.email)
                    .Replace("<%Telefone1%>", lead.telefone1)
                    .Replace("<%CEP%>", lead.cep)
                    .Replace("<%Endereco%>", lead.endereco)
                    .Replace("<%Numero%>", lead.numero.ToString())
                    .Replace("<%Bairro%>", lead.bairro)
                    .Replace("<%Cidade%>", lead.cidade)
                    .Replace("<%Estado%>", lead.estado)
                    .Replace("<%Complemento%>", lead.complemento ?? "")
                    .Replace("<%DataPedido%>", pedido.Data.ToString("dd/MM/yyyy HH:mm"))
                    .Replace("<%Total%>", pedido.Total.ToString("N2"));

                // Montar lista de itens
                var itensTexto = string.Join("", pedido.Itens.Select(i =>
                 $"<p><strong>{i.NomeProduto}</strong><br/>" +
                 $"Quantidade: {i.Quantidade}<br/>" +
                 $"Preço unitário: R$ {i.PrecoUnitario:N2}</p>"
                 ));
                corpoEmail = corpoEmail.Replace("<%ItensPedido%>", itensTexto);

                // Cria a mensagem
                var message = new MailMessage
                {
                    From = new MailAddress(_emailSender),
                    Subject = "Novo Pagamento Recebido - EcoStore",
                    Body = corpoEmail,
                    IsBodyHtml = true
                };

                // Adiciona todos os destinatários
                foreach (var email in destinatarios)
                {
                    message.To.Add(email);
                }

                // Envio via SMTP
                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_emailSender, _emailPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o email de confirmação do pedido: {ex.Message}");
                throw;
            }
        }


    }
}
