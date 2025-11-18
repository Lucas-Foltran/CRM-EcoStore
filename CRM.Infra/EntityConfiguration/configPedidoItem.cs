using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.EntityConfiguration
{
    public class configPedidoItem : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            // Chave primária
            builder.HasKey(x => x.PedidoItemId);

            // Propriedades
            builder.Property(x => x.NomeProduto)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.PrecoUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .IsRequired();

            // Relacionamento PedidoItem -> Pedido (muitos para 1)
            builder.HasOne(x => x.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
