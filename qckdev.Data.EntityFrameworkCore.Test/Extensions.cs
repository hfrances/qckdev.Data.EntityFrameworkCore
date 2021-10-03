using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Data.EntityFrameworkCore.Test
{
    static class Extensions
    {

        public static TDbContext CreateDbContext<TDbContext>(Func<DbContextOptionsBuilder<TDbContext>, DbContextOptionsBuilder<TDbContext>> builder) where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            var options = builder(optionsBuilder).Options;

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        }

    }
}
