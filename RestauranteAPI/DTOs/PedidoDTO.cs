namespace RestauranteAPI.DTOs
{
    public class PedidoDTO
    {
        public string MesaId { get; set; }
        public List<ItemDTO> Itens { get; set; }
    }
}
