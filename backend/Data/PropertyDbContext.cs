using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class PropertyDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<OwnershipLog> OwnershipLogs { get; set; }

        public DbSet<OwnerType> OwnerTypes { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<PropertyStatus> PropertyStatuses { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Renovation> Renovations { get; set; }

        public DbSet<Valuation> Valuations{ get; set; }

        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {

        }
    }


}
