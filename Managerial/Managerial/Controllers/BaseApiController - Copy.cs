using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.loC;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.Interfaces;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Managerial.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class WarehousesController : BaseApiController<Warehouse, WarehouseViewModel>
    {
    }
      
}