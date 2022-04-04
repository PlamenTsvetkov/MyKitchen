namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.City;
    public class City
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public int CountryId { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}

