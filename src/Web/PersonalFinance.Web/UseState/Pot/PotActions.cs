using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Pot;

public abstract class PotActions
{
    public record DeletePotSuccessAction(Guid PotId);
    public record GetAllPotsAction;
    public record GetAllPotsSuccessAction(List<PotDto> Pots);
    public record GetPotByIdAction(Guid PotId);
    public record GetPotByIdSuccessAction(PotDto Pot);
    public record RegisterPotSuccessAction(PotDto Pot);
    public record UpdatePotSuccessAction(PotDto Pot);
}
