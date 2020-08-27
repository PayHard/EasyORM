using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core.EntityAttribute
{
    /// <summary>
    /// 数据库
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DataBaseAttribute : Attribute
    {
        public DataBaseAttribute() { }
        public DataBaseAttribute(string databaseName)
        {
            this.Name = databaseName;
        }
        public string Name { get; private set; }

        public static string GetName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(DataBaseAttribute), true)?.FirstOrDefault();
            return (attr as DataBaseAttribute)?.Name ?? type.Name;
        }
    }
}
