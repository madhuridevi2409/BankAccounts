using BankAccountsAssignment.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankAccountsAssignment.DTO
{
    public class BankAccountsDTO
    {
       
        public int Id { get; set; }
        public int BankUserId { get; set; }
        [Required]
        public AccountTypeEnums AccountType { get; set; }
        [Required]
        public Decimal Amount { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
