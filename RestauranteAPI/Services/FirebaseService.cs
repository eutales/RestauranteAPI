using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using RestauranteAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RestauranteAPI.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;
        public FirebaseService(IConfiguration configuration)
        {
            var firebaseUrl = configuration["Firebase:DatabaseUrl"];
            _firebaseClient = new FirebaseClient(firebaseUrl);
        }
        // Obter mesa pelo ID
        public async Task<Mesa> GetMesaAsync(string mesaId)
        {
            var mesa = (await _firebaseClient
            .Child("mesas")
            .Child(mesaId)
            .OnceAsync<Mesa>())
            .FirstOrDefault()?.Object;
            return mesa;
        }
        // Adicionar pedido à mesa
        public async Task AddPedidoAsync(Pedido pedido)
        {
            var mesa = await GetMesaAsync(pedido.MesaId);
            if (mesa == null)
            {
                mesa = new Mesa { Id = pedido.MesaId };
            }
            mesa.Pedidos.Add(pedido);
            await _firebaseClient.Child("mesas").Child(pedido.MesaId).PutAsync(mesa);
        }
        // Fechar mesa e calcular o total
        public async Task FecharMesaAsync(string mesaId)
        {
            var mesa = await GetMesaAsync(mesaId);

            if (mesa != null)
            {
                double totalMesa = mesa.Pedidos.Sum(p => p.Total);
                mesa.Total = totalMesa;
                await _firebaseClient.Child("mesas").Child(mesaId).Child("Total").PutAsync(mesa.Total);
                foreach (var pedido in mesa.Pedidos)
                {
                    pedido.Fechado = true;
                    await _firebaseClient.Child("mesas").Child(mesaId).Child("Pedidos").Child(pedido.Id).PutAsync(pedido);
                }
            }
        }
    }
}