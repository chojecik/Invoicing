using System.ComponentModel.DataAnnotations;

namespace Invoicing.Core.Database.Entities
{
    public class Contractor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(4)]
        public string HouseNumber { get; set; }

        [Required]
        public int LocalNumber { get; set; }

        [Required]
        [MaxLength(6)]
        [RegularExpression("[0 - 9]{2}\\-[0-9]{3}", ErrorMessage = "Niepoprawny format kodu pocztowego")]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string Nip { get; set; }
    }
}
