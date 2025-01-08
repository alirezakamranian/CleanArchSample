using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public abstract class BaseAuthorizationRequirements
    {
        public static OperationAuthorizationRequirement GetRequirement { get; set; } =
          new() { Name = nameof(GetRequirement) };

        public static OperationAuthorizationRequirement CreateRequirement { get; set; } =
           new() { Name = nameof(CreateRequirement) };

        public static OperationAuthorizationRequirement UpdateRequirement { get; set; } =
            new() { Name = nameof(UpdateRequirement) };

        public static OperationAuthorizationRequirement DeleteRequirement { get; set; } =
            new() { Name = nameof(DeleteRequirement) };
    }
}
