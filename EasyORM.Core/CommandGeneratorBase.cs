using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Core
{
    public abstract class CommandGeneratorBase
    {
        internal SqlContext Context;

        /// <summary>
        /// 列集合
        /// </summary>
        protected List<string> _columns;
        /// <summary>
        /// 默认查询100条
        /// </summary>
        protected string _limit;
        protected string _alias;
        protected string _where;
        protected string _orderBy;

        //ConcurrentDictionary是线程安全的Dictionary
        private static ConcurrentDictionary<Type, PropertyInfo[]> _propertiesDic = new ConcurrentDictionary<Type, PropertyInfo[]>();
        protected static PropertyInfo[] GetPropertiesDicByType(Type type)
        {
            _propertiesDic.AddOrUpdate(type, type.GetProperties());
            return _propertiesDic[type];
        }

        public abstract string Add<TEntity>(TEntity entity) where TEntity : class;
        public abstract string Delete<TEntity>(TEntity entity) where TEntity : class;
        public abstract string Update<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public abstract string Query<TEntity>() where TEntity : class;
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public abstract string QueryOrderBy<TEntity>() where TEntity : class;
    }
}
