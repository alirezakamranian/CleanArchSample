using Application.ArticleUsecases.GetArticles;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Articale.CreateArticle;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Articale.GetArticles
{
    public class GetArticlesEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/article/getall", async ([FromQuery]int pageSize, [FromQuery] int pageIndex, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new GetArticlesQuery(
                    pageSize, pageIndex);

                var response = await mediator
                    .Send(command, cancellationToken);

                List<GetArticlesDto> articles = [];

                foreach (var article in response.Articles.Items)
                {
                    articles.Add(new(
                        article.Id.ToString(),
                        article.Title,
                        article.Content,
                        article.CreatedAt,
                        article.UserId.ToString()));
                }

                var finalResponse = new PaginatedList<GetArticlesDto>(
                    articles,
                    response.Articles.PageIndex,
                    response.Articles.TotalPages);

                return Results.Ok(finalResponse);
            }).WithName("GetAllArticles").WithOpenApi().RequireRateLimiting("FixedForGet");
        }
    }
}
