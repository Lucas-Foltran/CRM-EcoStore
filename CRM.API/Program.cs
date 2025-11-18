using CRM.API.Extensions;
using CRM.API.Models;
using CRM.Application;
using CRM.Application.Interface;
using CRM.Domain.Interface.Repositories;
using CRM.Domain.Interface.Services;
using CRM.Domain.Services;
using CRM.Infra.Context;
using CRM.Infra.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });



// Configurar Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM API", Version = "v1" });
});
builder.Services.AddDbContext<ContextDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("con"));
});

// Adicionando Serilog para logging
SerilogExtension.AddSerilog(builder.Configuration);
builder.Logging.AddSerilog(Log.Logger);

// Registrando serviços
builder.Services.AddScoped(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
builder.Services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILeadAppService, LeadAppService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();

builder.Services.AddScoped<IPedidoAppService, PedidoAppService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();       
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<IPedidoItemAppService, PedidoItemAppService>();
builder.Services.AddScoped<IPedidoItemService, PedidoItemService>();
builder.Services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddProblemDetails();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var app = builder.Build();

// Configuração de handler de exceção personalizada
app.ConfigureExceptionHandler(app.Environment);

// Configuração de CORS
app.UseCors(builder =>
{
    builder
    .WithOrigins("http://localhost:4200")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Rodando a API
app.Run();
