using BankAccountsAssignment.DTO;

namespace BankAccountsAssignment.Service
{
    public interface IBankService
    {
        public  Task<List<BankUsersDTO>> GetAllUsers();

        public Task<BankUsersDTO> AmountTransact(int bankUserId, int sourceAccountId, int destinyAccountId, decimal amount);
    }
}
