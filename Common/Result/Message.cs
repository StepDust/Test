﻿using Interfaces;

namespace Common {
    /// <summary>
    /// 消息类
    /// </summary>
    public class Message : IMessage {

        public Message() { }

        public Message(int state, string msg) {
            this.State = state;
            this.Msg = msg;
        }

        public Message(bool state, string msg) {
            this.State = state ? 1 : 0;
            this.Msg = msg;
        }

        /// <summary>
        /// 状态，（0:失败，1:成功）
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
    }
}
