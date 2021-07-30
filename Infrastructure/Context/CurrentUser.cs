using Domain;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infrastructure.Context
{
    public class CurrentUser : ICurrentUser
    {
        public CurrentUser(ManagerialDbContext context, IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            context.CurrentUserId = GetUsername();
        }

        public IHttpContextAccessor ContextAccessor { get; }

        public bool IsAuthenticated()
        {
            return ContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaims()
        {
            return ContextAccessor.HttpContext.User.Claims;
        }

        public string GetUsername()
        {
            return ContextAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
        }
    }
}