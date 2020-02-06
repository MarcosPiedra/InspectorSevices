using InspectorServices.Domain.Models;
using InspectorServices.SqlDataAccess.Configs;
using Microsoft.EntityFrameworkCore;
using System;

namespace InspectorServices.SqlDataAccess
{
    public class InspectorContext : DbContext
    {
        public InspectorContext() : base()
        {
        }

        public InspectorContext(DbContextOptions<InspectorContext> options) : base(options)
        {
        }

        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InspectionConfig());
            modelBuilder.ApplyConfiguration(new InspectorConfig());
        }
    }
}
