using Application.UserUsecases.CreateUser;
using FluentValidation;
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
            app.MapPost("/user/create", async ([FromBody][Required]CreateUserRequest request, IMediator mediator,IValidator<CreateUserRequest> validator, CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(request, cancellationToken);

                var command = new CreateUserCommand(
                    request.UserName, request.PhoneNumber, request.Password);

                var response = await mediator
                    .Send(command, cancellationToken);

                return response;
            }).WithName("CreateUser").WithTags("User").WithOpenApi().RequireRateLimiting("FixedForUserRegister");
        }
    }
}
