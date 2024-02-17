using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.EC;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _generic;
        private readonly DataContext _context;


        public CustomerController(IGenericRepository<Customer> generic, DataContext context)
        {
            _generic = generic;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            customer.User = user;
            await _generic.Create(customer);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerSeller()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
            var customers = await _context.Customers.Where(x => x.User == user).ToListAsync();
            return Ok(customers);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetCustomerDetail(int Id)
        {
            var customer =  _context.Customers.FirstOrDefault(x => x.Id == Id);
            var detailsCustomer = await _context.CustomersDetail.Where(x => x.Customer == customer).ToListAsync();
            return Ok(detailsCustomer);
        }

        [HttpPut]
        public async Task<IActionResult> Deliver(Customer customer)
        {
            var user = _context.Users.Where(x => x.Email == User.Identity!.Name).FirstOrDefault();
            var custo = _context.Customers.Where(x => x.Id.Equals(customer.Id)).FirstOrDefault();
            if(custo != null) 
                custo.Mont -= customer.Mont; 
            else
                return BadRequest("Debe Sellecionar Un Cliente");
            _context.Customers.Update(customer);
            user!.MontDeliver -= customer.Mont;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
