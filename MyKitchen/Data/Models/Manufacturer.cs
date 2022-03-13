namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Manufacturer;
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Kitchens = new HashSet<Kitchen>();
        }
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Kitchen> Kitchens { get; init; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}


