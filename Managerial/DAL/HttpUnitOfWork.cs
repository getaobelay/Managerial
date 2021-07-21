// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public class HttpUnitOfWork<TEntity> : UnitOfWork<TEntity>
        where TEntity : AuditableEntity, new()
    {
        public HttpUnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpAccessor) : base(context)
        {
            context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
        }
    }
}