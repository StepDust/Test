using System;
using Factory;
using Interface;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Service {

    /// <summary>
    /// 数据库服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataService<T> where T : class, new() {

        /// <summary>
        /// 数据服务类
        /// </summary>
        public DataService() {
            baseDalModel = DataBaseFactory.CreateService<BaseData<T>>();
        }

        private IBaseData<T> baseDalModel;

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private IBaseData<T> BaseDalModel => baseDalModel;
        
        #region 添加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public T AddEntity(T entity, bool IsSave = true) {
            BaseDalModel.AddEntity(entity);
            if (IsSave)
                if (SaveChanges() <= 0)
                    return null;
            return entity;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entity">数据集合</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public IEnumerable<T> AddEntityList(IEnumerable<T> entityList, bool IsSave = true) {
            BaseDalModel.AddEntityList(entityList);
            if (IsSave)
                if (SaveChanges() <= 0)
                    return null;
            return entityList;
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public int DeleteEntity(int Id, bool IsSave = true) {
            BaseDalModel.DeleteEntity(Id);
            if (IsSave)
                return SaveChanges();
            else
                return 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public int DeleteEntity(T entity, bool IsSave = true) {
            BaseDalModel.DeleteEntity(entity);
            if (IsSave)
                return SaveChanges();
            else
                return 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList">数据集合</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public int DeleteEntityList(IEnumerable<T> entityList, bool IsSave = true) {
            BaseDalModel.DeleteEntityList(entityList);
            if (IsSave)
                return SaveChanges();
            else
                return 0;
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="IsSave">是否立即保存</param>
        /// <returns></returns>
        public int EditEntity(T entity, bool IsSave = true) {
            BaseDalModel.EditEntity(entity);
            if (IsSave)
                return SaveChanges();
            else
                return 0;
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns></returns>
        public T FindEntity(int Id) {
            return BaseDalModel.FindEntity(Id);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public T FindEntity(Expression<Func<T, bool>> where) {
            return BaseDalModel.FindEntity(where);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public IQueryable<K> LoadEntities<K>(string sqlStr) {
            return BaseDalModel.LoadEntities<K>(sqlStr);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlStr">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> where) {
            return BaseDalModel.LoadEntities(where);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlStr">查询条件</param>
        /// <param name="field">排序字段</param>
        /// <param name="sqlStr">是否升序，默认：true</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities<s>(Expression<Func<T, bool>> where, Func<T, s> field, bool isAsc = true) {
            if (isAsc)
                return BaseDalModel.LoadEntities(where).OrderBy(field).AsQueryable();
            return BaseDalModel.LoadEntities(where).OrderByDescending(field).AsQueryable();
        }

        #endregion

        #region 事务处理

        /// <summary>
        /// 事务开始
        /// </summary>
        /// <param name="tranLevel">事物隔离级别</param>
        public void BeginTrans() {
            IsolationLevel tranLevel = IsolationLevel.ReadUncommitted;
            BaseDalModel.BeginTrans(tranLevel);
        }

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit() {
            return BaseDalModel.Commit();
        }

        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback() {
            BaseDalModel.Rollback();
        }

        #endregion
        
        #region 拓展方法

        /// <summary>
        /// 返回指定字段的值
        /// </summary>
        /// <param name="field">返回的字段</param>
        /// <param name="where">查询条件</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public s GetFieldVal<s>(Expression<Func<T, s>> field, Expression<Func<T, bool>> where, s def = default(s)) {
            var list = BaseDalModel.LoadEntities(where);
            if (list.Count() > 0)
                return list.Select(field).FirstOrDefault();
            return def;
        }

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> where) {
            return BaseDalModel.LoadEntities(where).Count() > 0;
        }

        #endregion

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int SaveChanges() => BaseDalModel.SaveChanges();
    }
}