using DAL.Core.Helpers.BaseDtos;
using System.Collections.Generic;

namespace DAL.Core.CommonCQRS.Queries.Responses
{

    public class ListQueryResponse<TDto> 
        where TDto : class, IBaseViewModel, new()
    {
        public IEnumerable<TDto> Dtos { get; set; }
    }
}