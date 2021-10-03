using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace qckdev.Data.EntityFrameworkCore.Test
{
    sealed class TestDbContext : DbContext
    {

        public TestDbContext(DbContextOptions options)
            : base(options) { }


        public DbSet<Entities.Test> Tests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
