﻿using Application.UserUsecases.SignInUser;
using FluentValidation;
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
            app.MapPost("/user/signin", async ([FromBody][Required] SignInUserRequest request, IMediator mediator, IValidator<SignInUserRequest> validator, CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(request, cancellationToken);

                var command = new SignInUserCommand(request.UserName,request.Password);

                var response = await mediator
                .Send(command, cancellationToken);

                return response;
            }).WithName("SignInUser").WithTags("User").WithOpenApi();
        }
    }
}
