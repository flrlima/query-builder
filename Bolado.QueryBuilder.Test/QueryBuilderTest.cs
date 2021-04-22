using Bolado.QueryBuilder.Data;
using Bolado.QueryBuilder.Data.Builder;
using Bolado.QueryBuilder.Data.Model;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bolado.QueryBuilder.Test
{
    public class QueryBuilderTest
    {
        [Fact]
        public void BuildingSelectQuery()
        {
            var builder = new QueryBuilder<UserWithPhonesDto>("User");
            builder.AddJoin("LEFT", "User", "Phone", nameof(UserWithPhonesDto.PhoneId), "=", nameof(UserWithPhonesDto.PhoneId));

            var query = builder.BuildSelect();
            true.Should().BeTrue();
        }

        [Fact]
        public void BuildingSelectQueryWithJoin()
        {
            var user = "User";
            var phone = "Phone";

            var properties = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(user, nameof(UserWithPhonesDto.UserId)),
                new Tuple<string, string>(user, nameof(UserWithPhonesDto.Name)),
                new Tuple<string, string>(phone, nameof(UserWithPhonesDto.PhoneId)),
                new Tuple<string, string>(phone, nameof(UserWithPhonesDto.Code)),
                new Tuple<string, string>(phone, nameof(UserWithPhonesDto.Number)),
                new Tuple<string, string>(phone, nameof(UserWithPhonesDto.IsWhatsApp))
            };

            var builder = new QueryBuilder<UserWithPhonesDto>("User", properties);
            builder.AddJoin("LEFT", "User", "Phone", nameof(UserWithPhonesDto.PhoneId), "=", nameof(UserWithPhonesDto.PhoneId));

            var query = builder.BuildSelect();
            true.Should().BeTrue();
        }

        [Fact]
        public void BuildingInsertQuery()
        {
            var builder = new QueryBuilder<PhoneDto>("Phone", nameof(PhoneDto.PhoneId));
            var query = builder.BuildInsert();
            true.Should().BeTrue();
        }

        [Fact]
        public void BuildingUpdateQuery()
        {
            var builder = new QueryBuilder<PhoneDto>("Phone", nameof(PhoneDto.UserId));
            var query = builder.BuildUpdate();
            true.Should().BeTrue();
        }

        [Fact]
        public void BuildingDeleteQuery()
        {
            var builder = new QueryBuilder<UserDto>("User", nameof(UserDto.UserId));
            var query = builder.BuildDelete();
            true.Should().BeTrue();
        }
    }
}