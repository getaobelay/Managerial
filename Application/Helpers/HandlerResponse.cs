using Application.Interfaces;
using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Helpers
{
    public static class HandlerResponse
    {
        public static HandlerResponse<TDto> NullResponse<TDto>(IEnumerable<string> errorsMessages)
            where TDto : BaseViewModel, new() =>
            new HandlerResponse<TDto>(@object: null,
                                      success: false,
                                      errorsMessages: errorsMessages);

        public static HandlerResponse<TDto> ListResponse<TDto>(IEnumerable<TDto> responseDtos)
            where TDto : BaseViewModel, new() =>
            new HandlerResponse<TDto>(@object: responseDtos,
                                      true,
                                      default);

        public static HandlerResponse<TDto> SingleResponse<TDto>(TDto response)
             where TDto : BaseViewModel, new() =>
             new HandlerResponse<TDto>(response,
                                       true,
                                       default);
    }

    public class HandlerResponse<TDto> : IHandlerResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public HandlerResponse(object @object, bool success, IEnumerable<string> errorsMessages)
        {
            Object = @object;
            Success = success;
            ErrorsMessages = errorsMessages;
        }

        public object Object { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorsMessages { get; set; }
    }
}