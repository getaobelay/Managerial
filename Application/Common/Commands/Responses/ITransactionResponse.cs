using Application.ViewModels;
using Domain.Common;

namespace Application.Common.Commands.Responses
{
    public interface ITransactionResponse<TSourceDto, TDestDto>
        where TSourceDto : AuditableEntity, new()
        where TDestDto : BaseViewModel, new()
    {
    }
}