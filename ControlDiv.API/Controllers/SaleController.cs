using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SaleController : ControllerBase
    {
        private readonly IGenericRepository<Sale> _genericRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly DataContext _context;
        

        public SaleController(IGenericRepository<Sale> genericRepository , ISaleRepository saleRepository, DataContext dataContext)
        {
            _genericRepository = genericRepository;
            _saleRepository = saleRepository;
            _context = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _genericRepository.GetAll();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add(SaleDTO saleDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            if (user != null)
            {
                var result = await _saleRepository.Add(saleDTO, user);
                if (result != string.Empty) 
                    return Ok();
                else 
                    return BadRequest(result);
            }
            else 
                return BadRequest("Debe estar logueado");
               
        }
    }
}
