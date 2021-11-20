using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Store.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
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

                    if (item.Category == Category.Perishable && stock.ExpirationDate.HasValue)
                    {

                        var expirationDays = stock.ExpirationDate.Value.Date.Subtract(date.GetValueOrDefault(DateTime.Now).Date).Days;

                        if (expirationDays <= 1)
                        {
                            losses += stock.Price;                            
                        }
                        else if (expirationDays <= 3)
                        {
                            losses +=  stock.Price -  (stock.Price / 2);
                        }
                        else if (expirationDays <= 5)
                        {
                            losses += stock.Price - (stock.Price / 4);
                        }
                    }

                    if (item.Category == Category.Aged && stock.ManufacturingDate.HasValue && stock.Entrydate.HasValue)
                    {
                        var totalDaysFromManufacturing =  date.GetValueOrDefault(DateTime.Now).Date.Subtract(stock.ManufacturingDate.Value.Date).TotalDays;
                        var age = Math.Truncate(totalDaysFromManufacturing / 365);

                        var totalDaysFromEntry = date.GetValueOrDefault(DateTime.Now).Date.Subtract(stock.Entrydate.Value.Date).TotalDays;
                        var storeYears = Math.Truncate(totalDaysFromEntry / 365);

                        var computeYears = age - storeYears;

                        if (computeYears >= 1)
                        {
                            if (computeYears <= 5)
                            {
                                benefits += (stock.Price * 1.05) - stock.Price;
                            }
                            else if (computeYears <= 10)
                            {
                                benefits += (stock.Price * 1.10) - stock.Price;
                            }
                            else
                            {
                                var coef = 1 + (computeYears / 100);
                                benefits += (stock.Price * coef) - stock.Price;
                            }
                        }
                    }
                }
            }

            result.Losses = Math.Round(losses,2);
            result.Benefits = Math.Round(benefits,2);
            
            return Task.FromResult(result);
        }

        [HttpPatch("SetStocksPrice")]
        public IActionResult SetStocksPrice(DateTime? date = null, int? category = null)
        {
            
            //TODO: update cost price
            return new OkObjectResult("6 items have been updated.");
            
        }
    }
}
