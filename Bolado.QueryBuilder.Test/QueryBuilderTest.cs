using Bolado.QueryBuilder.Data;
using Bolado.QueryBuilder.Data.Builder;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bolado.QueryBuilder.Test
{
    public class QueryBuilderTest
    {
        [Fact]
        public void BuildingQuery()
        {
            var builder = new QueryBuilder<UserDto>("User");
            builder.AddJoin("LEFT", "User", "Phone", nameof(UserDto.PhoneId), "=", nameof(UserDto.PhoneId));

            var query = builder.Build();
            true.Should().BeTrue();
        }

        [Fact]
        public void BuildingQueryAnotherTime()
        {
            var user = "User";
            var phone = "Phone";

            var properties = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(user, nameof(UserDto.UserId)),
                new Tuple<string, string>(user, nameof(UserDto.Name)),
                new Tuple<string, string>(phone, nameof(UserDto.PhoneId)),
                new Tuple<string, string>(phone, nameof(UserDto.Code)),
                new Tuple<string, string>(phone, nameof(UserDto.Number)),
                new Tuple<string, string>(phone, nameof(UserDto.IsWhatsApp))
            };

            var builder = new QueryBuilder<UserDto>("User", properties);
            builder.AddJoin("LEFT", "User", "Phone", nameof(UserDto.PhoneId), "=", nameof(UserDto.PhoneId));

            var query = builder.Build();
            true.Should().BeTrue();
        }
    }
}