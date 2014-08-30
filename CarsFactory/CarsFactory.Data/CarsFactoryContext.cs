﻿namespace CarsFactory.Data
{
    using System.Data.Entity;
    using Models;

    public class CarsFactoryContext : DbContext
    {

        public CarsFactoryContext()
            : base("CarsFactory")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarsFactoryContext, Configuration>());
        }

        public CarsFactoryContext(string connectionString)
            : this()
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SalesReport> SalesReports { get; set; }

        public DbSet<Town> Towns { get; set; }
    }
}