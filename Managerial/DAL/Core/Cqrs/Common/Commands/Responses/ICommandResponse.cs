using DAL.ViewModels;

namespace DAL.Core.Cqrs.Common.Commands.Responses
{
    public interface ICommandResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public bool Success { get; set; }
    }
}