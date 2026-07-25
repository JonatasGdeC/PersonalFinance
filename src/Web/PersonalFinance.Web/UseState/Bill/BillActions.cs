using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Bill;

public abstract class BillActions
{
    public record DeleteBillSuccessAction(Guid BillId);
    public record GetAllBillsAction;
    public record GetAllBillsSuccessAction(List<BillDto> Bills);
    public record GetBillByIdAction(Guid BillId);
    public record GetBillByIdSuccessAction(BillDto Bill);
    public record RegisterBillSuccessAction(BillDto Bill);
    public record UpdateBillSuccessAction(BillDto Bill);
}
