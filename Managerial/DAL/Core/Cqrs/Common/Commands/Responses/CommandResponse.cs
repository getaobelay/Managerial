using DAL.ViewModels;

namespace DAL.Core.Cqrs.Common.Commands.Responses
{
    public abstract class BaseCommandResponse<TDto> : ICommandResponse<TDto>
        where TDto : BaseViewModel, new()

    {
        public TDto ViewModel { get; set; } = new TDto();
        public bool Success { get; set; } = false;
    }

    public class CommandResponse<TDto> : BaseCommandResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public CommandResponse(TDto model, bool success)
        {
            Success = success;
            ViewModel = model;
        }
    }
}