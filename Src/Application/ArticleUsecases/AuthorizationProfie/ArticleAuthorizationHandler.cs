using Application.UtilityServicesAbstractions;
using Domain.ArticleAggregate;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ArticleUsecases.AuthorizationProfie
{
    public class ArticleAuthorizationHandler(IUserManager userManager) : AuthorizationHandler<OperationAuthorizationRequirement, Article>
    {
        private readonly IUserManager _userManager = userManager;

        protected override async Task<Task> HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Domain.ArticleAggregate.Article resource)
        {
            var userId = context.User.Claims
                .FirstOrDefault(c => c.Type == "Id").Value;

            switch (requirement.Name)
            {
                case nameof(ArticleAuthorizationRequirements.CreateRequirement):
                    {
                        if (await _userManager.CheckRole(userId, UserRoles.Author))
                            context.Succeed(requirement);
                        break;
                    }
            }

            return Task.CompletedTask;
        }
    }
}
