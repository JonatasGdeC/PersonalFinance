using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Participant;
using FinanceService.Domain.Filters;
using FinanceService.Domain.ReadModels;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Participant.GetAll;
using FinanceService.Domain.Entities;

public class GetAllParticipantUseCase(
    IParticipantReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllParticipantUseCase
{
    public async Task<GetAllParticipantResponse> Execute()
    {
        User user = await loggedUser.Get();

        ParticipantFilter filter = new()
        {
            Pagination = new Pagination { PageNumber = 1, PageSize = int.MaxValue }
        };

        PagedList<Participant> participants = await readRepository.GetAll(userId: user.Id, filter: filter);

        return new GetAllParticipantResponse
        {
            ListParticipants = mapper.Map<List<ParticipantDto>>(source: participants.Items)
        };
    }
}
