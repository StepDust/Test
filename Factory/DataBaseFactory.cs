using Common;
using Common.Actions;
using Interface;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Factory {
    /// <summary>
    /// 数据库工厂类
    /// </summary>
    public class DataBaseFactory {

        private static TResult Create<TResult>(Func<TResult> func) {
            try {
                // 从线程数据集合（CallContext）中拿对应键值的数据
                TResult model = (TResult)CallContext.GetData(typeof(TResult).AssemblyQualifiedName);
                // 若没有上下文对象
                if (model == null) {
                    model = func.Invoke();
                    // 创建了实例后存入
                    if (model != null)
                        CallContext.SetData(typeof(TResult).AssemblyQualifiedName, model);
                }
                return model;
            }
            catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// 返回上下文对象，保证线程内唯一
        /// </summary>
        /// <param name="ConStr">连接字符串</param>
        /// <returns></returns>
        public static DbContext CreateDbContext() {
            return Create(() =>
                Reflex.CreateModel<DbContext>(Constant.DataBaseContext[0], Constant.DataBaseContext[1]));
        }

        /// <summary>
        /// 返回DAL层表对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IBaseDal<T> CreateDalBase<T>() where T : class, new() {
            return Create(() =>
                Reflex.CreateModel<IBaseDal<T>>(Constant.DalPath, Constant.IBaseDal));
        }

        /// <summary>
        /// 返回BLL层表对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        public static T CreateBllBase<T>() {
            return Create(() =>
            {
                string className = typeof(T).Name;
                className = className.Substring(1, className.Length - 1);
                return Reflex.CreateModel<T>(Constant.DalPath, Constant.BaseDal + "." + className);
            });
        }

        public static T CreateService<T>() {
            return Create(() =>
            {
                string className = typeof(T).Name;
                className = className.Substring(1, className.Length - 1);
                return Reflex.CreateModel<T>(Constant.BllPath, Constant.BaseService + "." + className);
            });
        }

    }
}