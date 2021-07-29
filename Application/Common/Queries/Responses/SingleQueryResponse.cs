using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Common.Queries.Responses
{
    public class SingleQueryResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public TDto ViewModel { get; set; }
        public bool Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}