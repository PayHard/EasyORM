using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute() { }
        public ColumnAttribute(string columnName)
        {
            this.Name = columnName;
        }

        public string Name { get; private set; }

        public string GetName(string @default) => this.Name ?? @default;

        public static string GetName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(ColumnAttribute), true)?.FirstOrDefault();
            return (attr as ColumnAttribute)?.Name ?? type.Name;
        }
        public static string GetName(Type type, string defaultName)
        {
            var attr = type.GetCustomAttributes(typeof(ColumnAttribute), true)?.FirstOrDefault();
            return (attr as ColumnAttribute)?.Name ?? defaultName;
        }
    }
}
