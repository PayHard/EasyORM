using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core
{
    class SqlContext
    {
        public string TableName { get; set; }
        /// <summary>
        /// Sql语句
        /// </summary>
        public string SqlText { get; set; }

        /// <summary>
        /// 参数化查询参数
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; }

    }
}
