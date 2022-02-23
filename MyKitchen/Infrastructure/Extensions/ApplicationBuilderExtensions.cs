namespace MyKitchen.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            SeedCategories(services);

            SeedColors(services);

            SeedCountries(services);

            SeedCities(services);

            SeedColors(services);

            MigrateDatabase(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyKitchenDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyKitchenDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl, string Description)>
            {
                ("L-Shaped kitchen", "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg", "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining"),
                ("U-Shaped kitchen", "https://www.cliqstudios.com/media/cms-pages/u-shape-cliqstudios-kitchen-shape.jpg", "The U-Shape design is the most versatile layout for both large and small kitchens. It surrounds the cook on all sides and allows for ample countertop space and storage. The U-Shape kitchen creates an efficient work triangle, and creates a large amount of storage space. This arrangement is suited toward separating the cooking space from the dining space. The U-Shape layout is ideal for creating large amounts of storage space."),
                ("Island kitchen", "https://www.cliqstudios.com/media/cms-pages/islandsl-cliqstudios-kitchen-shape.jpg", "The kitchen island is a place to be creative. The size will be determined by the amount of space you have, and the countertop material you are using. At least 36”- 42” of space should surround the island on all sides to allow appliances such as the dishwasher and stove to be opened and closed. If you’re looking at a seamless solid surface countertop, especially granite, the size of the stone can be limiting. Granite and engineered stone slabs generally don’t exceed 120” by 72” and some are much smaller. Will your island host an appliance, sink, trash, recycling, eating area and food prep area? Since the island has the potential to be a mini-kitchen, it requires a carefully thought-out design."),
                ("One-wall kitchen", "https://www.cliqstudios.com/media/cms-pages/one-wall-cliqstudios-kitchen-shape.jpg", "The one wall kitchen is the answer for very small homes and condos. The work triangle flattens out by placing the sink between the range and the refrigerator for maximum efficiency. When using the single wall layout, the refrigerator should be positioned so the door opens away from the kitchen sink to remove the possibility of a disturbance in workflow. Take into consideration whether you are right or left handed when placing the dishwasher and frequently accessed kitchen cabinet."),
                ("G-Shape Kitchen", "https://www.cliqstudios.com/media/cms-pages/g-shape-cliqstudios-kitchen-shape.jpg", "The G-shape layout is a variation of the U-Shape, with the addition of a peninsula or a partial fourth wall, which can be used for extra countertop and storage space. By adding a second sink, cooktop or range, the G-Shape kitchen can easily accommodate two work triangles, allowing two cooks total independence."),
                ("Galley Kitchen", "https://www.cliqstudios.com/media/cms-pages/galley-cliqstudios-kitchen-shape.jpg", "The galley kitchen is the most efficient layout for a narrow space. It consists of work spaces on two opposing walls with a single traffic lane between. Placing the range or cooktop on one side of the kitchen and the refrigerator and sink on the opposite wall allows for easy workflow. This design can be used so the kitchen opens to the rest of the house on one or both ends. The ideal width for a galley kitchen is 7 to 12 feet and works particularly well in a rectangular space. It can be transformed by replacing a wall with an island or peninsula open to an adjacent room."),
            };
            foreach (var category in categories)
            {
                data.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                    ImageUrl = category.ImageUrl,
                });
            }

            data.SaveChanges();
        }
        private static void SeedColors(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyKitchenDbContext>();

            if (data.Colors.Any())
            {
                return;
            }

            var colors = new List<string>
            {
                "White",
                "Off white",
                "Black",
                "Yellow",
                "Orange",
                "Red",
                "Pink",
                "Violet",
                "Blue",
                "Green",
                "Brown",
                "Gray",
                "Purple",
                "Olive",
                "Maroon",
                "Charcoal",
                "Magenta",
                "Bronze",
                "Cream",
                "Tan",
                "Teal",
                "Mustard",
                "Navy blue",
                "Coral",
                "Burgundy",
                "Lavender",
                "Mauve",
                "Peach",
                "Rust",
                "Gold",
                "Silver",
                "Cyan",
                "Indigo",
                "Ruby",
                "Clay",
                "Turquoise",
                "Amber",
                "Mint",
            };
            foreach (var color in colors)
            {
                data.Colors.AddAsync(new Color
                {
                    Name = color,
                });
            }

            data.SaveChanges();
        }
        private static void SeedCountries(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyKitchenDbContext>();

            if (data.Countries.Any())
            {
                return;
            }

            var countries = new List<string>
            {
                "Bulgaria",
                "Turkey",
                "Russia",
                "Germany",
                "United Kingdom",
                "France",
                "Italy",
                "Spain",
                "Ukraine",
                "Poland",
                "Romania",
                "Netherlands",
                "Belgium",
                "Czech Republic (Czechia)",
                "Greece",
                "Portugal",
                "Sweden",
                "Hungary",
                "Belarus",
                "Austria",
                "Serbia",
                "Switzerland",
                "Denmark",
                "Finland",
                "Slovakia",
                "Norway",
                "Croatia",
                "Moldova",
                "Albania",
                "Lithuania",
                "North Macedonia",
                "Slovenia",
                "Latvia",
                "Estonia",
                "Montenegro",
                "Luxembourg",
                "Malta",
                "Iceland",
                "Andorra",
                "Monaco",
                "Liechtenstein",
                "San Marino",
                "USA",
                "Canada",
                "Mexico",
                "China",
                "India",
            };
            foreach (var country in countries)
            {
                data.Countries.AddAsync(new Country
                {
                    Name = country,
                });
            }

            data.SaveChanges();
        }
        private static void SeedCities(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyKitchenDbContext>();

            if (data.Cities.Any())
            {
                return;
            }

            var cities = new List<(string Name, int CountryId)>
            {
                ("Sofia",1),
                ("Plovdiv",1),
                ("Varna",1),
                ("Burgas",1),
                ("Ruse",1),
                ("Stara Zagora",1),
                ("Pleven",1),
                ("Sliven",1),
                ("Dobrich",1),
                ("Shumen",1),
                ("Pernik",1),
                ("Haskovo",1),
                ("Yambol",1),
                ("Pazardzhik",1),
                ("Blagoevgrad",1),
                ("Veliko Tarnovo",1),
                ("Vratsa",1),
                ("Gabrovo",1),
                ("Asenovgrad",1),
                ("Vidin",1),
                ("Kazanlak",1),
                ("Kyustendil",1),
                ("Kardzhali",1),
                ("Montana",1),
                ("Dimitrovgrad",1),
                ("Targovishte",1),
                ("Lovech",1),
                ("Silistra",1),
                ("Dupnitsa",1),
                ("Svishtov",1),
                ("Razgrad",1),
                ("Gorna Oryahovitsa",1),
                ("Smolyan",1),
                ("Petrich",1),
                ("Sandanski",1),
                ("Sevlievo",1),
                ("Lom",1),
                ("Karlovo",1),
                ("Velingrad",1),
            };
            foreach (var city in cities)
            {
                data.Cities.AddAsync(new City
                {
                    Name = city.Name,
                    CountryId = city.CountryId,
                });
            }

            data.SaveChanges();
        }
    }
}

