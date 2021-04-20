using Bolado.QueryBuilder.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Bolado.QueryBuilder.Data
{
    public static class Mapper
    {
        public static IList<User> ToUsers(IEnumerable<UserDto> dtos)
        {
            return dtos.GroupBy(dto => dto.UserId).Select(g => g.First())
                .Select(dto =>
                {
                    var phones = dtos.Where(d => d.UserId == dto.UserId)
                        .Select(d => new Phone(d.PhoneId, d.Code, d.Number, d.IsWhatsApp ?? false))
                        .ToList();

                    return new User(dto.UserId, dto.Name, phones);
                })
                .ToList();
        }
    }
}