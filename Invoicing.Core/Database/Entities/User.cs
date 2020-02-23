using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Core.Database.Entities
{
    public class User
    {
        public User()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Contractors = new HashSet<Contractor>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(4)]
        public string HouseNumber { get; set; }

        public int? LocalNumber { get; set; }

        [MaxLength(6)]
        public string ZipCode { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string Nip { get; set; }

        [MaxLength(34)]
        public string BankAccount { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
    }
}
