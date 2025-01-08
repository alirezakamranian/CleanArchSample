using System.ComponentModel.DataAnnotations;

namespace Presentation.Articale.CreateArticle
{
    public record CreateArticleRequest([Required]string Title,string Content);
}
