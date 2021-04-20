namespace Bolado.QueryBuilder.Domain.Model
{
    public class Phone
    {
        public Phone(int phoneId, string code, string number, bool isWhatsApp = false)
        {
            PhoneId = phoneId;
            Code = code;
            Number = number;
            IsWhatsApp = isWhatsApp;
        }

        public int PhoneId { get; private set; }
        public string Code { get; private set; }
        public string Number { get; private set; }
        public bool IsWhatsApp { get; private set; }
    }
}