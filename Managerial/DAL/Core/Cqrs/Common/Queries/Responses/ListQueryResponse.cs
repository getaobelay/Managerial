using DAL.ViewModels.Interfaces;
using System.Collections.Generic;

namespace DAL.Core.Cqrs.Common.Queries.Responses
{
    public class ListQueryResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public IEnumerable<TDto> Dtos { get; set; }
    }
}