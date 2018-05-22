using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;
using Factory;
using Interface; 

namespace BLL {

    /// <summary>
    /// 数据访问类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> : IBaseDal<T> where T : class, new() {

        /// <summary>
        /// 数据访问层的公共方法
        /// </summary>
        /// <param name="ConStrName">连接字符串名字</param>
        public BaseDal() {
            dbWrite = DataBaseFactory.CreateDbContext();
        }

        /// <summary>
        /// 数据库上下文对象
        /// </summary>
        DbContext DbWrite { get { return dbWrite; } }

        DbContext dbWrite;

        #region 添加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T AddEntity(T entity) {
            return DbWrite.Set<T>().Add(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entity">数据集合</param>
        /// <returns></returns>
        public IEnumerable<T> AddEntityList(IEnumerable<T> entityList) {
            DbWrite.Set<T>().AddRange(entityList);
            return entityList;
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool DeleteEntity(int ID) {
            T entity = FindEntity(ID);
            return DeleteEntity(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity) {
            if (entity != null) {
                DbWrite.Entry<T>(entity).State = EntityState.Deleted;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList">数据集合</param>
        /// <returns></returns>
        public IEnumerable<T> DeleteEntityList(IEnumerable<T> entityList) {
            DbWrite.Set<T>().RemoveRange(entityList);
            return entityList;
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public bool EditEntity(T entity) {
            // 如果已经被上下文追踪
            if (DbWrite.Entry<T>(entity).State == EntityState.Modified)
                return true;
            if (DbWrite.Entry<T>(entity).State == EntityState.Detached) {
                try {
                    DbWrite.Set<T>().Attach(entity);
                    DbWrite.Entry<T>(entity).State = EntityState.Modified;
                }
                catch {
                    int id = GetPrkVal(entity);
                    string tbName = typeof(T).Name.ToString();
                    string sql = string.Format("select top(1) * from {0} where id='{1}'", tbName, id);
                    T old = DbWrite.Set<T>().SqlQuery(sql).FirstOrDefault();
                    DbWrite.Entry(old).CurrentValues.SetValues(entity);
                }
            }
            return true;
        }

        private int GetPrkVal(T entity) {
            PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return 0; }
            string tbName = typeof(T).Name;
            string colName = "id";
            var item = properties.FirstOrDefault(a => a.Name.ToLower().Equals(colName));
            if (item != null) {
                return (int)item.GetValue(entity, null);
            }
            else {
                return 0;
            }
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public T FindEntity(int ID) {
            // 获取当前的表名
            string tableName = typeof(T).Name;
            string sqlStr = string.Format(" select top(1) * from {0} where id='{1}' ", tableName, ID + "");
            return LoadEntities<T>(sqlStr).FirstOrDefault();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public T FindEntity(Expression<Func<T, bool>> where) {
            return DbWrite.Set<T>().Where(where).FirstOrDefault();
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public IQueryable<K> LoadEntities<K>(string sqlStr) {
            return DbWrite.Database.SqlQuery<K>(sqlStr).AsQueryable();
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlStr">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> where) {
            return DbWrite.Set<T>().Where(where).AsNoTracking();
        }

        #endregion

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
