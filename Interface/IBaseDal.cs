using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Interface {
    public interface IBaseDal<T> where T : class, new() {

        #region 添加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        T AddEntity(T entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entity">数据集合</param>
        /// <returns></returns>
        List<T> AddEntityList(List<T> entityList);

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        T DeleteEntity(int ID);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        T DeleteEntity(T entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList">数据集合</param>
        /// <returns></returns>
        List<T> DeleteEntityList(List<T> entityList);

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        bool EditEntity(T entity);

        #endregion

        #region 查询数据

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        T FindEntity(int ID);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        T FindEntity(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(string sqlstr);

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlstr">查询条件</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> where);

        #endregion

        #region 事务处理

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        void BeginTrans();

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <param name="tranLevel">事物隔离级别</param>
        void BeginTrans(IsolationLevel tranLevel);

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        int Commit();

        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        void Rollback();

        #endregion

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        int SaveChanges();
    }
}
