using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.GetArticles
{
    public record GetArticlesQuery(int PageSize,int PageIndex):IRequest<GetArticlesQueryResponse>;
}
