namespace MyKitchen.Test.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using MyKitchen.Data;

    public static class DatabaseMock
    {
        public static MyKitchenDbContext  Instance
        { 
            get
            {
                var dbContextOption = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new MyKitchenDbContext(dbContextOption);

            } 
        }
    }
}
