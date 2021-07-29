using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Common.Commands.Responses
{
    public static class CommandResponse
    {
        public static CommandResponse<TDto> CommandFailed<TDto>(string message, TDto model = default)
            where TDto : BaseViewModel, new()
        {
            return new CommandResponse<TDto>(model: model, messages: new List<string>() { message }, error: true);
        }

        public static CommandResponse<TDto> CommandExecuted<TDto>(string message, TDto model)
            where TDto : BaseViewModel, new()
        {
            return new CommandResponse<TDto>(model: model, messages: new List<string>() { message }, error: false);
        }
    }

    public abstract class BaseCommandResponse<TDto> : ICommandResponse<TDto>
        where TDto : BaseViewModel, new()

    {
        public TDto ViewModel { get; set; } = new TDto();
        public bool Error { get; set; } = false;
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }

    public class CommandResponse<TDto> : BaseCommandResponse<TDto>
        where TDto : BaseViewModel, new()
    {
        public CommandResponse(TDto model, List<string> messages, bool error)
        {
            Error = error;
            ViewModel = model;
            ErrorMessages = messages;
        }
    }
}