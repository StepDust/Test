using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;
using System.Configuration;
using Factory;

namespace DAL {
    /// <summary>
    /// 数据访问层的公共方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> where T : class, new() {

        /// <summary>
        /// 数据访问层的公共方法
        /// </summary>
        /// <param name="ConStrName">连接字符串名字</param>
        public BaseDal(string ConStrName) {
            dbWrite = DbContextFactory.CreateDbContext(ConStrName);
        }

        /// <summary>
        /// 数据库上下文对象
        /// </summary>
        DbContext DbWrite {
            get { return dbWrite; }
        }
        DbContext dbWrite;

        #region 事务处理

        /// <summary>
        /// 事务对象
        /// </summary>
        DbTransaction Tran { get; set; }

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public void BeginTrans() {
            DbConnection dbConnection = ((IObjectContextAdapter)DbWrite).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed) {
                dbConnection.Open();
            }
            Tran = dbConnection.BeginTransaction();
        }

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <param name="tranLevel">事物隔离级别</param>
        public void BeginTrans(IsolationLevel tranLevel) {
            DbConnection dbConnection = ((IObjectContextAdapter)DbWrite).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed) {
                dbConnection.Open();
            }
            Tran = dbConnection.BeginTransaction(tranLevel);
        }

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit() {
            try {
                int returnValue = SaveChanges();
                if (Tran != null) {
                    Tran.Commit();
                }
                return returnValue;
            }
            catch (Exception ex) {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException) {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    string msg = sqlEx.Message;
                }
                throw;
            }
        }

        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback() {
            this.Tran.Rollback();
            this.Tran.Dispose();
        }

        #endregion

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int SaveChanges() {
            return DbWrite.SaveChanges();
        }

    }
}
