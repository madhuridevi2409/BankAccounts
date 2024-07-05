using BankAccountsAssignment.DbContextFolder;
using BankAccountsAssignment.DomainModels;
using BankAccountsAssignment.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankAccountsAssignment.Service
{
    public class BankService : IBankService
    {
        private readonly DbContextClass _dbContext;
        public BankService(DbContextClass dbContextObj)
        {
            _dbContext = dbContextObj;
        }

        public async Task<List<BankUsersDTO>> GetAllUsers()
        {
            var bankUsers = await _dbContext.BankUsers.ToListAsync();
            
            var bankAccounts = await _dbContext.BankAccounts.ToListAsync();

            var bankUsersDTO = new List<BankUsersDTO>();
            foreach (var bankUser in bankUsers) 
            {
                var userBankAccounts = bankAccounts.FindAll(x => x.BankUserId == bankUser.Id);

                var bankAccountsDTO = new List<BankAccountsDTO>();
                foreach (var bankAccount in userBankAccounts)
                {
                    bankAccountsDTO.Add(new BankAccountsDTO()
                    { 
                    Id = bankAccount.Id,
                    BankUserId = bankAccount.BankUserId,
                    AccountType = bankAccount.AccountType,
                    Amount = bankAccount.Amount,
                    Enabled = bankAccount.Enabled,
                    CreatedAt = bankAccount.CreatedAt,
                    UpdatedAt = bankAccount.UpdatedAt
                    });
                }

                bankUsersDTO.Add(new BankUsersDTO()
                {
                    Id = bankUser.Id,
                    FirstName = bankUser.FirstName,
                    LastName = bankUser.LastName,
                    Address_Address1  = bankUser.Address_Address1,
                    Address_Address2 = bankUser.Address_Address2,
                    Address_City = bankUser.Address_City,
                    Address_Country = bankUser.Address_Country,
                    Address_State = bankUser.Address_State,
                    Address_ZipCode = bankUser.Address_ZipCode,
                    CreatedAt   = bankUser.CreatedAt,
                    BankAccountsDTO = bankAccountsDTO
                });

            }
            return bankUsersDTO;
        }

        public async Task<BankUsersDTO> GetUsers( int id)
        {
            var bankUser = await _dbContext.BankUsers.FindAsync(id);
            var bankAccounts = await _dbContext.BankAccounts.Where(x => x.BankUserId == id).ToListAsync();

            var bankUsersDTO = new BankUsersDTO();
            
            var bankAccountsDTO = new List<BankAccountsDTO>();
            foreach (var bankAccount in bankAccounts)
            {
                    bankAccountsDTO.Add(new BankAccountsDTO()
                    {
                        Id = bankAccount.Id,
                        BankUserId = bankAccount.BankUserId,
                        AccountType = bankAccount.AccountType,
                        Amount = bankAccount.Amount,
                        Enabled = bankAccount.Enabled,
                        CreatedAt = bankAccount.CreatedAt,
                        UpdatedAt = bankAccount.UpdatedAt
                    });
            }

            bankUsersDTO.Id = bankUser.Id;
            bankUsersDTO.FirstName = bankUser.FirstName;
            bankUsersDTO.LastName = bankUser.LastName;
            bankUsersDTO.Address_Address1 = bankUser.Address_Address1;
            bankUsersDTO.Address_Address2 = bankUser.Address_Address2;
            bankUsersDTO.Address_City = bankUser.Address_City;
            bankUsersDTO.Address_Country = bankUser.Address_Country;
            bankUsersDTO.Address_State = bankUser.Address_State;
            bankUsersDTO.Address_ZipCode = bankUser.Address_ZipCode;
            bankUsersDTO.CreatedAt = bankUser.CreatedAt;
            bankUsersDTO.BankAccountsDTO = bankAccountsDTO;

            return bankUsersDTO;
        }

        public async Task<BankUsersDTO> AmountTransact(int bankUserId, int sourceAccountId, int destinyAccountId, decimal amount)
        {
            var BankUser = await _dbContext.BankUsers.FirstOrDefaultAsync(x => x.Id == bankUserId);

            var BankAccounts = await _dbContext.BankAccounts.Where(x => x.BankUserId == bankUserId).ToListAsync();

            if(BankUser == null)
            {
                throw new Exception("User Id doesn't Exist!");
            }
            else
            {
                if(BankAccounts == null)
                {

                    throw new Exception("User don't have Bank Accounts!");
                }
                else
                {
                    var srcBankAccount = BankAccounts.FirstOrDefault(x => x.Id == sourceAccountId && x.Enabled == true);
                    var destBankAccount = BankAccounts.FirstOrDefault(x => x.Id == destinyAccountId && x.Enabled == true);

                    if(srcBankAccount == null) 
                    {
                        throw new BadHttpRequestException("Source account is invalid/inactive");
                    }
                    else if (destBankAccount == null)
                    {
                        throw new BadHttpRequestException("Destination account is invalid/inactive");
                    }
                    else
                    {
                        if(srcBankAccount.Amount < amount)
                        {
                            throw new BadHttpRequestException("Insufficient Balance");
                        }
                        else
                        {
                            srcBankAccount.Amount -= amount;
                            destBankAccount.Amount += amount;
                            await _dbContext.SaveChangesAsync();


                        }
                    }

                    var BankUserDTO = await GetUsers(bankUserId);
                    return BankUserDTO;
                }
            }
        }
    }
}
