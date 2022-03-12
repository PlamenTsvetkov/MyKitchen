namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models;
    using MyKitchen.Data.Models.Enum;
    using MyKitchen.Models.Colors;
    using MyKitchen.Models.Comments;

    public class SingleKitchenViewModel : IKitchenModel
    {
        public int Id { get; init; }

        public DateTime CreatedOn { get; set; }

        public string МanufacturerName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }

        public string Description { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }
        public double KitchenMeter { get; set; }

        public  IEnumerable<ColorsViewModel> KitchensColorsColor { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }

        public double AverageVote { get; set; }

        public IEnumerable<PostCommentViewModel> Comments { get; set; }
    }
}

