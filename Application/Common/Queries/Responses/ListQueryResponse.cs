using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Common.Queries.Responses
{
    public class ListQueryResponse<TDto>
      where TDto : BaseViewModel, new()
    {
        public IEnumerable<TDto> Dtos { get; set; }
        public bool Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}