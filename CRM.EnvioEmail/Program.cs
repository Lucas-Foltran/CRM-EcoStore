using CRM.Domain.Interface.Repositories;
using CRM.Domain.Interface.Services;
using CRM.Domain.Services;
using CRM.Email;
using CRM.EnvioEmail;
using CRM.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//alterar para o caminho da API em seu pc
//Directory.SetCurrentDirectory("C:\\ProjetosTafner\\VotacaoCRM\\VotacaoCRM\\CRM.API");
//Directory.SetCurrentDirectory("C:\\Users\\camil\\OneDrive\\Documentos\\Projetos Tafner\\VotacaoCRM\\CRM.API");
//caminho na web
Directory.SetCurrentDirectory("E:\\Inetpub\\vhosts\\tafner.net.br\\crmapi.tafner.net.br\\");

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var leadService = serviceProvider.GetService<ILeadService>();

static void ConfigureServices(IServiceCollection services)
{
    services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    services.AddScoped<ILeadService, LeadService>();
    services.AddScoped<ILeadRepository, LeadRepository>();
}

Console.WriteLine("Iniciando envio de e-mail...");
EnvioEmail objEnvio = new EnvioEmail(leadService);
objEnvio.Enviar();
objEnvio.EnviarEmailBoleto();
Console.WriteLine("Envio finalizado");

