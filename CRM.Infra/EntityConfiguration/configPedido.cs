using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.EntityConfiguration
{
    public class configPedido : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.PedidoId);

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.Total)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
               .IsRequired()
               .HasMaxLength(50)
               .HasDefaultValue("Aguardando Pagamento");

            // Relacionamento 1:N Pedido -> Itens
            builder.HasMany(x => x.Itens)
                .WithOne(x => x.Pedido)
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Cascade); // Para deletar itens quando pedido for deletado

            // Relacionamento 1:1 (ou 0..1) Pedido -> Lead
            builder.HasOne(x => x.Lead)
                .WithOne(l => l.Pedido)
                .HasForeignKey<Pedido>(x => x.LeadId)
                .IsRequired() // Pedido deve ter Lead
                .OnDelete(DeleteBehavior.Restrict); // Evita deletar Lead ao deletar pedido

            // Garantir que LeadId seja único para garantir 1:1 (um pedido por lead)
            builder.HasIndex(x => x.LeadId).IsUnique();
        }
    }
}
