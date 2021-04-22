using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolado.QueryBuilder.Data.Builder
{
    public class QueryBuilder<TDto>
        where TDto : class
    {
        private readonly IList<string> _properties;

        /// <summary>
        /// use to build any query
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        public QueryBuilder(string tableName, string primaryKey = null)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            PrimaryKey = primaryKey;

            _properties = typeof(TDto).GetProperties()
                .Select(p => p.Name).ToList();

            Joins = new List<Join>();
        }

        /// <summary>
        /// Used to build queries with alias
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="properties"></param>
        /// <param name="primaryKey"></param>
        public QueryBuilder(string tableName, IEnumerable<Tuple<string, string>> properties, string primaryKey = null) : this(tableName, primaryKey)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

            _properties = properties.Select(p => $"{p.Item1}.{p.Item2}").ToList();
        }

        public string TableName { get; private set; }
        public string PrimaryKey { get; private set; }
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

        public string BuildSelect()
        {
            return $@"
            SELECT {string.Join(",", _properties)}
            FROM {TableName}
            {string.Join(" ", Joins.Select(j => j.ToString()))}
            ";
        }

        public string BuildInsert()
        {
            var fields = _properties.Where(p => string.IsNullOrEmpty(PrimaryKey) || PrimaryKey != p);

            return $@"
            INSERT INTO {TableName} ({string.Join(",", fields)})
            VALUES ({string.Join(",", fields.Select(p => $"@{p}"))})
            ";
        }

        public string BuildUpdate()
        {
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                throw new ArgumentNullException(nameof(PrimaryKey));
            }

            var fields = _properties.Where(p => PrimaryKey != p);

            return $@"
            UPDATE {TableName}
            SET {string.Join(",", fields.Select(p => $"{p} = @{p}"))}
            WHERE {PrimaryKey} = @{PrimaryKey}
            ";
        }

        public string BuildDelete()
        {
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                throw new ArgumentNullException(nameof(PrimaryKey));
            }

            return @$"DELETE FROM {TableName} WHERE {PrimaryKey} = @{PrimaryKey}";
        }
    }
}