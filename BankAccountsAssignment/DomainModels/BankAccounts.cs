using BankAccountsAssignment.Enumerables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountsAssignment.DomainModels
{
    public class BankAccounts
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BankUsers")]
        public int BankUserId { get; set; }
        [Required]
        public AccountTypeEnums AccountType { get; set; }
        [Required]
        public Decimal Amount { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set;}

        public BankUsers BankUsers { get; set; }
    }
}
