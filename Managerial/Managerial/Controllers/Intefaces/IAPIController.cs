using DAL.Core.Helpers.BaseDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PAL.Controllers.Intefaces
{
    public interface IAPIController<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        Task<IActionResult> DeleteAsync(int Id);
        Task<IActionResult> GetAllAsync([FromRoute] int page, [FromRoute] int pageSize);
        Task<IActionResult> GetByIdAsync(int Id);
        Task<IActionResult> PostAsync(TDto createdObject);
        Task<IActionResult> PutAsync(int Id, TDto updatedObject);
    }
}