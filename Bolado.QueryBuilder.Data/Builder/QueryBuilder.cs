using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolado.QueryBuilder.Data.Builder
{
    public class QueryBuilder<TDto>
        where TDto : class
    {
        private readonly IList<string> _properties;

        public QueryBuilder(string tableName)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

            _properties = typeof(TDto).GetProperties()
                .Select(p => p.Name).ToList();

            Joins = new List<Join>();
        }

        public QueryBuilder(string tableName, IEnumerable<Tuple<string, string>> properties) : this(tableName)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

            _properties = properties.Select(p => $"{p.Item1}.{p.Item2}").ToList();
        }

        public string TableName { get; private set; }
        public IList<Join> Joins { get; private set; }

        public void AddJoin(string type, string originTableName, string joinTableName, string firstField, string condition, string secondField)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(originTableName))
            {
                throw new ArgumentNullException(nameof(originTableName));
            }

            if (string.IsNullOrEmpty(joinTableName))
            {
                throw new ArgumentNullException(nameof(joinTableName));
            }

            if (!_properties.Any(p => p == firstField))
            {
                throw new ArgumentException(nameof(firstField));
            }

            if (!_properties.Any(p => p == secondField))
            {
                throw new ArgumentException(nameof(secondField));
            }

            Joins.Add(new Join(type, originTableName, joinTableName, firstField, condition, secondField));
        }

        public string Build()
        {
            return $@"
            SELECT {string.Join(",", _properties)}
            FROM {TableName}
            {string.Join(" ", Joins.Select(j => j.ToString()))}
            ";
        }
    }
}