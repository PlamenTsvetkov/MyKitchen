namespace MyKitchen.Test.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using System;

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
