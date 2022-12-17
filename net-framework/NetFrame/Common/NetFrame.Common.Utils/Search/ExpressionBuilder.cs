using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace NetFrame.Common.Utils.Search
{
    /// <summary>
    /// Expression işlemlerinin yapılmasını sağlayan sınıf
    /// </summary>
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains")!;
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) , typeof(bool), typeof(CultureInfo)})!;
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string), typeof(bool), typeof(CultureInfo) })!;

        /// <summary>
        /// Gelen SearchFilter değerlerine göre Expression üretmeyi sağlar.
        /// </summary>
        /// <param name="filters">Filtre parametreleri</param>
        /// <typeparam name="T">Sınıf tipi</typeparam>
        /// <returns>Oluşturulan expression değerini döner</returns>
        public static Expression<Func<T, bool>> GetExpression<T>(IList<SearchFilter> filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");

            if (filters.Count == 0)
                return Expression.Lambda<Func<T, bool>>(GetExpression<T>(param, new SearchFilter()) , false);

            Expression? exp = null;
             
            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            return Expression.Lambda<Func<T, bool>>(exp!, param);
        }

        /// <summary>
        /// Expression oluşturmayı sağlar.
        /// </summary>
        /// <typeparam name="T">Sınıf tipi</typeparam>
        /// <param name="param">Expression parametreleri</param>
        /// <param name="filter">Expression filter</param>
        /// <returns>Gelen parametrelere göre expression oluşturur</returns>
        // ReSharper disable once UnusedTypeParameter
        private static Expression GetExpression<T>(ParameterExpression param, SearchFilter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);

            ConstantExpression filterValue = Expression.Constant(filter.Value, member.Type);
            var ignoreCase = Expression.Constant(true, typeof(bool));
            var turkishCulture = Expression.Constant(new CultureInfo("tr-TR"), typeof(CultureInfo));
            switch (filter.Operation)
            {
                case ConditionType.Equals:
                    return Expression.Equal(member, filterValue);

                case ConditionType.GreaterThan:
                    return Expression.GreaterThan(member, filterValue);

                case ConditionType.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, filterValue);

                case ConditionType.LessThan:
                    return Expression.LessThan(member, filterValue);

                case ConditionType.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, filterValue);

                case ConditionType.Contains:
                    return Expression.Call(member, ContainsMethod, filterValue);

                case ConditionType.StartsWith:
                    return Expression.Call(member, StartsWithMethod, filterValue, ignoreCase, turkishCulture);

                case ConditionType.EndsWith:
                    return Expression.Call(member, EndsWithMethod, filterValue, ignoreCase, turkishCulture);
            }

            return null!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="filter1"></param>
        /// <param name="filter2"></param>
        /// <returns></returns>
        private static BinaryExpression GetExpression<T>(ParameterExpression param, SearchFilter filter1, SearchFilter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
}
