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
    public class PedidoItem
    {
        public int PedidoItemId { get; set; }

        // Chave estrangeira para o Pedido
        public int PedidoId { get; set; }

        // Nome do produto (pode ser só o nome ou ter um relacionamento com uma tabela de produtos depois)
        public string NomeProduto { get; set; }

        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }

        // Navegação opcional
        public Pedido Pedido { get; set; }
    }
}
