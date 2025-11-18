using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infra.EntityConfiguration
{
    public class configUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.UsuarioId);
            builder.Property(x => x.nome).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.sobrenome).HasColumnType("varchar(200)");
            builder.Property(x => x.email).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.senha).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.status).IsRequired().HasColumnType("bit");
            builder.Property(x => x.adm).HasColumnType("bit");

        }
    }
}
