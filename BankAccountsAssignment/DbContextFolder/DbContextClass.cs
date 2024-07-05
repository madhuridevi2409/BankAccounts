using BankAccountsAssignment.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BankAccountsAssignment.DbContextFolder
{
    public class DbContextClass : DbContext
    {
        public DbContextClass( DbContextOptions<DbContextClass> dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<BankUsers> BankUsers { get; set; }

        public DbSet<BankAccounts> BankAccounts { get; set; }   
    }
}
