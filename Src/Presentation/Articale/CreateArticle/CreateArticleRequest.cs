using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Presentation.Articale.CreateArticle
{
    public record CreateArticlesRequest(string Title,string Content);
}
