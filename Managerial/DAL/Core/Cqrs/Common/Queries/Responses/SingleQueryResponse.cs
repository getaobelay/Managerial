using DAL.Core.Helpers.BaseDtos;
using System.Collections.Generic;

namespace DAL.Core.CommonCQRS.Queries.Responses
{
    public class SingleQueryResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public TDto ViewModal { get; set; }
        public bool Succes { get; set; }
    }
}