using ControlDiv.API.Data;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceDollarController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PriceDollarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var dollarPrice = await _dataContext.Prices.FirstOrDefaultAsync();
            return Ok(dollarPrice);
        }
        [HttpPut]
        public async Task<IActionResult> Update(PriceDollar priceDollar)
        {
            priceDollar.Id = 2;
            _dataContext.Prices.Update(priceDollar);
            var result = await _dataContext.SaveChangesAsync();
            if(result > 0)
                return Ok();
            else 
                return BadRequest("Error Actualizando");
        }
    }
}
