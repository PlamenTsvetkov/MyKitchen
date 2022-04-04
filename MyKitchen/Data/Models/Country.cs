namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Country;
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
