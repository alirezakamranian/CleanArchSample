using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u=>u.Id).ValueGeneratedOnAdd();
            builder.OwnsOne(u => u.PhoneNumber, p =>
            {
                p.Property(p => p.Value) 
                    .HasColumnName("PhoneNumber")
                .IsRequired();
            });

            builder.HasOne(b => b.RefreshToken)
            .WithOne(r => r.User)
            .HasForeignKey<UserRefreshToken>(r => r.Id);
        }
    }
}
