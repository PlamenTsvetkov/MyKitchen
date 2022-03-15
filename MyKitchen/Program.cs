using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Data.Models;
using MyKitchen.Infrastructure.Extensions;
using MyKitchen.Services.Addresses;
using MyKitchen.Services.Categories;
using MyKitchen.Services.Cities;
using MyKitchen.Services.Colors;
using MyKitchen.Services.Comments;
using MyKitchen.Services.Countries;
using MyKitchen.Services.Kitchens;
using MyKitchen.Services.Manufacturers;
using MyKitchen.Services.Vote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyKitchenDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyKitchenDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(option=>
{
    option.HeaderName = "X-CSRF-TOKEN";
});


builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IKitchenService, KitchenService>();
builder.Services.AddTransient<IManufacturersService, ManufacturersService>();
builder.Services.AddTransient<IColorsService, ColorsService>();
builder.Services.AddTransient<ICountriesService, CountriesService>();
builder.Services.AddTransient<ICitiesService, CitiesService>();
builder.Services.AddTransient<IVotesService, VotesService>();
builder.Services.AddTransient<IAddressesService, AddressesService>();
builder.Services.AddTransient<ICommentsService, CommentsService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMemoryCache();


var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();


app.MapControllerRoute(
              name: "Areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
                        name: "Kitchen Details",
                        pattern: "/Kitchens/Details/{id}/{information}",
                        defaults: new
                        {
                            controller = typeof(KitchensController).GetControllerName(),
                            action = nameof(KitchensController.Details)
                        });

app.MapControllerRoute(
    "manufacturerKitchens",
     "{controller}/{action}/{manufacturerId}/{pageId}",
      new { controller = "Manufacturer", action = "AllKitchen", manufacturerId = "", pageId = "" });
app.MapControllerRoute(
    "kitchenCategory",
    "k/{name:minlength(3)}",
    new { controller = "Categories", action = "ByName" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseAuthentication();
app.Run();
