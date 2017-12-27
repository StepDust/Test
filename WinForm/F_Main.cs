using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Common;
using BLL.GSQ_PaChong;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace WinForm
{
    public partial class F_Main : Form
    {
        public F_Main()
        {
            InitializeComponent();
        }

        #region 写入日志

        /// <summary>
        /// 写入日志委托
        /// </summary>
        /// <param name="msg"></param>
        private delegate void textInvoke(string msg, Color Color);

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Color">文字颜色</param>
        private void SetLog(string msg, Color Color)
        {
            // 是否在其它线程调用此控件
            if (txt_log.InvokeRequired)
            {
                textInvoke ti = new textInvoke(SetLog);
                this.Invoke(ti, msg, Color);
            }
            else
            {
                msg += "\n";
                msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + "\t" + msg;

                txt_log.SelectionColor = Color;

                txt_log.AppendText(msg);

            }
        }

        /// <summary>
        /// 写入日志文本
        /// </summary>
        /// <param name="context">内容</param>
        public void SetLogTxt(string context)
        {
            string path = Path.GetFullPath($"../../LogTxt/{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            FileAction.AppendStr(path, context);
        }

        /// <summary>
        /// 清空日志显示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_clear_Click(object sender, EventArgs e)
        {
            SetLogTxt(txt_log.Text);
            txt_log.Text = "";
        }

        /// <summary>
        /// 一个小时定时清理日志信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_clear_Tick(object sender, EventArgs e)
        {
            SetLogTxt(txt_log.Text);
            txt_log.Text = "";
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_log.Text))
                SetLogTxt(txt_log.Text);
            SetLogTxt($"" +
                "==========>\n" +
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + "\t关闭窗体！\n" +
                $"\t\t\t\t\t\t\t运行时间：{lb_time.Text}\t抓取数量：{lb_num.Text}\t总数量：{lb_con.Text}\t爬取网站：{lb_all.Text} \n" +
                "==========>\n");
            Environment.Exit(0);
        }

        #endregion

        #region 更新数量

        /// <summary>
        /// 更新新闻抓取数量
        /// </summary>
        /// <param name="num"></param>
        public delegate void UpLabel(int num);

        /// <summary>
        /// 更新抓取数量
        /// </summary>
        /// <param name="num"></param>
        public void UpNum(int num)
        {
            if (lb_num.InvokeRequired)
            {
                UpLabel up = new UpLabel(UpNum);
                this.Invoke(up, num);
            }
            else
            {
                int n = 0;
                string text = lb_num.Text;
                int.TryParse(text.Substring(0, text.Length - 2), out n);
                n += num;
                lb_num.Text = n + " 个";
            }
        }

        /// <summary>
        /// 更新全部数量
        /// </summary>
        /// <param name="num"></param>
        public void UpCon(int num)
        {
            if (lb_num.InvokeRequired)
            {
                UpLabel up = new UpLabel(UpCon);
                this.Invoke(up, num);
            }
            else
            {
                int n = 0;
                string text = lb_con.Text;
                int.TryParse(text.Substring(0, text.Length - 2), out n);
                n += num;
                lb_con.Text = n + " 个";
            }
        }

        /// <summary>
        /// 更新全部数量
        /// </summary>
        /// <param name="num"></param>
        public void UpAll(int num)
        {
            if (lb_num.InvokeRequired)
            {
                UpLabel up = new UpLabel(UpAll);
                this.Invoke(up, num);
            }
            else
            {
                int n = 0;
                string text = lb_all.Text;
                int.TryParse(text.Substring(1, text.Length - 2), out n);
                n += num;
                lb_all.Text = "共 " + n + " 个";
            }
        }

        #endregion

        #region 运行时间

        int TimeCon = 0;

        private void t_con_Tick(object sender, EventArgs e)
        {
            TimeCon++;
            TimeSpan ts = new TimeSpan(0, 0, TimeCon);
            lb_time.Text = "";
            if ((int)ts.TotalHours > 0)
                lb_time.Text += (int)ts.TotalHours + " 时 ";
            if (ts.Minutes > 0 || (int)ts.TotalHours > 0)
                lb_time.Text += ts.Minutes + " 分 ";
            if (ts.Seconds > 0 || ts.Minutes > 0 || (int)ts.TotalHours > 0)
                lb_time.Text += ts.Seconds + " 秒";
        }

        #endregion

        #region 复选框加载&&联动

        private void Box_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (sender as CheckBox).Checked;

            if (check == false)
            {
                cb_all.Checked = false;
                return;
            }
            else
                foreach (Control Control in pl_UrlList.Controls)
                {
                    if (Control is CheckBox)
                        if ((Control as CheckBox).Checked != check)
                        {
                            cb_all.Checked = false;
                            return;
                        }
                }
            cb_all.Checked = true;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (sender as CheckBox).Checked;

            foreach (Control Control in pl_UrlList.Controls)
            {
                if (Control is CheckBox)
                    (Control as CheckBox).Checked = check;
            }
        }

        #endregion

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_Main_Load(object sender, EventArgs e)
        {

        }

        // 用于保存所有通信客户端的Socket
        Dictionary<Socket, ClientInfo> dicSocket = new Dictionary<Socket, ClientInfo>();
        private List<SocketMessage> msgPool = new List<SocketMessage>();
        private bool isClear = true;


        private void bt_Start_Click(object sender, EventArgs e)
        {
            // 定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 服务端发送信息需要一个IP地址和端口号  
            IPAddress address = IPAddress.Parse("192.168.20.93");
            // 将IP地址和端口号绑定到网络节点point上  
            IPEndPoint point = new IPEndPoint(address, 8888);

            // 监听绑定的网络节点  
            server.Bind(point);
            // 将套接字的监听队列长度限制为20  
            server.Listen(20);
            // 开始接受接入请求
            server.BeginAccept(new AsyncCallback(WatchConnecting), server);
            SetLog("开启Socket服务！地址：" + point.ToString(), Color.Gray);
            bt_Start.Enabled = false;
        }

        /// <summary>
        /// 监听客户端发来的请求  
        /// </summary>
        /// <param name="result">服务端socket</param>
        private void WatchConnecting(IAsyncResult result)
        {
            Socket server = result.AsyncState as Socket;
            Socket client = server.EndAccept(result);
            try
            {

                // 处理下一个客户端连接
                server.BeginAccept(new AsyncCallback(WatchConnecting), server);

                byte[] buffer = new byte[1024];

                ClientInfo info = new ClientInfo();
                info.Id = client.RemoteEndPoint;
                info.handle = client.Handle;
                info.buffer = buffer;
                // 把客户端存入clientPool
                dicSocket.Add(client, info);

                // 接收客户端消息
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Recieve), client);

                //// 获取客户端的IP和端口号  
                //IPEndPoint EndPoint = client.RemoteEndPoint as IPEndPoint;
                //IPAddress clientIP = EndPoint.Address;
                //int clientPort = EndPoint.Port;

                // 让客户显示"连接成功的"的信息  
                string sendmsg = "连接服务端成功！\r\n" + "本地IP:";
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                client.Send(arrSendMsg);

                // 显示与客户端连接情况
                SetLog("成功与" + client.RemoteEndPoint + "客户端建立连接！", Color.Green);
            }
            catch (Exception ex)
            {
                // 提示套接字监听异常     
                SetLog(ex.Message, Color.Red);
                return;
            }
        }

        /// <summary>
		/// 处理客户端发送的消息，接收成功后加入到msgPool，等待广播
		/// </summary>
		/// <param name="result">Result.</param>
		private void Recieve(IAsyncResult result)
        {
            Socket client = result.AsyncState as Socket;
            
            if (client == null || !dicSocket.ContainsKey(client))
                return;

            try
            {
                int length = client.EndReceive(result);
                byte[] buffer = dicSocket[client].buffer;

                //接收消息
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Recieve), client);
                string msg = Encoding.UTF8.GetString(buffer, 0, length);

                if (!dicSocket[client].IsHandShaked && msg.Contains("Sec-WebSocket-Key"))
                {
                    client.Send(PackageHandShakeData(buffer, length));
                    dicSocket[client].IsHandShaked = true;
                    return;
                }

                msg = AnalyzeClientData(buffer, length);

                SocketMessage sm = new SocketMessage();
                sm.Client = dicSocket[client];
                sm.Time = DateTime.Now;

                Regex reg = new Regex(@"{<(.*?)>}");
                Match m = reg.Match(msg);
                if (m.Value != "")
                { //处理客户端传来的用户名
                    dicSocket[client].NickName = Regex.Replace(m.Value, @"{<(.*?)>}", "$1");
                    sm.isLoginMessage = true;
                    sm.Message = "login!";
                    Console.WriteLine("{0} login @ {1}", client.RemoteEndPoint, DateTime.Now);
                }
                else
                { //处理客户端传来的普通消息
                    sm.isLoginMessage = false;
                    sm.Message = msg;
                    Console.WriteLine("{0} @ {1}\r\n    {2}", client.RemoteEndPoint, DateTime.Now, sm.Message);
                }
                msgPool.Add(sm);
                isClear = false;

            }
            catch (Exception ex)
            {
                // 把客户端标记为关闭，并在clientPool中清除
               // client.Disconnect(true);
                SetLog(ex.Message + " " + dicSocket[client].Name, Color.Red);
                
                dicSocket.Remove(client);
            }
        }

        /// <summary>
        /// 打包服务器握手数据
        /// </summary>
        /// <returns>The hand shake data.</returns>
        /// <param name="handShakeBytes">Hand shake bytes.</param>
        /// <param name="length">Length.</param>
        private byte[] PackageHandShakeData(byte[] handShakeBytes, int length)
        {
            string handShakeText = Encoding.UTF8.GetString(handShakeBytes, 0, length);
            string key = string.Empty;
            Regex reg = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = reg.Match(handShakeText);
            if (m.Value != "")
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }

            byte[] secKeyBytes = SHA1.Create().ComputeHash(
                                     Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            string secKey = Convert.ToBase64String(secKeyBytes);

            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + "\r\n");
            responseBuilder.Append("Upgrade: websocket" + "\r\n");
            responseBuilder.Append("Connection: Upgrade" + "\r\n");
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKey + "\r\n\r\n");

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }

        /// <summary>
		/// 解析客户端发送来的数据
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="recBytes">Rec bytes.</param>
		/// <param name="length">Length.</param>
		private string AnalyzeClientData(byte[] recBytes, int length)
        {
            if (length < 2)
            {
                return string.Empty;
            }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧  
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理 
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码  
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度  

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }

            return Encoding.UTF8.GetString(payload_data);
        }

        /// <summary>
        /// 把发送给客户端消息打包处理（拼接上谁什么时候发的什么消息）
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="message">Message.</param>
        private byte[] PackageServerData(SocketMessage sm)
        {
            StringBuilder msg = new StringBuilder();
            if (!sm.isLoginMessage)
            { //消息是login信息
                msg.AppendFormat("{0} @ {1}:\r\n    ", sm.Client.Name, sm.Time.ToShortTimeString());
                msg.Append(sm.Message);
            }
            else
            { //处理普通消息
                msg.AppendFormat("{0} login @ {1}", sm.Client.Name, sm.Time.ToShortTimeString());
            }


            byte[] content = null;
            byte[] temp = Encoding.UTF8.GetBytes(msg.ToString());

            if (temp.Length < 126)
            {
                content = new byte[temp.Length + 2];
                content[0] = 0x81;
                content[1] = (byte)temp.Length;
                Array.Copy(temp, 0, content, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                content = new byte[temp.Length + 4];
                content[0] = 0x81;
                content[1] = 126;
                content[2] = (byte)(temp.Length & 0xFF);
                content[3] = (byte)(temp.Length >> 8 & 0xFF);
                Array.Copy(temp, 0, content, 4, temp.Length);
            }
            else
            {
                // 暂不处理超长内容  
            }

            return content;
        }

        /// <summary>
        /// 接收客户端发来的信息，客户端套接字对象
        /// </summary>
        /// <param name="socketclientpara"></param>    
        public void Recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;

            while (true)
            {
                // 创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                // 将接收到的信息存入到内存缓冲区，并返回其字节数组的长度    
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);

                    // 将机器接受到的字节数组转换为人可以读懂的字符串     
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);

                    // 将发送的字符串信息附加到文本框txtMsg上     
                    SetLog("客户端:" + socketServer.RemoteEndPoint + "\r\n" + strSRecMsg + "", Color.LightSkyBlue);

                    socketServer.Send(Encoding.UTF8.GetBytes("测试server 是否可以发送数据给client "));
                }
                catch (Exception ex)
                {
                    //dicSocket.Remove(socketServer.RemoteEndPoint.ToString());

                    // 提示套接字监听异常  
                    SetLog("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n", Color.Red);
                    // 关闭之前accept出来的和客户端进行通信的套接字
                    socketServer.Close();
                    break;
                }
            }
        }

        private void bt_Stop_Click(object sender, EventArgs e)
        {

        }

        private void txt_log_TextChanged(object sender, EventArgs e)
        {
            
        }
    }


    public class ClientInfo
    {
        public byte[] buffer;

        public string NickName { get; set; }

        public EndPoint Id { get; set; }

        public IntPtr handle { get; set; }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(NickName))
                {
                    return NickName;
                }
                else
                {
                    return string.Format("{0}#{1}", Id, handle);
                }
            }
        }

        public bool IsHandShaked { get; set; }
    }

    public class SocketMessage
    {
        public bool isLoginMessage { get; set; }

        public ClientInfo Client { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }

}
