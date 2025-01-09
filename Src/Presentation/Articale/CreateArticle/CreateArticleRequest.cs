using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Presentation.Articale.CreateArticle
{
    public record GetArticlesRequest([Required]string Title,string Content);
}
