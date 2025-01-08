using Application.ArticleUsecases.AuthorizationProfie;
using Application.ArticleUsecases.CreateArticle;
using Application.UtilityServicesAbstractions;
using Domain.ArticleAggregate;
using Infrastructure.UtilityServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Presentation.Abstractions;
using Presentation.User.CreateUser;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Articale.CreateArticle
{
    public class CreateArticleEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/article/create", async ([FromBody][Required] CreateArticleRequest request, IMediator mediator, HttpContext httpContext, IAuthorizationService authorizationService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.User.Claims
                    .FirstOrDefault(c => c.Type.Equals("Id")).Value;

                var authorizationResult = await authorizationService
                .AuthorizeAsync(httpContext.User,new Article(), ArticleAuthorizationRequirements.CreateRequirement);

                if (!authorizationResult.Succeeded)
                    return Results.Forbid();

                var command = new CreateArticleCommand(
                        request.Title, request.Content, Guid.Parse(userId));

                var response = await mediator
                    .Send(command, cancellationToken);

                return Results.Ok(response);
            }).WithName("CreateArticle").WithOpenApi().RequireAuthorization();
        }
    }
}
