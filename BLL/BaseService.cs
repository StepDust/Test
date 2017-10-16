using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Interface;

namespace BLL {
    /// <summary>
    /// 数据服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T>  where T : class, new() {

        /// <summary>
        /// 数据服务类
        /// </summary>
        /// <param name="ConStrName">连接字符串名字</param>
        public BaseService(string ConStrName){
            baseDalModel = new BaseDal<T>(ConStrName) as IBaseDal<T>;
        }

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        IBaseDal<T> BaseDalModel {
            get { return baseDalModel; }
        }
        IBaseDal<T> baseDalModel;

        #region 添加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T AddEntity(T entity) {
            return BaseDalModel.AddEntity(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entity">数据集合</param>
        /// <returns></returns>
        public List<T> AddEntityList(List<T> entityList) {
            return BaseDalModel.AddEntityList(entityList);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public T DeleteEntity(int ID) {
            return BaseDalModel.DeleteEntity(ID);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public T DeleteEntity(T entity) {
            return BaseDalModel.DeleteEntity(entity);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList">数据集合</param>
        /// <returns></returns>
        public List<T> DeleteEntityList(List<T> entityList) {
            return BaseDalModel.DeleteEntityList(entityList);
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public bool EditEntity(T entity) {
            return BaseDalModel.EditEntity(entity);
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public T FindEntity(int ID) {
            return BaseDalModel.FindEntity(ID);
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
        /// <param name="sqlstr">SQL语句</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(string sqlstr) {
            return BaseDalModel.LoadEntities(sqlstr);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlstr">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> where) {
            return BaseDalModel.LoadEntities(where);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="sqlstr">查询条件</param>
        /// <param name="field">排序字段</param>
        /// <param name="sqlstr">是否升序，默认：true</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities<s>(Expression<Func<T, bool>> where, Func<T, s> field, bool isAsc = true) {
            if (isAsc)
                return BaseDalModel.LoadEntities(where).OrderBy(field).AsQueryable();
            return BaseDalModel.LoadEntities(where).OrderByDescending(field).AsQueryable();
        }

        /// <summary>
        /// 分页查询数据集
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="info">分页信息</param>
        /// <param name="where">查询条件</param>
        /// <param name="field">排序字段</param>
        /// <param name="isAsc">是否升序，默认：true</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities<s>( IPage info, Expression<Func<T, bool>> where, Func<T, s> field, bool isAsc = true) {
            var dataList = LoadEntities(where, field, isAsc);
            info.PageCount = dataList.Count();
            return dataList.Skip((info.PageIndex - 1) * info.PageSize).Take(info.PageSize);
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

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int SaveChanges() {
            return BaseDalModel.SaveChanges();
        }

        #region 拓展方法

        /// <summary>
        /// 返回指定字段的值
        /// </summary>
        /// <param name="Field">返回的字段</param>
        /// <param name="where">查询条件</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public s GetFieldVal<s>(Expression<Func<T, s>> Field, Expression<Func<T, bool>> where, s def = default(s)) {
            var list = BaseDalModel.LoadEntities(where);
            if (list.Count() > 0)
                return list.Select(Field).FirstOrDefault();
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
    }
}