using AutoMapper;

namespace DAL.Core.Helpers
{
    public interface IMapFrom<TEntity>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(TEntity), GetType()).ReverseMap();
    }
}