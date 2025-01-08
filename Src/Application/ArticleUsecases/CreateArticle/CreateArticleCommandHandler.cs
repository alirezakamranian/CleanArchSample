using Application.Common.Abstractions;
using Domain.ArticleAggregate;
using Domain.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.CreateArticle
{
    public class CreateArticleCommandHandler(IDataContext context) : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
    {
        private readonly IDataContext _context = context;

        public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id
                    .Equals(request.UserId), cancellationToken);

            var article = new Article() { Title = request.Title, Content = request.Content, UserId = request.UserId, CreatedAt = DateTime.UtcNow };

            user.Articles.Add(article);

            await _context.SaveChangesAsync(cancellationToken);

            return new(article.Id.ToString());
        }
    }
}
