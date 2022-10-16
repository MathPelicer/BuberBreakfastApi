using AutoMapper;
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;

namespace BuberBreakfast.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Breakfast, BreakfastResponse>();
        }
    }
}
