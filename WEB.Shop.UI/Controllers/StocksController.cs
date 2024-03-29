﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB.Shop.Application.StockAdmin;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        [HttpGet("")]
        public IActionResult GetStocks([FromServices] GetStocks getStock) => 
            Ok(getStock.Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStockAsync([FromBody] CreateStock.Request request, [FromServices] CreateStock createStock) => 
            Ok(await createStock.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockAsync(int id, [FromServices] DeleteStock deleteStock) => 
            Ok(await deleteStock.Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStockAsync([FromBody] UpdateStock.Request request, [FromServices] UpdateStock updateStock) => 
            Ok(await updateStock.Do(request));

    }
}