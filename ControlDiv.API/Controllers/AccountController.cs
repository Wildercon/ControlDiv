using ControlDiv.API.Data;
using ControlDiv.API.Repository;
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
        private readonly IAccountRepository _accountRepository;
        

        public AccountController(IGenericRepository<Account> repository , IAccountRepository accountRepository) 
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list =  await _repository.GetAll();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Account account)
        {
            var result = await _repository.Create(account);
            return Ok(result);
        }
    }
}
