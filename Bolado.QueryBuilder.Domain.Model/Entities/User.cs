using System.Collections.Generic;

namespace Bolado.QueryBuilder.Domain.Model
{
    public class User
    {
        public User(int id, string name)
        {
            UserId = id;
            Name = name;
        }

        public User(int userId, string name, IList<Phone> phones)
        {
            UserId = userId;
            Name = name;
            Phones = phones;
        }

        public int UserId { get; private set; }
        public string Name { get; private set; }
        public virtual IList<Phone> Phones { get; private set; }
    }
}