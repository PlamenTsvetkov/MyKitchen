﻿namespace MyKitchen.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Kitchens = new HashSet<Kitchen>();
            this.Manufacturers = new HashSet<Manufacturer>();
            this.Images = new HashSet<Image>();
            this.Votes = new HashSet<Vote>();
        }
        public string Name { get; set; }

        public ICollection<Image> Images { get; init; }

        public ICollection<Kitchen> Kitchens { get; init; }

        public ICollection<Manufacturer> Manufacturers { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
