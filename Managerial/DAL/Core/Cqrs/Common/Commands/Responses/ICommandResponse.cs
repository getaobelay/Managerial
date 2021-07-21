using DAL.Core.Helpers.BaseDtos;
using System.Collections.Generic;

namespace DAL.Core.CommonCQRS.Commands.Responses
{
    public interface ICommandResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public bool Success { get; set; }
    }
}
