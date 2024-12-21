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
        public CreateUserTest()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                 .UseSqlServer("Server=127.0.0.1;Database=CleanDb;User Id=SA;Password=12230500o90P;TrustServerCertificate=True").Options;
            _context = new DataContext(contextOptions);

            _handler = new(_context);
        }

        private readonly DataContext _context;
        private readonly CreateUserCommandHandler _handler;


        [Fact]
        public async void ShouldReturnCreatedUserId()
        {
            CreateUserCommand request = new("test", "test");

            CreateUserCommandResponse response = 
                await _handler.Handle(request,new CancellationToken());

            await _context.Users.Where(u => u.Id.Equals(
                Guid.Parse(response.Id))).ExecuteDeleteAsync();

            Assert.NotNull(response);
            Assert.IsType<string>(response.Id);
        }
    }
}
