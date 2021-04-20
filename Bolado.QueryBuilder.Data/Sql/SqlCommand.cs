namespace Bolado.QueryBuilder.Data.Sql
{
    public static class SqlCommand
    {
        public static string GetUserWithPhone { get; private set; }

        static SqlCommand()
        {
            GetUserWithPhone = $@"
            SELECT
                User.UserId,
                User.Name,
                Phone.PhoneId,
                Phone.Code,
                Phone.Number,
                Phone.IsWhatsApp
            FROM User
            LEFT JOIN Phone ON Phone.UserId = User.UserId
            ";
        }
    }
}