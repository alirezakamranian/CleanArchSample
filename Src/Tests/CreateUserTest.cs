using Application.User.CreateUser;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Tests
{
    public class CreateUserTest
    {
        [Fact]
        public async void ShouldReturnCreatedUserId()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=127.0.0.1;Database=CleanDb;User Id=SA;Password=12230500o90P;TrustServerCertificate=True").Options;
            using var _context = new DataContext(contextOptions);

            CreateUserCommand request = new("test", "test");

            CreateUserCommandHandler handler = new(_context);

            CreateUserCommandResponse response = 
                await handler.Handle(request,new CancellationToken());

            await _context.Users.Where(u => u.Id.Equals(
                Guid.Parse(response.Id))).ExecuteDeleteAsync();

            Assert.NotNull(response);
            Assert.IsType<string>(response.Id);
        }
    }
}
