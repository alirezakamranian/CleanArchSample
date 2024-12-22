using Application.Common.Abstractions;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class DataContext(DbContextOptions<DataContext> options):DbContext(options),IDataContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(IInfrastructureAssemblyMarker).Assembly);
        }
    }
}
