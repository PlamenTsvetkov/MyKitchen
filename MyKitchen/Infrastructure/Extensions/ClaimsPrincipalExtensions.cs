namespace MyKitchen.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;
    using static Areas.Manufacturer.ManufacturerConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
           => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
        public static bool IsAManufacturer(this ClaimsPrincipal user)
           => user.IsInRole(ManufacturerRoleName);
    }
}
