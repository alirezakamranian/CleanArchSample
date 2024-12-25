using Application.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Presentation.User.CreateUser
{
    public class CreateUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/login", async ([FromBody][Required]CreateUserRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new CreateUserCommand(
                    request.UserName, request.PhoneNumber, request.Password);

                var response = await mediator
                .Send(command, cancellationToken);

                return response;
            }).WithName("CreateUser").WithOpenApi();
        }
    }
}
