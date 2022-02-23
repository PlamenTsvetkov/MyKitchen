namespace MyKitchen.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.Manufacturers = new HashSet<Manufacturer>();
        }

        public int Id { get; init; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string AddedByUserId { get; set; }
        public virtual ApplicationUser AddedByUser { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}


