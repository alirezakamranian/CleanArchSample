using Application.ArticleUsecases.AuthorizationProfie;
using Application.Common.Abstractions;
using Domain.ArticleAggregate;
using Domain.Common.Exceptions;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.CreateArticle
{
    public class CreateArticleCommandHandler(IDataContext context, IAuthorizationService authorizationService) : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
    {
        private readonly IDataContext _context = context;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(request.User, Article.Create(), ArticleAuthorizationRequirements.CreateRequirement);

            if (!authorizationResult.Succeeded)
                throw new AccessDeniedException();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id
                    .Equals(request.UserId), cancellationToken);

            var article = new Article(request.Title, request.Content, request.UserId);

            user.Articles.Add(article);

            await _context.SaveChangesAsync(cancellationToken);

            return new(article.Id.ToString());
        }
    }
}
