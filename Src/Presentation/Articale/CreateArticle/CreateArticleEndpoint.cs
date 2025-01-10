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
            app.MapPost("/article/create", async ([FromBody][Required] GetArticlesRequest request, IMediator mediator, HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.User.Claims
                    .FirstOrDefault(c => c.Type.Equals("Id")).Value;

                var command = new CreateArticleCommand(
                        request.Title, request.Content, Guid.Parse(userId),httpContext.User);

                var response = await mediator
                    .Send(command, cancellationToken);

                return Results.Ok(response);
            }).WithName("CreateArticle").WithOpenApi().RequireAuthorization().RequireRateLimiting("FixedForCreate");
        }
    }
}
