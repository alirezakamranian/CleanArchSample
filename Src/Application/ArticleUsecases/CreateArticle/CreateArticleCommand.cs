using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.CreateArticle
{
    public record CreateArticleCommand(string Title,string Content,Guid UserId,ClaimsPrincipal User):IRequest<CreateArticleCommandResponse>;
}
