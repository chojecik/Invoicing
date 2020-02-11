﻿using System.Collections.Generic;
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

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
    }
}
