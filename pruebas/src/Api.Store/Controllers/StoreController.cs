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

                    if (item.Category == Category.Perishable)
                    {

                        var expirationDays = stock.ExpirationDate.GetValueOrDefault(DateTime.MaxValue).Subtract(date.GetValueOrDefault(DateTime.Now)).TotalDays;

                        if (expirationDays < 1)
                        {
                            losses += stock.CostPrice;                            
                        }
                        else if (expirationDays < 3)
                        {
                            losses += stock.CostPrice / 2;
                        }
                        else if (expirationDays < 5)
                        {
                            losses += stock.CostPrice / 4;
                        }
                    }

                    if (item.Category == Category.Aged && stock.ManufacturingDate.HasValue)
                    {
                        var totalDaysFromManufacturing =  date.GetValueOrDefault(DateTime.Now).Subtract(stock.ManufacturingDate.Value).TotalDays;
                        var age = Math.Truncate(totalDaysFromManufacturing / 365);

                        var totalDaysFromEntry = date.GetValueOrDefault(DateTime.Now).Subtract(stock.Entrydate.Value).TotalDays;
                        var storeYears = Math.Truncate(totalDaysFromEntry / 365);

                        var computeYears = age - storeYears;

                        if (computeYears > 1)
                        {
                            if (computeYears < 5)
                            {
                                benefits += stock.CostPrice * 1.05;
                            }
                            else if (computeYears < 10)
                            {
                                benefits += stock.CostPrice * 1.10;
                            }
                            else
                            {
                                var coef = 1 + (computeYears / 100);
                                benefits += stock.CostPrice * coef;
                            }
                        }
                    }
                }
            }

            result.Losses = Math.Round(losses,2);
            result.Benefits = Math.Round(benefits,2);
            
            return Task.FromResult(result);
        }
    }
}
