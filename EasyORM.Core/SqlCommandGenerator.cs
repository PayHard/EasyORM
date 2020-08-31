using EasyORM.Core.EntityAttribute;
using EasyORM.Core.Enum;
using EasyORM.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core
{
    public class SqlCommandGenerator : CommandGeneratorBase
    {
        public void SetAlias(string alias)
        {
            this._alias = alias;
        }
        public void SetLimit(int limit)
        {
            this._limit = $"TOP {limit}";
        }
        //Expression表达式树
        //表达式树是把Lambda表达式存起来。在一些底层里面可以作为SQL的where条件使用。
        public void SetOrderBy<T>(OrderBy orderBy, Expression<Func<T, object>> orderByExpression) where T : class
        {
            if (orderBy == OrderBy.Desc)
                this._orderBy = $"ORDER BY {ExpressionHelper.ConvertOrderBy(orderByExpression)} DESC";
            else
                this._orderBy = $"ORDER BY {ExpressionHelper.ConvertOrderBy(orderByExpression)} ASC";
        }
        public override string Add<TEntity>(TEntity entity)
        {
            Context.TableName = TableAttribute.GetName(typeof(TEntity));
            Context.Parameters = new Dictionary<string, object>();

            StringBuilder builder_front = new StringBuilder(), builder_behind = new StringBuilder();
            builder_front.Append("INSERT INTO ");
            builder_front.Append(Context.TableName);
            builder_front.Append(" (");
            builder_behind.Append(" VALUES (");

            PropertyInfo[] propertyInfos = GetPropertiesDicByType(typeof(TEntity));
            string columnName = string.Empty;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //自增字段不拼进字段
                if (propertyInfo.GetCustomAttribute(typeof(AutoAttribute), true) is AutoAttribute)
                    continue;
                //Column
                if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute column)
                {
                    builder_front.Append(column.GetName(propertyInfo.Name));
                    builder_front.Append(",");
                    builder_behind.Append("@");
                    columnName = column.GetName(propertyInfo.Name).Replace("[", "").Replace("]", "");
                    builder_behind.Append(columnName);
                    builder_behind.Append(",");
                    Context.Parameters.AddOrUpdate($"@{columnName}", propertyInfo.GetValue(entity));
                }
            }

            //去除末尾‘,’
            builder_front.Remove(builder_front.Length - 1, 1);
            builder_front.Append(")");
            builder_behind.Remove(builder_behind.Length - 1, 1);
            builder_behind.Append(")");

            //传入生成的sql语句
            return Context.SqlText = builder_front.Append(builder_behind.ToString()).ToString().TrimEnd();
        }

        public override string Delete<TEntity>(TEntity entity)
        {
            Context.Parameters = new Dictionary<string, object>();
            Context.TableName = TableAttribute.GetName(typeof(TEntity));
            PropertyInfo[] propertyInfos = GetPropertiesDicByType(typeof(TEntity));
            //get property which is key
            var property = propertyInfos.Where(t => t.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute)?.FirstOrDefault();

            if (property == null)
                throw new Exception($"table '{Context.TableName}' not found key column");

            string colunmName = property.Name;
            var value = property.GetValue(entity);

            if (property.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute columnAttr)
                colunmName = columnAttr.GetName(property.Name);

            Context.Parameters.AddOrUpdate($"@t{colunmName}", value);
            return Context.SqlText = $"DELETE t FROM {Context.TableName} t WHERE t.{colunmName} = @t{colunmName}".TrimEnd();
        }

        public override string Update<TEntity>(TEntity entity)
        {
            Context.TableName = TableAttribute.GetName(typeof(TEntity));
            Context.Parameters = new Dictionary<string, object>();

            StringBuilder builder_front = new StringBuilder(), builder_behind = new StringBuilder();
            builder_front.Append("UPDATE ");
            builder_front.Append(Context.TableName);
            builder_front.Append(" SET ");

            PropertyInfo[] propertyInfos = GetPropertiesDicByType(typeof(TEntity));
            string columnName = string.Empty;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //自增字段不拼进字段
                if (propertyInfo.GetCustomAttribute(typeof(AutoAttribute), true) is AutoAttribute)
                {
                    //默认主键是Where条件
                    if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyColumn)
                    {
                        builder_behind.Append(" WHERE ");
                        builder_behind.Append(keyColumn.GetName(propertyInfo.Name));
                        builder_behind.Append("=");
                        builder_behind.Append($"@t");
                        columnName = keyColumn.GetName(propertyInfo.Name).Replace("[", "").Replace("]", "");
                        builder_behind.Append(columnName);
                        Context.Parameters.AddOrUpdate($"@t{columnName}", propertyInfo.GetValue(entity));
                    }
                    continue;
                }
                //Column
                if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute column)
                {
                    builder_front.Append(column.GetName(propertyInfo.Name));
                    builder_front.Append("=");
                    builder_front.Append($"@t");
                    columnName = column.GetName(propertyInfo.Name).Replace("[", "").Replace("]", "");
                    builder_front.Append(columnName);
                    builder_front.Append(",");
                    Context.Parameters.AddOrUpdate($"@t{columnName}", propertyInfo.GetValue(entity));
                }
                //in the end,remove the redundant symbol of ','
                if (propertyInfos.Last() == propertyInfo)
                {
                    builder_front.Remove(builder_front.Length - 1, 1);
                }
            }
            //传入生成的sql语句
            return Context.SqlText = builder_front.Append(builder_behind.ToString()).ToString().TrimEnd();
        }

        public override string Query<TEntity>()
        {
            string queryColumns = (_columns == null || !_columns.Any()) ? "*" : string.Join(",", _columns.Select(t => $"{_alias}.{t}"));
            return Context.SqlText = $"SELECT {queryColumns} FROM {Context.TableName} ".TrimEnd();
        }
        public override string QueryOrderBy<TEntity>()
        {
            string queryColumns = (_columns == null || !_columns.Any()) ? "*" : string.Join(",", _columns.Select(t => $"{_alias}.{t}"));
            return Context.SqlText = $"SELECT {_limit} {queryColumns} FROM {Context.TableName} {_alias} {_where} {_orderBy}".TrimEnd();
        }

    }
}
