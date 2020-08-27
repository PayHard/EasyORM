using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.EntityAttribute
{
    /// <summary>
    /// 表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(){}
        public TableAttribute(string tableName)
        {
            this.Name = tableName;
        }
        public string Name { get; set; }

        public static string GetName (Type type)
        {
            var attr = type.GetCustomAttributes(typeof(TableAttribute), true)?.FirstOrDefault();
            return (attr as TableAttribute)?.Name ?? type.Name;
        }
    }
}
