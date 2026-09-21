using System.Linq.Expressions;
using System.Reflection;
using Delex_POS.Application.Common.Enums;

namespace Delex_POS.Application.Common.Extensions;

public static class LinqExtensions
{
    private static PropertyInfo GetPropertyInfo(Type objType, string name)
    {
        var properties = objType.GetProperties();
        var matchedProperty =
            Array.Find(properties, p => String.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        if (matchedProperty == null)
            throw new ArgumentException(null, nameof(name));

        return matchedProperty;
    }

    private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
    {
        var paramExpr = Expression.Parameter(objType);
        var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
        var expr = Expression.Lambda(propAccess, paramExpr);
        return expr;

    }

    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name,
        TableSort sort = TableSort.ASC)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);
        var expr = GetOrderExpression(typeof(T), propInfo);

        var orderType = sort == TableSort.DESC ? "OrderByDescending" : "OrderBy";

        var method = Array.Find(typeof(Enumerable).GetMethods(),
            m => m.Name == orderType && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(name), nameof(method));

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
        return (IEnumerable<T>)genericMethod!.Invoke(null, new object[] { query, expr.Compile() })!;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name,
        TableSort sort = TableSort.DESC)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);
        var expr = GetOrderExpression(typeof(T), propInfo);

        var orderType = sort == TableSort.DESC ? "OrderByDescending" : "OrderBy";

        var method = Array.Find(typeof(Queryable).GetMethods(),
            m => m.Name == orderType && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(name), nameof(method));

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
        return (IQueryable<T>)genericMethod!.Invoke(null, new object[] { query, expr })!;
    }
}
