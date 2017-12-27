using BLL.Demo;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers
{
    public class SoketController : Manager
    {
        // 创建 1个客户端套接字 和1个负责监听服务端请求的线程  
        Thread threadclient = null;
        Socket socketclient = null;

        // GET: WebFunction/Soket
        public ActionResult Index()
        {
            IndexData data = new IndexData();

            S_UserService _UserService = new S_UserService();

            if (Session["S_User"] != null)
            {
                data._USer = Session["S_User"] as S_User;
                data.UserList = _UserService.LoadEntities(c => true).ToList();

                #region Soket

                //定义一个套接字监听  
                //socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ////获取文本框中的IP地址  
                //IPAddress address = IPAddress.Parse("192.168.20.93");
                ////将获取的IP地址和端口号绑定在网络节点上  
                //IPEndPoint point = new IPEndPoint(address, 8888);
                //try
                //{
                //    //客户端套接字连接到网络节点上，用的是Connect  
                //    socketclient.Connect(point);

                //}
                //catch (Exception)
                //{
                //    Debug.WriteLine("连接失败\r\n");

                //    // this.txtDebugInfo.AppendText("连接失败\r\n");
                //    ;
                //}

                //threadclient = new Thread(Recv);
                //threadclient.IsBackground = true;
                //threadclient.Start();
                #endregion

            }

            return View(data);
        }

        /// <summary>
        /// 接收服务端发来信息的方法    
        /// </summary>
        private void Recv()
        {
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                    byte[] arrRecvmsg = new byte[1024 * 1024];

                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = socketclient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if (x == 1)
                    {
                        IndexCompleted("服务器:" + DateTime.Now + "\r\n" + strRevMsg + "\r\n\n");
                        Debug.WriteLine("服务器:" + DateTime.Now + "\r\n" + strRevMsg + "\r\n\n");
                    }
                    else
                    {
                        // this.txtDebugInfo.AppendText(strRevMsg + "\r\n\n");
                        Debug.WriteLine(strRevMsg + "\r\n\n");
                        x = 1;
                    }
                }
                catch (Exception ex)
                {
                    IndexCompleted(ex.Message);
                    Debug.WriteLine("远程服务器已经中断连接" + "\r\n\n");
                    Debug.WriteLine("远程服务器已经中断连接" + "\r\n");
                    break;
                }
            }
        }

        /// <summary>
        /// 发送字符信息到服务端的方法  
        /// </summary>
        /// <param name="sendMsg"></param>
        void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //调用客户端套接字发送字节数组     
            socketclient.Send(arrClientSendMsg);
            //将发送的信息追加到聊天内容文本框中     
            Debug.WriteLine("hello...." + ": " + DateTime.Now + "\r\n" + sendMsg + "\r\n\n");
            //this.txtDebugInfo.AppendText("hello...." + ": " + GetCurrentTime() + "\r\n" + sendMsg + "\r\n\n");
        }

        /// <summary>
        /// 当异步线程完成时向客户端发送响应
        /// </summary>
        /// <param name="token">数据封装对象</param>
        /// <returns></returns>
        public ActionResult IndexCompleted(string info)
        {
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConWebSocket() {
             
            return Json("{}");
        }


        /// <summary>
        /// 登录&&注册
        /// </summary>
        /// <param name="_User"></param>
        public string LoginSoket(S_User _User)
        {
            S_UserService _UserService = new S_UserService();

            S_User model = _UserService.FindEntity(c => c.UserName == _User.UserName && c.Password == _User.Password);

            if (model == null)
            {
                _User.LoginTime = new DateTime();
                _User.LoginTime = DateTime.Now;
                _User.LoginState = 1;
                _UserService.AddEntity(_User);
                _UserService.SaveChanges();
                Session["S_User"] = _User;
            }
            else
            {
                model.LoginTime = DateTime.Now;
                model.LoginState = 1;
                _UserService.EditEntity(model);
                Session["S_User"] = model;
            }
            return Utils.ObjectToJson(Session["S_User"]);
        }

        public string CreateRoom(string userID)
        {
            S_RoomService _RoomService = new S_RoomService();


            return "";
        }

    }

    public class IndexData
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<S_User> UserList { get; set; }
        /// <summary>
        /// 登录人
        /// </summary>
        public S_User _USer { get; set; }
    }

}