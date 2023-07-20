using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporalSaleController : ControllerBase
    {
        private readonly IGenericRepository<TemporalSale> _genericRepository;
        private readonly DataContext _context;

        public TemporalSaleController(Repository.IGenericRepository<TemporalSale> genericRepository,DataContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Temporals.Include(x => x.Account).Include(x => x.User).ToListAsync());
        }    
    }
}
