using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Common.Commands.Responses
{
    public interface ICommandResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public bool Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}