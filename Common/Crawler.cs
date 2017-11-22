using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web.Script;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common {
    /// <summary>
    /// 爬虫类
    /// </summary>
    public class Crawler {

        /// <summary>
        /// 爬虫开始事件
        /// </summary>
        public event EventHandler<OnStartEventArgs> OnStart;
        /// <summary>
        /// 爬虫完成事件
        /// </summary>
        public event EventHandler<OnCompletedEventArgs> OnCompleted;
        /// <summary>
        /// 爬虫出错事件
        /// </summary>
        public event EventHandler<Exception> OnError;

        /// <summary>
        /// Cookie容器
        /// </summary>
        public CookieContainer CookiesContainer { get; set; }

        /// <summary>
        /// 定义PhantomJS内核参数
        /// </summary>
        private PhantomJSOptions _options = new PhantomJSOptions();

        /// <summary>
        /// 定义Selenium驱动配置
        /// </summary>
        private PhantomJSDriverService _service = PhantomJSDriverService.CreateDefaultService();

        /// <summary>
        /// 开始抓取数据
        /// </summary>
        /// <param name="uriStr">地址</param>
        /// <param name="regex">匹配正则</param>
        /// <param name="proxy">代理服务IP</param>
        /// <returns></returns>
        public async Task<string> Start(string uriStr, string regex = "", WebProxy proxy = null)
        {
            return await Task.Run(() => {
                string pageSource = string.Empty;
                Uri uri = new Uri(uriStr);

                // 触发开启事件
                if (this.OnStart != null) this.OnStart(this, new OnStartEventArgs(uri));
                // 记录运行时间
                Stopwatch watch = new Stopwatch();
                watch.Start();
                // 发送请求
                HttpWebRequest request = null;
                // 响应请求
                HttpWebResponse respose = null;
                // 响应流
                Stream stream = null;
                // 读取流
                StreamReader read = null;

                try {

                    // 请求网站
                    request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Accept = "*/*";
                    // 自定义文档类型及编码
                    request.ContentType = "application/x-www-form-urlencoded";
                    // 禁止自动跳转
                    request.AllowAutoRedirect = false;
                    // 设置Uesr-Agent，伪装为谷歌
                    // requert.UserAgent = "Mozilla/5.0 (Windows NT 10.0:)";
                    // 请求超时，5秒
                    request.Timeout = 5000;
                    // 启用长链接
                    request.KeepAlive = true;
                    // 请求方式
                    request.Method = "GET";
                    // 设置代理服务器IP，伪装请求地址
                    if (proxy != null) request.Proxy = proxy;
                    // 附加Cookie容器
                    request.CookieContainer = this.CookiesContainer;
                    // 最大连接数
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    // 获取请求响应
                    respose = (HttpWebResponse)request.GetResponse();
                    // 将Cookie加入容器，并保存登录状态
                    foreach (System.Net.Cookie item in respose.Cookies) {
                        this.CookiesContainer.Add(item);
                    }
                    // 获取响应流
                    stream = respose.GetResponseStream();
                    // 以UTF8的方式读取
                    read = new StreamReader(stream, Encoding.UTF8);
                    // 获取网页源代码
                    pageSource = read.ReadToEnd();
                    // 停止计时
                    watch.Stop();

                    // 获取当前任务线程id
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    // 获取执行时间
                    long milliseconds = watch.ElapsedMilliseconds;
                    // 触发结束事件
                    if (this.OnCompleted != null) this.OnCompleted(this, new OnCompletedEventArgs(uri, threadId, pageSource, regex, milliseconds));
                }
                catch (Exception e) {
                    if (this.OnError != null) this.OnError(this, e);
                }
                finally {
                    // 释放资源
                    if (stream != null)
                        stream.Close();
                    if (read != null)
                        read.Close();
                    if (request != null)
                        request.Abort();
                    if (respose != null)
                        respose.Close();
                }
                return pageSource;
            });
        }

        public async Task Start(string uriStr, Operation operation, Script script = null)
        {
            await Task.Run(() => {

                Uri uri = new Uri(uriStr);
                // 触发开启事件
                if (this.OnStart != null) this.OnStart(this, new OnStartEventArgs(uri));
                // 实例化PhantomJS的WebDricer
                PhantomJSDriver driver = new PhantomJSDriver(_service);

                try {
                    DateTime watch = DateTime.Now;

                    // 请求Url
                    driver.Navigate().GoToUrl(uriStr);
                    // 执行JavaScript代码
                    if (script != null) driver.ExecuteAsyncScript(script.Code, script.Args);
                    // 执行网页操作
                    if (operation.Action != null) operation.Action.Invoke(driver);
                    // 设置超时时间
                    WebDriverWait driverWatch = new WebDriverWait(driver, TimeSpan.FromMilliseconds(operation.timeout));
                    if (operation.Condition != null) driverWatch.Until(operation.Condition);

                    // 获取线程ID
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    // 获取执行时间
                    long milliseconds = DateTime.Now.Subtract(watch).Milliseconds;
                    string pageSource = driver.PageSource;
                    // 触发结束事件
                    if (this.OnCompleted != null) this.OnCompleted(this, new OnCompletedEventArgs(uri, threadId, pageSource, driver, milliseconds));
                }
                catch (Exception e) {

                    if (this.OnError != null) this.OnError(this, e);
                }
                finally {
                    driver.Close();
                    driver.Quit();
                }
            });
        }
    }

    /// <summary>
    /// 爬虫启动事件
    /// </summary>
    public class OnStartEventArgs {

        public OnStartEventArgs(Uri url) => this.Uri = url;

        /// <summary>
        /// 爬虫地址
        /// </summary>
        public Uri Uri { get; set; }

    }

    /// <summary>
    /// 爬虫结束事件
    /// </summary>
    public class OnCompletedEventArgs {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">爬虫地址</param>
        /// <param name="treadID">线程ID</param>
        /// <param name="pageSource">页面源代码</param>
        /// <param name="milliseconds">爬虫请求执行时间</param>
        public OnCompletedEventArgs(Uri url, int treadID, string pageSource, string regex, long milliseconds)
        {
            this.Uri = url;
            this.TreadID = treadID;
            this.PageSource = pageSource;
            this.Regex = regex;
            this.Milliseconds = milliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">爬虫地址</param>
        /// <param name="treadID">线程ID</param>
        /// <param name="pageSource">页面源代码</param>
        /// <param name="milliseconds">爬虫请求执行时间</param>
        public OnCompletedEventArgs(Uri url, int treadID, string pageSource, PhantomJSDriver driver, long milliseconds)
        {
            this.Uri = url;
            this.TreadID = treadID;
            this.PageSource = pageSource;
            this.Driver = driver;
            this.Milliseconds = milliseconds;
        }

        /// <summary>
        /// 爬虫地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 线程ID
        /// </summary>
        public int TreadID { get; private set; }

        /// <summary>
        /// 页面源代码
        /// </summary>
        public string PageSource { get; set; }

        /// <summary>
        /// 页面源代码
        /// </summary>
        public string Regex { get; set; }

        public PhantomJSDriver Driver { get; set; }

        /// <summary>
        /// 爬虫请求执行时间
        /// </summary>
        public long Milliseconds { get; private set; }

    }

    public class Operation {

        public Action<PhantomJSDriver> Action;

        public Func<IWebDriver, bool> Condition;

        public int timeout { get; set; } = 5000;
    }

    public class Script {

        public string Code;

        public object[] Args;

    }

}
