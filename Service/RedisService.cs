using Common;
using Common.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service {
    /// <summary>
    /// Redis服务类
    /// Date：2018-06-17 11:38:58
    /// </summary>
    public class RedisService {

        /// <summary>
        /// Redis库编号
        /// </summary>
        private int DbIndex { get; }

        private readonly ConnectionMultiplexer _conn;
        private string CustomKey => Constant.CustomKey;

        #region 构造函数

        public RedisService(int dbIndex = 0) : this(dbIndex, null) {

        }

        public RedisService(int dbIndex, string readWriteHosts) {
            this.DbIndex = dbIndex;
            _conn =
                string.IsNullOrWhiteSpace(readWriteHosts) ?
                BaseRedis.ReaisManager :
                BaseRedis.GetConnectionMultiplexer(readWriteHosts);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 默认添加一个前缀
        /// </summary>
        /// <param name="oldKey"></param>
        /// <returns></returns>
        private string AddSysCustomKey(string oldKey) => $"{CustomKey}:{oldKey}";

        /// <summary>
        /// 读写Redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private T Do<T>(Func<IDatabase, T> func) {
            var redisBase = _conn.GetDatabase();
            return func.Invoke(redisBase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private IEnumerable<T> ConvertList<T>(RedisValue[] values) {
            foreach (var item in values) {
                yield return StringUtils.ConvertObject<T>(item);
            }
        }

        private RedisKey[] ConvertKeys(IEnumerable<string> redisKeys) {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }

        #endregion

        #region Key的管理

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>是否删除成功</returns>
        public bool KeyDelete(string key) {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyDelete(key));
        }

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key) {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyExists(key));
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">原始Key</param>
        /// <param name="newKey">新的Key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey) {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyRename(key, newKey));
        }

        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?)) {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyExpire(key, expiry));
        }

        #endregion

        #region String类型封装

        /// <summary>
        /// 保存单个数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?)) {
            key = AddSysCustomKey(key);
            return Do(db => 
                db.StringSet(key, value, expiry));
        }

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public string StringGet(string key) {
            key = AddSysCustomKey(key);
            return Do(db => db.StringGet(key));
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?)) {
            key = AddSysCustomKey(key);
            return Do(db => db.StringSet(key, StringUtils.ConvertJson(obj), expiry));
        }

        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key) {
            key = AddSysCustomKey(key);
            return Do(db => StringUtils.ConvertObject<T>(db.StringGet(key)));
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1) {
            key = AddSysCustomKey(key);
            return Do(db => db.StringIncrement(key, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1) {
            key = AddSysCustomKey(key);
            return Do(db => db.StringDecrement(key, val));
        }

        #endregion

        #region List类型封装

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListRemove<T>(string key, T value) {
            key = AddSysCustomKey(key);
            Do(db => db.ListRemove(key, StringUtils.ConvertJson(value)));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<T> ListRange<T>(string key) {
            key = AddSysCustomKey(key);
            return Do(redis =>
            {
                var values = redis.ListRange(key);
                return ConvertList<T>(values);
            });
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListRightPush<T>(string key, T value) {
            key = AddSysCustomKey(key);
            Do(db => db.ListRightPush(key, StringUtils.ConvertJson(value)));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key) {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var value = db.ListRightPop(key);
                return StringUtils.ConvertObject<T>(value);
            });
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListLeftPush<T>(string key, T value) {
            key = AddSysCustomKey(key);
            Do(db => db.ListLeftPush(key, StringUtils.ConvertJson(value)));
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key) {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var value = db.ListLeftPop(key);
                return StringUtils.ConvertObject<T>(value);
            });
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key) {
            key = AddSysCustomKey(key);
            return Do(redis => redis.ListLength(key));
        }

        #endregion

        #region 发布订阅

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="subChannel"></param>
        /// <param name="handler"></param>
        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null) {
            ISubscriber sub = _conn.GetSubscriber();
            sub.Subscribe(subChannel, (channel, message) =>
            {
                if (handler == null) {
                    Console.WriteLine(subChannel + " 订阅收到消息：" + message);
                }
                else {
                    handler(channel, message);
                }
            });
        }

        /// <summary>
        /// 发布订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long Publish<T>(string channel, T msg) {
            ISubscriber sub = _conn.GetSubscriber();
            return sub.Publish(channel, StringUtils.ConvertJson(msg));
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public void Unsubscribe(string channel) {
            ISubscriber sub = _conn.GetSubscriber();
            sub.Unsubscribe(channel);
        }

        /// <summary>
        /// 取消全部订阅
        /// </summary>
        public void UnsubscribeAll() {
            ISubscriber sub = _conn.GetSubscriber();
            sub.UnsubscribeAll();
        }

        #endregion 发布订阅      

    }
}