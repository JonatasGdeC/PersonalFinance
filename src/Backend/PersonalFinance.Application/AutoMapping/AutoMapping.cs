using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Requests.Transaction;
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
        CreateMap<RegisterTransactionRequest, Transaction>();
        CreateMap<RegisterBudgetRequest, Budget>();
    }

    private void EntityToResponse()
    {
        CreateMap<User, UserDto>();
        CreateMap<Pot, PotDto>();
        CreateMap<Participant, ParticipantDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Transaction, TransactionDto>()
            .ForMember(destinationMember: dto => dto.ParticipantDto, memberOptions: opt => opt.MapFrom(mapExpression: transaction => transaction.Participant));
        CreateMap<Budget, BudgetDto>();
    }
}
