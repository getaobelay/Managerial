using DAL.ViewModels.Interfaces;

namespace DAL.Core.Cqrs.Common.Commands.Responses
{
    public interface ICommandResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public bool Success { get; set; }
    }
}