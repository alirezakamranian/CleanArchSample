using Application.Common.Abstractions;
using Application.Common.Models;
using Domain.ArticleAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.GetArticles
{
    public class GetArticlesQueryHandler(IDataContext context) : IRequestHandler<GetArticlesQuery, GetArticlesQueryResponse>
    {
        private readonly IDataContext _context = context;

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize < 1 ||
                request.PageSize > 50 ||
                request.PageIndex < 1)
                throw new InvalidPaginationSizeException();

            var articles = await _context.Articles
                .AsNoTracking()
                .OrderBy(a => a.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            var count = await _context.Articles.CountAsync(cancellationToken: cancellationToken);
            var totalPages = (int)Math.Ceiling(count / (double)request.PageSize);

            return new GetArticlesQueryResponse(
                new PaginatedList<Article>(articles,
                    request.PageIndex, totalPages));
        }
    }
}
