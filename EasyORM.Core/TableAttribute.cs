using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    class TableAttribute:Attribute
    {
        public TableAttribute() { }
        public TableAttribute(string tableName)
        {
            this.Name = tableName;
        }

        public string Name { get; private set; }

        public static string GetName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(TableAttribute), true)?.FirstOrDefault();
            return (attr as TableAttribute)?.Name ?? type.Name;
        }
    }
}
