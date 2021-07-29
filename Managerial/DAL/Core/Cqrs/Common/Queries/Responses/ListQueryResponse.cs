using System.Collections.Generic;

namespace DAL.Core.Cqrs.Common.Queries.Responses
{
    public class ListQueryResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public IEnumerable<TDto> Dtos { get; set; }
    }
}