namespace FinanceService.Infrastructure.Migrations;

internal static class MigrationContants
{
    internal static class Version
    {
        internal const long TABLE_REGISTER_POTS = 1;
        internal const long TABLE_REGISTER_PARTICIPANT = 2;
        internal const long TABLE_REGISTER_CATEGORY = 3;
        internal const long TABLE_REGISTER_TRANSACTION = 4;
        internal const long TABLE_REGISTER_BUDGET = 5;
        internal const long TABLE_REGISTER_BILL = 6;
    }

    internal static class TableName
    {
        internal const string POTS = "Pots";
        internal const string TRANSACTIONS = "Transactions";
        internal const string PARTICIPANTS = "Participants";
        internal const string CATEGORIES = "Categories";
        internal const string BUDGETS = "Budgets";
        internal const string BILLS = "Bills";
    }
}