using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/{symbol}/24hAvgPrice")]
    public class SymbolsController(ISymbolPriceService symbolPriceService) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> Get(string symbol)
        {
            var averageSymbolPrice = await symbolPriceService.GetAveragePrice(symbol);

            if (averageSymbolPrice is null)
            {
                return BadRequest();
            }
          
            return Ok( averageSymbolPrice);
        }


        [HttpPost(nameof(SimpleMovingAverage))]
        public async Task<IActionResult> SimpleMovingAverage(SimpleMovingAverageRequest request)
        {
            var averageSymbolPrice = await symbolPriceService.SimpleMovingAverage(request);

            if (!averageSymbolPrice.Any())
            {
                return BadRequest();
            }

            return Ok(averageSymbolPrice);
        }
    }
}
