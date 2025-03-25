namespace RestauranteAPI.Models
{
    public class Item
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public double Total => Preco * Quantidade;
    }
}
