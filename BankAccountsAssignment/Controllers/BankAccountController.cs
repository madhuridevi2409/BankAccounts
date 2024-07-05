using BankAccountsAssignment.DbContextFolder;
using BankAccountsAssignment.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountsAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly DbContextClass _dbContext;
        private IBankService _bankService;
        public BankAccountController( DbContextClass dbContextObj, IBankService bankService)
        {
            _dbContext = dbContextObj;
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _bankService.GetAllUsers();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> AmountTransaction(int bankUserId, int sourceAccountId, int destinyAccountId, decimal amount)
        {
            var result = await _bankService.AmountTransact(bankUserId, sourceAccountId, destinyAccountId, amount);

            return Ok(result);
        }
    }
}
