using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstractions
{
    public interface IDataContext 
    {
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry Entry(object entity);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        ReferenceEntry Reference(object entity, string navigationProperty);

        void RemoveRecord<TEntity>(TEntity entity) where TEntity : class;
    }
}
