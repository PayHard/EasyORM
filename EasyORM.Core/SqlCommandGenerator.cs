using EasyORM.Core.EntityAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core
{
    internal class SqlCommandGenerator: CommandGeneratorBase
    {
        public string Add<TEntity>(TEntity entity)
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

                //in the end,remove the redundant symbol of ','
                if (propertyInfos.Last() == propertyInfo)
                {
                    builder_front.Remove(builder_front.Length - 1, 1);
                    builder_front.Append(")");
                    builder_behind.Remove(builder_behind.Length - 1, 1);
                    builder_behind.Append(")");
                }
            }
            //传入生成的sql语句
            return Context.SqlText = builder_front.Append(builder_behind.ToString()).ToString().TrimEnd();
        }

    }
}
