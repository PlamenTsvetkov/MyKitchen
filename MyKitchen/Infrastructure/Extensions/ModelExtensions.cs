namespace MyKitchen.Infrastructure.Extensions
{
    using MyKitchen.Models.Kitchens;
    public static class ModelExtensions
    {
        public static string GetInformation(this IKitchenModel kitchen)
           =>  kitchen.PreparationTime + "-" + kitchen.KitchenMeter;
    }
}
