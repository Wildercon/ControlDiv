using ControlDiv.API.Repository;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IGenericRepository<Seller> _repository;

        public SellerController(IGenericRepository<Seller> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Seller seller)
        {
           return Ok( await _repository.Create(seller)); 
        }
    }
}
