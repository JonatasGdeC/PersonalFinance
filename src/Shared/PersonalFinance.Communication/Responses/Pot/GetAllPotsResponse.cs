using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Pot;

public record GetAllPotsResponse
{
    public List<PotDto> ListPots { get; set; } = [];
}