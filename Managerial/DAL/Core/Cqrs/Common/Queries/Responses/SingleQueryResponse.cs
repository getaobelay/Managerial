using DAL.ViewModels.Interfaces;

namespace DAL.Core.Cqrs.Common.Queries.Responses
{
    public class SingleQueryResponse<TDto>
        where TDto : class, IBaseViewModel, new()
    {
        public TDto ViewModal { get; set; }
        public bool Succes { get; set; }
    }
}