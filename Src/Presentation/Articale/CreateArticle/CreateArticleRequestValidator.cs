using FluentValidation;

namespace Presentation.Articale.CreateArticle
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticlesRequest>
    {
        public CreateArticleRequestValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty();
            RuleFor(c => c.Content).NotNull().NotEmpty();
        }
    }
}
