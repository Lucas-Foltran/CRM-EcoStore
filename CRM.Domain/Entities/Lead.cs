using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities
{
    public class Lead
    {
        [Key]
        public int leadId { get; set; }

        [Required]
        [StringLength(200)]
        public string nomeContato { get; set; }

        [StringLength(20)]
        public string? telefone1 { get; set; }

        [Required]
        [StringLength(200)]
        public string email { get; set; }

        [StringLength(30)]
        public string? formaPagto { get; set; }

        public bool? pgto { get; set; }

        [StringLength(20)]
        public string? status { get; set; }

        [StringLength(200)]
        public string? link { get; set; }

        [StringLength(6)]
        public string? token { get; set; }


        [StringLength(9)]
        public string? cep { get; set; }

        [StringLength(200)]
        public string? endereco { get; set; }

        [StringLength(10)]
        public string? numero { get; set; }

        [StringLength(30)]
        public string? complemento { get; set; }

        [StringLength(200)]
        public string? bairro { get; set; }

        [StringLength(2)]
        public string? estado { get; set; }

        [StringLength(100)]
        public string? cidade { get; set; }

        [StringLength(20)]
        public string? statusCadastro { get; set; }

        public bool adm { get; set; }

        public DateTime? dataUltimaAlteracao { get; set; }
        public DateTime? dataPagamento { get; set; }
        public DateTime? dataCadastro { get; set; }

        [NotMapped]
        public string? tokenAPI { get; set; }

        public Pedido? Pedido { get; set; }
    }
}
