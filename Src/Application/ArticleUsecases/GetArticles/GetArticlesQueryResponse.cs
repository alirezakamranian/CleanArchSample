using Application.Common.Models;
using Domain.ArticleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.GetArticles
{
    public record GetArticlesQueryResponse(PaginatedList<Article> Articles);
}
