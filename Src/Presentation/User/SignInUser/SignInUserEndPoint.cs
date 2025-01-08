using Application.UserUsecases.SignInUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.User.CreateUser;
using System.ComponentModel.DataAnnotations;

namespace Presentation.User.SignInUser
{
    public class SignInUserEndPoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/signin", async ([FromBody][Required] SignInUserRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new SignInUserCommand(request.UserName,request.Password);

                var response = await mediator
                .Send(command, cancellationToken);

                return response;
            }).WithName("SignInUser").WithOpenApi();
        }
    }
}
