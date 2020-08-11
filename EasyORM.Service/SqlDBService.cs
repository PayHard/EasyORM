using EasyORM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyORM.Service
{
    public class SqlDBService<TEntity>where TEntity:class
    {
        SqlConnection conn;
        public SqlDBService(ConnStr conStr)
        {
            conn = new SqlConnection($"Data Source={conStr.DataSource};Initial Catalog={conStr.InitialCatalog};User ID={conStr.UserId};Password={conStr.Password};");
        }

        public SqlDBService(string conStr)
        {
            conn = new SqlConnection(conStr);
        }


        #region 添加

        public void Add(TEntity entity)
        {

        }

        public void Add(List<TEntity> entityList)
        {

        }

        #endregion

        #region 删除



        #endregion

        #region 更新



        #endregion

        #region 查询

        public TEntity Query()
        {

            return null;
        }

        //public List<TEntity> Query()
        //{

        //    return null;
        //}

        #endregion

    }
}
