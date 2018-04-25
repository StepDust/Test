using Common;
using Models;
using Models.CodeFirst;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Factory {
    /// <summary>
    /// 数据库工厂类
    /// </summary>
    public class DbContextFactory {

        /// <summary>
        /// 返回上下文对象，保证线程内唯一
        /// </summary>
        /// <param name="ConStr">连接字符串</param>
        /// <returns></returns>
        public static DbContext CreateDbContext() {
            try {
                // 从线程数据集合（CallContext）中拿对应键值的数据
                DbContext dbContext = (DbContext)CallContext.GetData(typeof(DbContext).AssemblyQualifiedName);
                // 若没有上下文对象
                if (dbContext == null) {
                    dbContext = new CodeFirst();
                    // 创建了实例后存入
                    if (dbContext != null)
                        CallContext.SetData(typeof(DbContext).AssemblyQualifiedName, dbContext);
                }
                return dbContext;
            }
            catch (Exception e) {
                throw e;
            }
        }

    }
}
