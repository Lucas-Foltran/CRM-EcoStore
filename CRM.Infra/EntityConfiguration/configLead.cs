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
    public class configLead : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.HasKey(x => x.leadId);
            builder.Property(x => x.nomeContato).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.telefone1).HasColumnType("varchar(20)");
            builder.Property(x => x.email).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.formaPagto).HasColumnType("varchar(30)");
            builder.Property(x => x.pgto);
            builder.Property(x => x.status).HasColumnType("varchar(20)");
            builder.Property(x => x.link).HasColumnType("varchar(200)");
            builder.Property(x => x.token).HasColumnType("varchar(6)");
            builder.Property(x => x.cep).HasColumnType("varchar(9)");
            builder.Property(x => x.endereco).HasColumnType("varchar(200)");
            builder.Property(x => x.numero).HasColumnType("varchar(10)");
            builder.Property(x => x.complemento).HasColumnType("varchar(30)");
            builder.Property(x => x.bairro).HasColumnType("varchar(200)");
            builder.Property(x => x.estado).HasColumnType("varchar(2)");
            builder.Property(x => x.cidade).HasColumnType("varchar(100)");
            builder.Property(x => x.statusCadastro).HasColumnType("varchar(20)");
            builder.Property(x => x.adm).HasColumnType("bit");
            builder.Property(x => x.dataUltimaAlteracao);
            builder.Property(x => x.dataPagamento);
            builder.Property(x => x.dataCadastro);
        }
    }
}
