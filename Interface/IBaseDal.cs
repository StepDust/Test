using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Interface {
    public interface IBaseDal<T> where T : class, new() {

        #region 添加数据
        
        T AddEntity(T entity);
        
        IEnumerable<T> AddEntityList(IEnumerable<T> entityList);

        #endregion

        #region 删除数据
        
        bool DeleteEntity(int ID);

        bool DeleteEntity(T entity);
        
        IEnumerable<T> DeleteEntityList(IEnumerable<T> entityList);

        #endregion

        #region 修改数据
        
        bool EditEntity(T entity);

        #endregion

        #region 查询数据
        
        T FindEntity(int ID);
        
        T FindEntity(Expression<Func<T, bool>> where);
        
        IQueryable<K> LoadEntities<K>(string sqlstr);
        
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> where);

        #endregion

        #region 事务处理
        
        void BeginTrans();
        
        void BeginTrans(IsolationLevel tranLevel);
        
        int Commit();
        
        void Rollback();

        #endregion
        
        int SaveChanges();
    }
}
