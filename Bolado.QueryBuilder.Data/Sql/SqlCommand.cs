namespace Bolado.QueryBuilder.Data.Sql
{
    public static class SqlCommand
    {
        public static string GetUserWithPhone { get; private set; }

        public static string InsertUser { get; private set; }
        public static string UpdateUser { get; private set; }
        public static string DeleteUser { get; private set; }

        public static string InsertPhone { get; private set; }
        public static string UpdatePhone { get; private set; }
        public static string DeletePhone { get; private set; }

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

            InsertUser = $@"INSERT INTO User (Name)
            VALUES (@Name)";

            UpdateUser = $@"
            Update User
            SET Name = @Name
            WHERE UserId = @UserId
            ";

            DeleteUser = @"DELETE FROM User WHERE UserId = @UserId";

            InsertPhone = $@"INSERT INTO Phone (UserId, Code, Number, IsWhatsApp)
            VALUES (@UserId, @Code, @Number, @IsWhatsApp)";

            UpdatePhone = $@"
            Update Phone
            SET UserId = @UserId,
                Code = @Code,
                Number = @Number,
                IsWhatsApp = @IsWhatsApp
            WHERE UserId = @UserId
            ";

            DeletePhone = @"DELETE FROM Phone WHERE PhoneId = @PhoneId";
        }
    }
}