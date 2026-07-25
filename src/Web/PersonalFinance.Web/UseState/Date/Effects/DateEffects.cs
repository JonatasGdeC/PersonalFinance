using Fluxor;
using PersonalFinance.Adapter.Services;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Bill;
using PersonalFinance.Communication.Responses.Budget;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Responses.Participant;
using PersonalFinance.Communication.Responses.Pot;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Web.UseState.Bill;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Participant;
using PersonalFinance.Web.UseState.Pot;
using PersonalFinance.Web.UseState.Transaction;

namespace PersonalFinance.Web.UseState.Date.Effects;

public class DateEffects(PersonalFinanceApi personalFinanceApi)
{
    [EffectMethod]
    public async Task HandleChangeDate(DateActions.ChangeDateAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesSuccessAction(Categories: []));
        dispatcher.Dispatch(action: new ParticipantActions.GetAllParticipantsSuccessAction(Participants: []));
        dispatcher.Dispatch(action: new TransactionActions.GetAllTransactionsSuccessAction(Transactions: []));
        dispatcher.Dispatch(action: new BudgetActions.GetAllBudgetsSuccessAction(Budgets: []));
        dispatcher.Dispatch(action: new PotActions.GetAllPotsSuccessAction(Pots: []));
        dispatcher.Dispatch(action: new BillActions.GetAllBillsSuccessAction(Bills: []));

        GetAllCategoryResponse? categories = await personalFinanceApi.Category.GetAll();
        if (categories != null)
        {
            dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesSuccessAction(Categories: categories.ListCategories));
        }

        GetAllParticipantResponse? participants = await personalFinanceApi.Participant.GetAll();
        if (participants != null)
        {
            dispatcher.Dispatch(action: new ParticipantActions.GetAllParticipantsSuccessAction(Participants: participants.ListParticipants));
        }

        GetListTransactionsResponse? transactions = await personalFinanceApi.Transaction.GetAll(request: new TransactionFilterRequest());
        if (transactions != null)
        {
            dispatcher.Dispatch(action: new TransactionActions.GetAllTransactionsSuccessAction(Transactions: transactions.ListTransactions));
        }

        GetAllBudgetResponse? budgets = await personalFinanceApi.Budget.GetAll();
        if (budgets != null)
        {
            dispatcher.Dispatch(action: new BudgetActions.GetAllBudgetsSuccessAction(Budgets: budgets.ListBudgets));
        }

        GetAllPotsResponse? pots = await personalFinanceApi.Pot.GetAll();
        if (pots != null)
        {
            dispatcher.Dispatch(action: new PotActions.GetAllPotsSuccessAction(Pots: pots.ListPots));
        }

        GetAllBillResponse? bills = await personalFinanceApi.Bill.GetAll(filter: new BillFilterRequest());
        if (bills != null)
        {
            dispatcher.Dispatch(action: new BillActions.GetAllBillsSuccessAction(Bills: bills.ListBills));
        }
    }
}
