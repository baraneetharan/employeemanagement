using Dapper;
using System.Data;

namespace EmployeeManagement.Application.Common;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        if (value == null || value == DBNull.Value) 
            return default;
        
        if (value is DateOnly dateOnly)
            return dateOnly;
        
        if (value is DateTime dateTime)
            return DateOnly.FromDateTime(dateTime);
        
        if (value is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
            return DateOnly.Parse(stringValue);
            
        throw new System.InvalidCastException($"Cannot convert {value?.GetType()} to DateOnly");
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        parameter.DbType = DbType.Date;
    }
}
