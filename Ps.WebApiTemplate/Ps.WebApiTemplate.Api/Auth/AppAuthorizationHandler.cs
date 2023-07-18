﻿using Microsoft.AspNetCore.Authorization;
using Ps.EfCoreRepository.SqlServer;
using Ps.WebApiTemplate.Data.DbModels;
using System.Security.Claims;

namespace Ps.WebApiTemplate.Api.Auth
{
    public class AppAuthorizationHandler : AuthorizationHandler<AppAuthorizationRequirement>
    {
        public AppAuthorizationHandler(IRepository repository)
        {
            Repository = repository;
        }
        public IRepository Repository { get; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizationRequirement requirement)
        {
            var emailId = context.User.FindFirst(c => c.Type == ClaimTypes.Email);

            if (emailId == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }


            var emp = Repository.GetById<Employee>(x => x.Email.ToLower() == emailId.Value.ToLower());

            if (emp == null)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
