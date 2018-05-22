using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Demo
{
    /// <summary>
    /// 聊天房间
    /// </summary>
    public class S_RoomService : BaseService<S_Room>
    {
        public S_RoomService() : base(Config.Name_Demo) { }

        /// <summary>
        /// 返回一个房间
        /// </summary>
        /// <param name="userId">聊天对象ID</param>
        /// <param name="logId">登陆者ID</param>
        /// <returns></returns>
        public S_Room GetRoom(int userId, int logId)
        {
            S_RoomMemberService _RoomMemberService = new S_RoomMemberService();
            S_Room _Room = new S_Room();

            // 查找同时包含两个用户的房间
            List<S_RoomMember> list = (
                from user in _RoomMemberService.LoadEntities(c => c.UserID == userId)
                join log in _RoomMemberService.LoadEntities(c => c.UserID == logId) on user.RoomID equals log.RoomID
                select new S_RoomMember
                {
                    RoomID = user.RoomID
                }).ToList();

            if (list == null || list.Count <= 0)
            {

            }



            return _Room;
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        //public S_Room CreateRoom(string IdStr) {

             

        //}

    }
}
