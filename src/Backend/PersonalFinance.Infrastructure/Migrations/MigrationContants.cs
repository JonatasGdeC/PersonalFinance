namespace PersonalFinance.Infrastructure.Migrations;

internal sealed class MigrationContants
{
    internal static class Version
    {
        internal const long TABLE_REGISTER_USER = 1;
        internal const long TABLE_REGISTER_POTS = 2;
        internal const long TABLE_REGISTER_PARTICIPANT = 3;
        internal const long TABLE_REGISTER_CATEGORY = 4;
        internal const long TABLE_REGISTER_TRANSACTION = 5;
        internal const long TABLE_REGISTER_BUDGET = 6;
        internal const long TABLE_REGISTER_BILL = 7;
    }
    
    internal static class TableName
    {
        internal const string USERS = "Users";
        internal const string POTS = "Pots";
        internal const string TRANSACTIONS = "Transactions";
        internal const string PARTICIPANTS = "Participants";
        internal const string CATEGORIES = "Categories";
        internal const string BUDGETS = "Budgets";
        internal const string BILLS = "Bills";
    }
}