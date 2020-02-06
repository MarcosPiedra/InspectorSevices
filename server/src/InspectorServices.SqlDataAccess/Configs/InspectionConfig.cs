using InspectorServices.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectorServices.SqlDataAccess.Configs
{
    public class InspectionConfig : IEntityTypeConfiguration<Inspection>
    {
        public void Configure(EntityTypeBuilder<Inspection> builder)
        {
            builder.ToTable("Inspection");

            builder.Property(m => m.Id).HasColumnName("Id");
            builder.Property(m => m.Customer).HasColumnName("Customer");
            builder.Property(m => m.Date).HasColumnName("Date")
                                         .HasConversion<string>(); 
            builder.Property(m => m.Observations).HasColumnName("Observations");
            builder.Property(m => m.InspectorId).HasColumnName("InspectorId");
            builder.Property(m => m.Status).HasColumnName("Status")
                                           .HasConversion<int>(); 

            builder.HasKey(a => a.Id);
        }
    }
}
