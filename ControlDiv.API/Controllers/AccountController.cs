using ControlDiv.API.Data;
using ControlDiv.API.Repository;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlDiv.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly DataContext _context;


        public AccountController(IGenericRepository<Account> repository, DataContext dataContext)
        {
            _repository = repository;
            _context = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAll();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Account account)
        {
            var result = await _repository.Create(account);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetId(int Id)
        {
            return Ok(_context.Accounts.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPut]
        public async Task<IActionResult> Update(AccountDTO accountDTO)
        {
            var account =_context.Accounts.FirstOrDefault(x => x.Id == accountDTO.Id);
             
            account!.Mont = accountDTO.Mont;
            account.AccountType = accountDTO.AccountType;
            await _repository.Update(account);
            return Ok();
        }
    }
}
