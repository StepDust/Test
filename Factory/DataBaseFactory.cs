﻿using Common;
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
            // 从线程数据集合（CallContext）中拿对应键值的数据
            TResult model = (TResult)CallContext.GetData(typeof(TResult).AssemblyQualifiedName);
            // 若没有指定对象
            if (model == null) {
                model = func.Invoke();
                // 创建了实例后存入
                if (model != null)
                    CallContext.SetData(typeof(TResult).AssemblyQualifiedName, model);
            }
            return model;
        }

        /// <summary>
        /// 创建上下文对象，保证线程内唯一
        /// </summary>
        /// <param name="ConStr">连接字符串</param>
        /// <returns></returns>
        public static DbContext CreateDbContext() {
            return Create(() =>
                Reflex.CreateModel<DbContext>(Constant.DataBaseContext[0], Constant.DataBaseContext[1]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateService<T>() {
            return Create(() =>
                Reflex.CreateModel<T>());
        }

    }
}