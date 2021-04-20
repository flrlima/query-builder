namespace Bolado.QueryBuilder.Data.Builder
{
    public class Join
    {
        public Join(string type, string originTableName, string joinTableName, string firstField, string condition, string secondField)
        {
            Type = type;
            OriginTableName = originTableName;
            JoinTableName = joinTableName;
            FirstField = firstField;
            Condition = condition;
            SecondField = secondField;
        }

        public string Type { get; private set; }
        public string OriginTableName { get; private set; }
        public string JoinTableName { get; private set; }
        public string FirstField { get; private set; }
        public string Condition { get; private set; }
        public string SecondField { get; private set; }

        public override string ToString()
        {
            return $"{Type} JOIN {JoinTableName} ON {JoinTableName}.{FirstField} {Condition} {OriginTableName}.{SecondField}";
        }
    }
}