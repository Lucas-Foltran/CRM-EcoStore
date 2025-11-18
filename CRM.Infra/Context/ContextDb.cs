using CRM.Domain.Entities;
using CRM.Infra.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRM.Infra.Context
{
    public class ContextDb : DbContext
    {
        public IConfigurationRoot? Configuration { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Lead> Lead { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }
        public ContextDb()
        {
        }
        public ContextDb(DbContextOptions<ContextDb> option) : base(option = new DbContextOptions<ContextDb>())
        {
            Database.EnsureCreated();
            Database.SetCommandTimeout(200);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer(retornaUrlConexao());
            }
        }

        public string retornaUrlConexao()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("con");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new configUsuario());
            modelBuilder.ApplyConfiguration(new configLead());
            modelBuilder.ApplyConfiguration(new configPedido());
            modelBuilder.ApplyConfiguration(new configPedidoItem());

        }
    }
}
