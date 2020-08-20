using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.Helper
{
    public class ExpressionHelper
    {
        public static string ConvertOrderBy<TEntity>(Expression<Func<TEntity, object>> orderby) where TEntity : class
        {
            var member = orderby.Body as MemberExpression;
            var unary = orderby.Body as UnaryExpression;
            return member != null ? member.Member.Name : (unary != null ? (unary.Operand as MemberExpression).Member.Name : null);
        }
    }
}
