using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IHandlerResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public bool Success { get; set; }
        public IEnumerable<string> ErrorsMessages { get; set; }
    }
}