using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Store.Data;
using Api.Store.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IPriceStrategyFactory _factory;

        public StoreController(StoreContext context, IPriceStrategyFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        [HttpGet]
        public IAsyncEnumerable<Item> Get()
        {            
            return _context.Items.Include(item => item.Stocks).AsAsyncEnumerable();
        }

        [HttpGet("Balance")]
        public Task<StockBalance> GetBalance(DateTime? date = null)
        {
            var result = new StockBalance();
            
            double losses = 0;
            double benefits = 0;            

            foreach (var item in _context.Items.Include(item=>item.Stocks))
            {
                foreach (var stock in item.Stocks)
                {

                    var strategy = _factory.Create(item);
                    var newPrice = strategy.CalculatePrice(stock, date);
                    var difPrice = newPrice - stock.Price;
                    if (difPrice < 0) losses += difPrice;
                    if (difPrice > 0) benefits += difPrice;
                }
            }

            result.Losses = Math.Abs(Math.Round(losses,2));
            result.Benefits = Math.Round(benefits,2);
            
            return Task.FromResult(result);
        }

        [HttpPatch("SetStocksPrice")]
        public async Task<IActionResult> SetStocksPrice(DateTime? date = null, int? category = null)
        {
            int updateItems = 0;

            foreach (var item in _context.Items.Include(item => item.Stocks).Where(p=>category == null || (int)p.Category == category))
            {
                foreach (var stock in item.Stocks)
                {

                    var strategy = _factory.Create(item);
                    var newPrice = strategy.CalculatePrice(stock, date);

                    if (newPrice != stock.Price)
                    {
                        stock.Price = newPrice;
                        updateItems++;
                    }
                }
            }

            if (updateItems > 0) 
                await _context.SaveChangesAsync();

            return new OkObjectResult($"{updateItems} items have been updated.");
            
        }
    }
}
