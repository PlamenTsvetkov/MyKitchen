namespace MyKitchen.Test.Data
{
    using MyKitchen.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Kitchens
    {
        public static IEnumerable<Kitchen> TenPublicKitchens
            => Enumerable.Range(0, 10).Select(i => new Kitchen
            {
                IsPublic = true
            });
    }
}
