using System;

namespace Service.CodeFirst {
    /// <summary>
    /// 服务抽象类
    /// Date：2018-06-16 22:26:44
    /// </summary>
    public abstract class AbstractService<T> where T : class, new() {
        
        #region 数据库服务

        /// <summary>
        /// 数据库服务
        /// </summary>
        protected DataService<T> ByData => new DataService<T>();

        #endregion

        #region Redis服务

        public RedisService ByRedis => new RedisService();

        #endregion

    }
}
