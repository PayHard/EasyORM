﻿using EasyORM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyORM.Core;

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

        internal SqlCommandGenerator SqlCommandGenerator;

        #region 添加

        public void Add(TEntity entity)
        {
            //验证参数

            //拼装数据库语句
            var sqlText=SqlCommandGenerator.Add(entity);
            //执行
            conn.Open();
            ExecuteNonQuery(sqlText);
            //释放
            conn.Close();
        }

        public void Add(List<TEntity> entityList)
        {

        }

        #endregion

        #region 删除

        public void Delete(TEntity entity)
        {
            //验证参数

            //拼装数据库语句
            var sqlText = SqlCommandGenerator.Add(entity);
            //执行
            conn.Open();
            ExecuteNonQuery(sqlText);
            //释放
            conn.Close();
        }

        #endregion

        #region 更新

        public void Update(TEntity entity)
        {
            //验证参数

            //拼装数据库语句
            var sqlText = SqlCommandGenerator.Add(entity);
            //执行
            conn.Open();
            ExecuteNonQuery(sqlText);
            //释放
            conn.Close();
        }

        #endregion

        #region 查询

        public TEntity Query()
        {
            //验证参数

            //拼装数据库语句
            
            //执行
            conn.Open();
            //ExecuteNonQuery();
            //释放
            conn.Close();
            return null;
        }

        //public List<TEntity> Query()
        //{

        //    return null;
        //}

        #endregion


        internal int ExecuteNonQuery(string sqlText)
        {
            //创建命令对象，指定要执行sql语句与连接对象conn
            SqlCommand cmd = new SqlCommand(sqlText, conn);
            //执行,返回影响行数
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }

    }
}
