// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using bdim1996_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace bdim1996_dotnet.Persistence.Contexts;

public class RealEstateContext : DbContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RealEstateAd>()
            .ToTable("real_estate_ad");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Id)
            .HasColumnName("id");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Title)
            .HasColumnName("title");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Description)
            .HasColumnName("description");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Address)
            .HasColumnName("address");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Price)
            .HasColumnName("price");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.DateOfCreation)
            .HasColumnName("date_of_creation");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Negotiable)
            .HasColumnName("negotiable");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.Area)
            .HasColumnName("area");
        modelBuilder.Entity<RealEstateAd>()
            .Property(p => p.AgentId)
            .HasColumnName("agent_id");
        modelBuilder.Entity<RealEstateAd>()
            .HasOne(a => a.Agent)
            .WithMany(a => a.RealEstateAds);
        modelBuilder.Entity<RealEstateAgent>()
            .ToTable("real_estate_agent");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Id)
            .HasColumnName("id");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Name)
            .HasColumnName("name");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Email)
            .HasColumnName("email");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Phone)
            .HasColumnName("phone");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Address)
            .HasColumnName("address");
        modelBuilder.Entity<RealEstateAgent>()
            .Property(p => p.Age)
            .HasColumnName("age");
    }

    public DbSet<RealEstateAd> RealEstateAds { get; set; } = null!;
    public DbSet<RealEstateAgent> RealEstateAgents { get; set; } = null!;
}
