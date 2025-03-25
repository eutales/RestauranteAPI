namespace RestauranteAPI.Models
{
    public class Mesa
    {
        public string Id { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public double Total { get; set; }
    }
}
