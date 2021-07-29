﻿using Application.ViewModels;
using Domain.Common;

namespace Application.Common.Commands.Requests
{
    /// <summary>
    /// this command deletes source and destination entities
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TDto">The source ViewModel to map result from</typeparam>
    public class DeleteCommandRequest<TEntity, TDto> : BaseCommandRequest<TEntity, TDto>
      where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    {
        public int Id { get; set; }
    }
}