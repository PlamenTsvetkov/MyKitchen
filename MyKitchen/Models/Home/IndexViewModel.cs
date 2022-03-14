namespace MyKitchen.Models.Home
{
    using MyKitchen.Models.Kitchens;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
        public List<HomeKitchensViewModel> Kitchens { get; set; }
    }
}


