using AutoMapper;
using PersonalFinance.Communication.Dtos;
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
    }
    
    private void EntityToResponse()
    {
        CreateMap<User, UserDto>();
    }
}
