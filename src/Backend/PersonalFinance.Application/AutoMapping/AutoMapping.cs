using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Application.AutoMapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<RegisterPotRequest, Pot>();
        CreateMap<RegisterParticipantRequest, Participant>();
        CreateMap<RegisterCategoryRequest, Category>();
    }

    private void EntityToResponse()
    {
        CreateMap<User, UserDto>();
        CreateMap<Pot, PotDto>();
        CreateMap<Participant, ParticipantDto>();
        CreateMap<Category, CategoryDto>();
    }
}
