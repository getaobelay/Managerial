using DAL.Core.Helpers.BaseDtos;
using System.Collections.Generic;

namespace DAL.Core.CommonCQRS.Commands.Responses
{
   
    public abstract class BaseCommandResponse<TDto> : ICommandResponse<TDto>
        where TDto : class, IBaseViewModel, new()

    {
        public TDto ViewModel { get; set; } = new TDto();
        public bool Success { get; set; } = false;
    }
    public class CommandResponse<TDto> : BaseCommandResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public CommandResponse(TDto model, bool success)
        {
            Success = success;
            ViewModel = model;
        }
    }
}