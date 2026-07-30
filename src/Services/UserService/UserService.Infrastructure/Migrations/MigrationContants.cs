namespace UserService.Infrastructure.Migrations;

internal static class MigrationContants
{
    internal static class Version
    {
        internal const long TABLE_REGISTER_USER = 1;
        internal const long TABLE_PASSWORD_RESET_CODE = 2;
    }
    
    internal static class TableName
    {
        internal const string USERS = "Users";
        internal const string PASSWORD_RESET_CODES = "PasswordResetCodes";
    }
}