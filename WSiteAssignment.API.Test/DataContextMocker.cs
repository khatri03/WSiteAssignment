using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WSiteAssignment.Data;

namespace WSiteAssignment.API.Test
{
    internal static class DataContextMocker
    {
        public static DataContext GetDataContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var dbContext = new DataContext(options);
            dbContext.Seed();

            return dbContext;
        }
    }
}
