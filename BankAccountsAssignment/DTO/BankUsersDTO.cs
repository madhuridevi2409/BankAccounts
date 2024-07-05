using System.ComponentModel.DataAnnotations;

namespace BankAccountsAssignment.DTO
{
    public class BankUsersDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(300)]
        public string Address_Address1 { get; set; }
        [MaxLength(300)]
        public string Address_Address2 { get; set; }

        [MaxLength(50)]
        public string Address_City { get; set; }
        [MaxLength(50)]
        public string Address_State { get; set; }
        [MaxLength(50)]
        public string Address_Country { get; set; }
        [MaxLength(5)]
        public string Address_ZipCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<BankAccountsDTO> BankAccountsDTO { get; set; }
    }
}
