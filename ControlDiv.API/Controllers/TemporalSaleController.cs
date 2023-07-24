using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.Entities;
using ControlDiv.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITemporalSaleRepository _temporalSaleRepository;

        public TemporalSaleController(Repository.IGenericRepository<TemporalSale> genericRepository, DataContext context, ITemporalSaleRepository temporalSaleRepository)
        {
            _genericRepository = genericRepository;
            _context = context;
            _temporalSaleRepository = temporalSaleRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            if (user!.UserType == UserType.Admin)
                return Ok(await _context.Temporals.Include(x => x.Account).Include(x => x.User).ToListAsync());
            else
                return Ok(await _context.Temporals.Include(x => x.Account).Include(x => x.User).Where(x => x.User == user).ToListAsync());
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AuthorizeSale(TemporalSale temporalSale)
        {
            var result = await _temporalSaleRepository.AuthorizeTemporalSale(temporalSale);
            if (result == string.Empty)
                return Ok();
            else
                return BadRequest(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _context.Temporals.Where(x => x.Id == Id).ExecuteDeleteAsync();
            return Ok();
        }

    }
}
