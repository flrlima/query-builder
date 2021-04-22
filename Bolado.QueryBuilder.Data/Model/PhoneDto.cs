namespace Bolado.QueryBuilder.Data.Model
{
    public class PhoneDto
    {
        public int PhoneId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public bool? IsWhatsApp { get; set; }
    }
}