namespace MyKitchen.Models.Roles
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Role;
    public class RoleFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
    }
}
