using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using ControlDiv.Shared.Enum;
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


        public SaleController(IGenericRepository<Sale> genericRepository, ISaleRepository saleRepository, DataContext dataContext)
        {
            _genericRepository = genericRepository;
            _saleRepository = saleRepository;
            _context = dataContext;
        }

        [HttpGet("{pag}")]
        public async Task<IActionResult> Get(int pag)
        {
            pag = pag * 5;
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            if (user!.UserType == UserType.Admin)
                return Ok(await _genericRepository.GetAll());
            else
            {
                var list = await _context.Sales.Where(x => x.User == user).OrderByDescending(x => x.Id).Skip(pag).Take(5).ToListAsync();
                return Ok(list);
            }
                
        }
        [HttpPost]
        public async Task<IActionResult> Add(SaleDTO saleDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            if (user == null) return BadRequest("Debe estar logueado");
            string result;
            if (saleDTO.IsSale)
            {
                result = await _saleRepository.Add(saleDTO, user);
                    
            }else
            {
                result = await _saleRepository.ReverseSale(saleDTO);
            }
           
            if (result == string.Empty)
                return Ok();
            else
                return BadRequest(result);

        }
        [HttpPost("Zelle")]
        public async Task<IActionResult> AddZelle(SaleDTO saleDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            if (user == null) return BadRequest("Debe estar logueado");
            var result = await _saleRepository.AddZelle(saleDTO,user);
            if(result == string.Empty) return Ok();
            return BadRequest(result);
        }
    }
}
