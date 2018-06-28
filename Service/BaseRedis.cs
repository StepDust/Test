using Common;
using Factory;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Service {
    /// <summary>
    /// Redis访问类
    /// Date：2018-06-16 16:12:23
    /// </summary>
    public class BaseRedis {

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        private static readonly string RedisConnectionString = Constant.RedisBaseContext;

        /// <summary>
        /// 用于锁定线程
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// Redis对象
        /// </summary>
        public static ConnectionMultiplexer ReaisManager {
            get {
                // 双重检查锁
                if (_redisConn == null || !_redisConn.IsConnected) {
                    lock (Locker) {
                        if (_redisConn == null || !_redisConn.IsConnected)
                            _redisConn = GetConnectionMultiplexer(RedisConnectionString);
                    }
                }
                return _redisConn;
            }
        }
        private static ConnectionMultiplexer _redisConn;

        /// <summary>
        /// 缓存所有的Redis对象
        /// </summary>
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 缓存Redis
        /// </summary>
        /// <param name="connectionString">Redis连接字符串</param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString) {
            if (!ConnectionCache.ContainsKey(connectionString)) {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        /// <summary>
        /// 根据Redis连接字符串，返回Redis对象
        /// </summary>
        /// <param name="connectionString">Redis连接字符串</param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager(string connectionString = null) {
            connectionString = connectionString ?? RedisConnectionString;
            var connect = ConnectionMultiplexer.Connect(connectionString);

            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return connect;
        }

        #region Redis事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e) {
            //  Console.WriteLine("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e) {
            //Console.WriteLine("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e) {
            //Console.WriteLine("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e) {
            //Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e) {
            //Console.WriteLine("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e) {
            //  Console.WriteLine("InternalError:Message" + e.Exception.Message);
        }

        #endregion 事件

    }
}
