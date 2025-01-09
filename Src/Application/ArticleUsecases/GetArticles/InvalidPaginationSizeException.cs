using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.GetArticles
{
    public class InvalidPaginationSizeException():DomainException("PageSize can not be smaller than 1 and bigger than 50. PageIndex can not be smaller than 1");
}
