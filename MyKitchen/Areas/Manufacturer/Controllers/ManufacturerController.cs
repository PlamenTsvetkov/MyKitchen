namespace MyKitchen.Areas.Manufacturer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ManufacturerConstants;

    [Area(AreaName)]
    [Authorize(Roles = ManufacturerRoleName)]
    public abstract class ManufacturerController : Controller
    {
    }
}

