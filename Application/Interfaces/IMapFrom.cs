using AutoMapper;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IMapFrom<TEntity>
        where TEntity : AuditableEntity, new()
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(TEntity), GetType()).ReverseMap();
    }
}