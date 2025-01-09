using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Presentation.Articale.CreateArticle
{
    public record CreateArticleRequest([Required]string Title,string Content);
}
