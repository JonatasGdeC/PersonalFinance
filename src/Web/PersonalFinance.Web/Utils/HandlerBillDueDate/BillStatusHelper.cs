using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Utils.HandlerBillDueDate;

public static class BillStatusHelper
{
    public static bool IsPaid(BillDto bill) => bill.InstallmentsPaid >= bill.InstallmentsTotal;

    public static bool IsDueSoon(BillDto bill) =>
        !IsPaid(bill: bill) && bill.DueDate.Date >= DateTime.Today && bill.DueDate.Date <= DateTime.Today.AddDays(value: 7);

    public static (int Count, double Amount) Summarize(IEnumerable<BillDto> bills)
    {
        List<BillDto> list = bills.ToList();
        return (list.Count, list.Sum(selector: bill => bill.Amount));
    }
}
