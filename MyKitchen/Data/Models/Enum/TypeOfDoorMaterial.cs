namespace MyKitchen.Data.Models.Enum
{
    using System.ComponentModel.DataAnnotations;

    public enum TypeOfDoorMaterial
    {
        [Display(Name = "Melamine faced")]
        MelamineFaced = 1,

        Hardwood = 2,

        [Display(Name = "Vinyl wrapped")]
        VinylWrapped = 3,

        [Display(Name = "High gloss")]
        HighGloss = 4
    }
}