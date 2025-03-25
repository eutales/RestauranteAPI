namespace RestauranteAPI.Models
{
    public class Pedido
    {
        public string Id { get; set; }
        public string MesaId { get; set; }
        public List<Item> Itens { get; set; } = new List<Item>();
        public double Total { get; set; }
        public bool Fechado { get; set; }
    }
}
