
namespace DAL.Core.Cqrs.Common.Queries.Responses
{
    public class SingleQueryResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public TDto ViewModal { get; set; }
        public bool Succes { get; set; }
    }
}