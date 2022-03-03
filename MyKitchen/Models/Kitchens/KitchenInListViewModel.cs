﻿namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models.Enum;

    public class KitchenInListViewModel 
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }
        public string UserName { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }
        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public string Description { get; set; }
        public string МanufacturerName { get; set; }


    }
}
