using System;
using System.Collections.Generic;

namespace CRM.Domain.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        // Relacionamento com o lead (caso você queira saber quem fez o pedido)
        public int LeadId { get; set; }

        // Propriedade de navegação para o Lead
        public Lead Lead { get; set; }

        // Data do pedido
        public DateTime Data { get; set; } = DateTime.Now;

        // Total (opcional, pode ser calculado também)
        public decimal Total { get; set; }

        public string Status { get; set; }

        // Navegação para os itens do pedido
        public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    }
}
