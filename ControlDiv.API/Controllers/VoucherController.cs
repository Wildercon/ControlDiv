using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IGenericRepository<Voucher> _repository;

        public VoucherController(IVoucherRepository voucherRepository,IGenericRepository<Voucher> repository)
        {
            _voucherRepository = voucherRepository;
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _voucherRepository.GetVouchers();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Voucher voucher)
        {
            var result = await _voucherRepository.AddVoucherAndUpdateAccount(voucher);
            if (result == string.Empty)           
                return Ok();
            else
                return BadRequest(result);
        }
    }
}
