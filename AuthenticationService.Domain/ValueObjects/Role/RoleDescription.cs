using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.Role
{
    public class RoleDescription : ValueObject
    {
        public string Description { get; }

        protected RoleDescription() { }

        protected RoleDescription(string description)
        {
            Description = description;
        }

        public static IExecutionResult<RoleDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return ExecutionResult.Failure<RoleDescription>(RoleError.DescriptionEmpty());

            return ExecutionResult.Success(new RoleDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}
