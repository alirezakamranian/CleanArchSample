using FluentValidation;
using Presentation.Articale.GetArticles;

namespace Presentation.Articale.GetArticle
{
    public class GetArticleRequestValidator : AbstractValidator<GetArticleRequest>
    {
        public GetArticleRequestValidator()
        {
            RuleFor(g => g.PageSize).NotNull()
                .NotEmpty().LessThan(51).GreaterThan(9);

            RuleFor(g => g.PageIndex).NotNull().GreaterThan(0);
        }
    }
}
