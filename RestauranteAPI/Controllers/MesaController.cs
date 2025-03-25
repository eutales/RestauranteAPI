using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;
using RestauranteAPI.Services;
using RestauranteAPI.DTOs;
using System.Threading.Tasks;
namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;
        public MesaController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        // Criar pedido
        [HttpPost("criar-pedido")]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoDTO pedidoDto)
        {
            if (pedidoDto == null)
                return BadRequest("Pedido inválido.");
            var pedido = new Pedido
            {
                Id = Guid.NewGuid().ToString(),
                MesaId = pedidoDto.MesaId,
                Itens = pedidoDto.Itens.Select(i => new Item
                {
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Quantidade = i.Quantidade
                }).ToList(),
                Total = pedidoDto.Itens.Sum(i => i.Preco * i.Quantidade)
            };
            await _firebaseService.AddPedidoAsync(pedido);
            return Ok(new { Message = "Pedido criado com sucesso." });
        }
        // Fechar mesa e calcular total
        [HttpPost("fechar-mesa/{mesaId}")]
        public async Task<IActionResult> FecharMesa(string mesaId)
        {
            var mesa = await _firebaseService.GetMesaAsync(mesaId);
            if (mesa == null)
                return NotFound("Mesa não encontrada.");
            await _firebaseService.FecharMesaAsync(mesaId);
            return Ok(new { Message = "Mesa fechada com sucesso.", Total = mesa.Total });
        }
    }
}