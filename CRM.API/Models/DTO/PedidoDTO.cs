namespace CRM.API.Models.DTO
{
    public class PedidoDTO
    {
        public int? PedidoId { get; set; }

        public int LeadId { get; set; }

        public string Data { get; set; } 

        public decimal Total { get; set; }

        public List<PedidoItemDTO> Itens { get; set; } = new List<PedidoItemDTO>();
    }

    public class PedidoItemDTO
    {
        public int? PedidoItemId { get; set; }

        public int? PedidoId { get; set; }

        public string NomeProduto { get; set; }

        public decimal PrecoUnitario { get; set; }

        public int Quantidade { get; set; }
    }
}
