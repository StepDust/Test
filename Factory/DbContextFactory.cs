using Common;
using Models;
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
        /// <param name="ConStrName">连接字符串名字</param>
        /// <returns></returns>
        public static DbContext CreateDbContext(string ConStrName) {
            try {
                // 从线程数据集合（CallContext）中拿对应键值的数据
                DbContext dbContext = (DbContext)CallContext.GetData(ConStrName);
                // 若没有上下文对象
                if (dbContext == null) {
                    // 根据ConStrName，进行分支创建
                    switch (ConStrName) {
                        case Config.Name_Demo:// 
                            dbContext = new  DemoEntities(); break;
                        case Config.Name_GSQ:
                            dbContext = new GSQ_PaChongEntities(); break;
                        case Config.Name_DDS:
                            dbContext = new DDS_KF_NewEntities(); break;
                    }
                    // 创建了实例后存入
                    if (dbContext != null)
                        CallContext.SetData(ConStrName, dbContext);
                }
                return dbContext;
            }
            catch(Exception e) {
                return null;
            }
        }

    }
}
