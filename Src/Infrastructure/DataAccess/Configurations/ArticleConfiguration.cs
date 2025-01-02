using Domain.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Configurations
{
    public class ArticleConfigurationL : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId);
        }
    }
}
